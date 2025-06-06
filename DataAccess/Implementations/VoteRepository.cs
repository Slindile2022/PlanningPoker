using DatabaseAccess.DatabaseContext;
using DatabaseAccess.Entity;
using DatabaseAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.Implementations
{
	public class VoteRepository : IVoteRepository
	{
		private readonly PlanningPokerDbContext _context;

		public VoteRepository(PlanningPokerDbContext context)
		{
			_context = context;
		}

		public async Task<bool> SubmitVoteAsync(Vote vote)
		{
			var existingVote = await _context.Votes
				.FirstOrDefaultAsync(v => v.StoryId == vote.StoryId && v.ParticipantId == vote.ParticipantId);

			if (existingVote != null)
			{
				existingVote.Value = vote.Value;
				existingVote.SubmittedAt = DateTime.UtcNow;
			}
			else
			{
				_context.Votes.Add(vote);
			}

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Vote>> GetVotesByStoryAsync(Guid storyId)
		{
			return await _context.Votes
				.Include(v => v.Participant)
				.Where(v => v.StoryId == storyId)
				.ToListAsync();
		}

		public async Task<double> GetAverageVoteForStoryAsync(Guid storyId)
		{
			var votes = await _context.Votes
				.Where(v => v.StoryId == storyId)
				.Select(v => v.Value)
				.ToListAsync();

			return votes.Any() ? votes.Average() : 0;
		}

		public async Task<double> GetMedianVoteForStoryAsync(Guid storyId)
		{
			var votes = await _context.Votes
				.Where(v => v.StoryId == storyId)
				.Select(v => v.Value)
				.OrderBy(v => v)
				.ToListAsync();

			int count = votes.Count;

			if (count == 0)
				return 0;

			if (count % 2 == 0)
				return (votes[count / 2 - 1] + votes[count / 2]) / 2.0;
			else
				return votes[count / 2];
		}

		public async Task<int> GetTotalVotesForStoryAsync(Guid storyId)
		{
			return await _context.Votes
				.Where(v => v.StoryId == storyId)
				.CountAsync();
		}
	}
}
