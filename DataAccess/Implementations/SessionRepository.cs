using DatabaseAccess.DatabaseContext;
using DatabaseAccess.Entity;
using DatabaseAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.Implementations
{
	public class SessionRepository : ISessionRepository
	{
		private readonly PlanningPokerDbContext _context;

		public SessionRepository(PlanningPokerDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Session>> GetAllActiveSessionsAsync()
		{
			return await _context.Sessions
				.Where(s => s.IsActive)
				.ToListAsync();
		}

		public async Task<Session?> GetSessionByIdAsync(Guid id, bool includeRelated = false)
		{
			IQueryable<Session> query = _context.Sessions;

			if (includeRelated)
			{
				query = query
					.Include(s => s.Participants.Where(p => p.IsActive))
					.Include(s => s.Stories);
			}
			return await query.FirstOrDefaultAsync(s => s.Id == id);
		}


		public async Task<Session> CreateSessionAsync(Session session)
		{
			_context.Sessions.Add(session);
			await _context.SaveChangesAsync();
			return session;
		}

		public async Task<bool> DeactivateSessionAsync(Guid id, string moderatorToken)
		{
			var session = await _context.Sessions.FindAsync(id);

			if (session == null || session.ModeratorToken != moderatorToken)
				return false;

			session.IsActive = false;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
