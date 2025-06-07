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
	public class ParticipantsController : ControllerBase
	{
		private readonly IParticipantRepository _participantRepository;
		private readonly ISessionRepository _sessionRepository;
		private readonly IHubContext<PlanningPokerHub> _hubContext;

		public ParticipantsController(
			IParticipantRepository participantRepository,
			ISessionRepository sessionRepository,
			IHubContext<PlanningPokerHub> hubContext)
		{
			_participantRepository = participantRepository;
			_sessionRepository = sessionRepository;
			_hubContext = hubContext;
		}

		// GET: api/Participants/session/{sessionId}
		[HttpGet("session/{sessionId}")]
		public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetParticipantsBySession(Guid sessionId)
		{
			var participants = await _participantRepository.GetParticipantsBySessionAsync(sessionId);

			return Ok(participants.Select(p => new ParticipantDto
			{
				Id = p.Id,
				Name = p.Name,
				SessionId = p.SessionId,
				JoinedAt = p.JoinedAt
			}));
		}

		// POST: api/Participants
		[HttpPost]
		public async Task<ActionResult<ParticipantDto>> AddParticipant(ParticipantCreateDto participantDto)
		{
			var session = await _sessionRepository.GetSessionByIdAsync(participantDto.SessionId);

			if (session == null || !session.IsActive)
			{
				return BadRequest("Session not found or inactive");
			}

			var participant = new Participant
			{
				Name = participantDto.Name,
				SessionId = participantDto.SessionId
			};

			var createdParticipant = await _participantRepository.AddParticipantAsync(participant);

			var participantResponse = new ParticipantDto
			{
				Id = createdParticipant.Id,
				Name = createdParticipant.Name,
				SessionId = createdParticipant.SessionId,
				JoinedAt = createdParticipant.JoinedAt
			};

			await _hubContext.Clients.Group(participantDto.SessionId.ToString())
				.SendAsync("ParticipantJoined", participantResponse);

			return CreatedAtAction(
				nameof(GetParticipant),
				new { id = createdParticipant.Id },
				participantResponse
			);
		}

		// GET: api/Participants/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ParticipantDto>> GetParticipant(Guid id)
		{
			var participant = await _participantRepository.GetParticipantByIdAsync(id);

			if (participant == null)
			{
				return NotFound();
			}

			return Ok(new ParticipantDto
			{
				Id = participant.Id,
				Name = participant.Name,
				SessionId = participant.SessionId,
				JoinedAt = participant.JoinedAt
			});
		}

		// DELETE: api/Participants/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveParticipant(Guid id)
		{
			var participant = await _participantRepository.GetParticipantByIdAsync(id);

			if (participant == null)
			{
				return NotFound();
			}

			var success = await _participantRepository.RemoveParticipantAsync(id);

			if (success)
			{
				await _hubContext.Clients.Group(participant.SessionId.ToString())
					.SendAsync("ParticipantLeft", new { ParticipantId = id });
			}

			return NoContent();
		}
	}
}
