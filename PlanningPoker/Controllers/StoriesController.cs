using DatabaseAccess.Entity;
using DatabaseAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PlanningPoker.DTOs;
using PlanningPoker.Hubs;

namespace PlanningPoker.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoriesController : ControllerBase
	{
		private readonly IStoryRepository _storyRepository;
		private readonly ISessionRepository _sessionRepository;
		private readonly IHubContext<PlanningPokerHub> _hubContext;

		public StoriesController(
			IStoryRepository storyRepository,
			ISessionRepository sessionRepository,
			IHubContext<PlanningPokerHub> hubContext)
		{
			_storyRepository = storyRepository;
			_sessionRepository = sessionRepository;
			_hubContext = hubContext;
		}

		// GET: api/Stories/session/{sessionId}
		[HttpGet("session/{sessionId}")]
		public async Task<ActionResult<IEnumerable<StoryDto>>> GetStoriesBySession(Guid sessionId)
		{
			var stories = await _storyRepository.GetStoriesBySessionAsync(sessionId);

			return Ok(stories.Select(s => new StoryDto
			{
				Id = s.Id,
				Title = s.Title,
				Description = s.Description,
				SessionId = s.SessionId,
				Status = s.Status
			}));
		}

		// GET: api/Stories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<StoryDto>> GetStory(Guid id)
		{
			var story = await _storyRepository.GetStoryByIdAsync(id, includeVotes: true);

			if (story == null)
			{
				return NotFound();
			}

			return Ok(new StoryDto
			{
				Id = story.Id,
				Title = story.Title,
				Description = story.Description,
				SessionId = story.SessionId,
				Status = story.Status
			});
		}

		// POST: api/Stories
		[HttpPost]
		public async Task<ActionResult<StoryDto>> CreateStory(StoryCreateDto storyDto)
		{
			var session = await _sessionRepository.GetSessionByIdAsync(storyDto.SessionId);

			if (session == null || !session.IsActive)
			{
				return BadRequest("Session not found or inactive");
			}

			if (session.ModeratorToken != storyDto.ModeratorToken)
			{
				return Unauthorized();
			}

			var story = new Story
			{
				Title = storyDto.Title,
				Description = storyDto.Description,
				SessionId = storyDto.SessionId
			};

			var createdStory = await _storyRepository.CreateStoryAsync(story);

			var storyResponse = new StoryDto
			{
				Id = createdStory.Id,
				Title = createdStory.Title,
				Description = createdStory.Description,
				SessionId = createdStory.SessionId,
				Status = createdStory.Status
			};

			await _hubContext.Clients.Group(storyDto.SessionId.ToString())
				.SendAsync("StoryAdded", storyResponse);

			return CreatedAtAction(
				nameof(GetStory),
				new { id = createdStory.Id },
				storyResponse
			);
		}

		// PATCH: api/Stories/5/status
		[HttpPatch("{id}/status")]
		public async Task<IActionResult> UpdateStoryStatus(Guid id, StoryStatusUpdateDto statusDto)
		{
			var story = await _storyRepository.GetStoryByIdAsync(id);

			if (story == null)
			{
				return NotFound();
			}

			var success = await _storyRepository.UpdateStoryStatusAsync(id, statusDto.Status, statusDto.ModeratorToken);

			if (!success)
			{
				return Unauthorized("Invalid moderator token");
			}

			await _hubContext.Clients.Group(story.SessionId.ToString())
				.SendAsync("StoryStatusChanged", new { StoryId = id, Status = statusDto.Status });

			return NoContent();
		}
	}
}
