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
	public class SessionsController : ControllerBase
	{
		private readonly ISessionRepository _sessionRepository;
		private readonly IHubContext<PlanningPokerHub> _hubContext;

		public SessionsController(ISessionRepository sessionRepository, IHubContext<PlanningPokerHub> hubContext)
		{
			_sessionRepository = sessionRepository;
			_hubContext = hubContext;
		}

		// GET: api/Sessions
		[HttpGet]
		public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
		{
			var sessions = await _sessionRepository.GetAllActiveSessionsAsync();

			return Ok(sessions.Select(s => new SessionDto
			{
				Id = s.Id,
				Name = s.Name,
				CreatedAt = s.CreatedAt
			}));
		}

		// GET: api/Sessions/5
		[HttpGet("{id}")]
		public async Task<ActionResult<SessionDto>> GetSession(Guid id)
		{
			var session = await _sessionRepository.GetSessionByIdAsync(id, includeRelated: true);

			if (session == null)
			{
				return NotFound();
			}

			return Ok(new SessionDto
			{
				Id = session.Id,
				Name = session.Name,
				CreatedAt = session.CreatedAt,
				ModeratorToken = session.ModeratorToken
			});
		}

		// POST: api/Sessions
		[HttpPost]
		public async Task<ActionResult<SessionDto>> CreateSession(SessionCreateDto sessionDto)
		{
			var session = new Session
			{
				Name = sessionDto.Name
			};

			var createdSession = await _sessionRepository.CreateSessionAsync(session);

			return CreatedAtAction(
				nameof(GetSession),
				new { id = createdSession.Id },
				new SessionDto
				{
					Id = createdSession.Id,
					Name = createdSession.Name,
					CreatedAt = createdSession.CreatedAt,
					ModeratorToken = createdSession.ModeratorToken
				}
			);
		}

		// DELETE: api/Sessions/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSession(Guid id, [FromHeader] string moderatorToken)
		{
			var success = await _sessionRepository.DeactivateSessionAsync(id, moderatorToken);

			if (!success)
			{
				return NotFound("Session not found or invalid moderator token");
			}

			await _hubContext.Clients.Group(id.ToString())
				.SendAsync("SessionDeactivated", new { SessionId = id });

			return NoContent();
		}
	}
}
