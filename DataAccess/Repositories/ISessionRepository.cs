using DatabaseAccess.Entity;

namespace DatabaseAccess.Repositories
{
	public interface ISessionRepository
	{
		Task<IEnumerable<Session>> GetAllActiveSessionsAsync();
		Task<Session?> GetSessionByIdAsync(Guid id, bool includeRelated = false);
		Task<Session> CreateSessionAsync(Session session);
		Task<bool> DeactivateSessionAsync(Guid id, string moderatorToken);
	}
}
