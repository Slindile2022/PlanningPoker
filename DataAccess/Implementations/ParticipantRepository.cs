using DatabaseAccess.DatabaseContext;
using DatabaseAccess.Entity;
using DatabaseAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.Implementations
{
	public class ParticipantRepository : IParticipantRepository
	{
		private readonly PlanningPokerDbContext _context;

		public ParticipantRepository(PlanningPokerDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Participant>> GetParticipantsBySessionAsync(Guid sessionId)
		{
			return await _context.Participants
				.Where(p => p.SessionId == sessionId && p.IsActive)
				.ToListAsync();
		}

		public async Task<Participant?> GetParticipantByIdAsync(Guid id)
		{
			return await _context.Participants.FindAsync(id);
		}

		public async Task<Participant> AddParticipantAsync(Participant participant)
		{
			_context.Participants.Add(participant);
			await _context.SaveChangesAsync();
			return participant;
		}

		public async Task<bool> RemoveParticipantAsync(Guid id)
		{
			var participant = await _context.Participants.FindAsync(id);

			if (participant == null)
				return false;

			participant.IsActive = false;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
