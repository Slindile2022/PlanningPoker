using DatabaseAccess.DatabaseContext;
using DatabaseAccess.Entity;
using DatabaseAccess.Enums;
using DatabaseAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.Implementations
{
	public class StoryRepository : IStoryRepository
	{
		private readonly PlanningPokerDbContext _context;

		public StoryRepository(PlanningPokerDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Story>> GetStoriesBySessionAsync(Guid sessionId)
		{
			return await _context.Stories
				.Where(s => s.SessionId == sessionId)
				.ToListAsync();
		}

		public async Task<Story?> GetStoryByIdAsync(Guid id, bool includeVotes = false)
		{
			IQueryable<Story> query = _context.Stories;

			if (includeVotes)
			{
				query = query.Include(s => s.Votes);
			}

			return await query.FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<Story> CreateStoryAsync(Story story)
		{
			_context.Stories.Add(story);
			await _context.SaveChangesAsync();
			return story;
		}

		public async Task<bool> UpdateStoryStatusAsync(Guid id, StoryStatus status, string moderatorToken)
		{
			var story = await _context.Stories
				.Include(s => s.Session)
				.FirstOrDefaultAsync(s => s.Id == id);

			if (story == null || story.Session.ModeratorToken != moderatorToken)
				return false;

			story.Status = status;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
