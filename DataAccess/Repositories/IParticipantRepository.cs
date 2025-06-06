using DatabaseAccess.Entity;

namespace DatabaseAccess.Repositories
{
	public interface IParticipantRepository
	{
		Task<IEnumerable<Participant>> GetParticipantsBySessionAsync(Guid sessionId);
		Task<Participant?> GetParticipantByIdAsync(Guid id);
		Task<Participant> AddParticipantAsync(Participant participant);
		Task<bool> RemoveParticipantAsync(Guid id);
	}
}
