using DatabaseAccess.Entity;
using DatabaseAccess.Enums;
using DatabaseAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PlanningPoker.DTOs;
using PlanningPoker.Hubs;

namespace PlanningPoker.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VotesController : ControllerBase
	{
		private readonly IVoteRepository _voteRepository;
		private readonly IStoryRepository _storyRepository;
		private readonly ISessionRepository _sessionRepository;
		private readonly IHubContext<PlanningPokerHub> _hubContext;

		public VotesController(
			IVoteRepository voteRepository,
			IStoryRepository storyRepository,
			ISessionRepository sessionRepository,
			IHubContext<PlanningPokerHub> hubContext)
		{
			_voteRepository = voteRepository;
			_storyRepository = storyRepository;
			_sessionRepository = sessionRepository;
			_hubContext = hubContext;
		}

		// POST: api/Votes
		[HttpPost]
		public async Task<ActionResult> SubmitVote(VoteSubmitDto voteDto)
		{
			var story = await _storyRepository.GetStoryByIdAsync(voteDto.StoryId);

			if (story == null)
			{
				return BadRequest("Story not found");
			}

			if (story.Status != StoryStatus.Voting)
			{
				return BadRequest("Voting is not in progress for this story");
			}

			var vote = new Vote
			{
				StoryId = voteDto.StoryId,
				ParticipantId = voteDto.ParticipantId,
				Value = voteDto.Value
			};

			var success = await _voteRepository.SubmitVoteAsync(vote);

			if (success)
			{
				var totalVotes = await _voteRepository.GetTotalVotesForStoryAsync(voteDto.StoryId);

				await _hubContext.Clients.Group(story.SessionId.ToString())
					.SendAsync("VoteSubmitted", new { StoryId = story.Id, TotalVotes = totalVotes });
			}

			return NoContent();
		}

		// GET: api/Votes/story/{storyId}
		[HttpGet("story/{storyId}")]
		public async Task<ActionResult<VoteResultsDto>> GetVoteResults(Guid storyId, [FromHeader] string moderatorToken)
		{
			var story = await _storyRepository.GetStoryByIdAsync(storyId);

			if (story == null)
			{
				return NotFound();
			}

			var session = await _sessionRepository.GetSessionByIdAsync(story.SessionId);

			// Determine if detailed votes should be included
			bool includeDetails = story.Status == StoryStatus.Revealed ||
								 story.Status == StoryStatus.Completed ||
								 session.ModeratorToken == moderatorToken;

			var results = new VoteResultsDto
			{
				StoryId = storyId,
				TotalVotes = await _voteRepository.GetTotalVotesForStoryAsync(storyId),
				Average = await _voteRepository.GetAverageVoteForStoryAsync(storyId),
				Median = await _voteRepository.GetMedianVoteForStoryAsync(storyId),
				Status = story.Status
			};

			if (includeDetails)
			{
				var votes = await _voteRepository.GetVotesByStoryAsync(storyId);

				results.Votes = votes.Select(v => new VoteDetailDto
				{
					ParticipantId = v.ParticipantId,
					ParticipantName = v.Participant.Name,
					Value = v.Value
				}).ToList();
			}

			return Ok(results);
		}
	}
}
