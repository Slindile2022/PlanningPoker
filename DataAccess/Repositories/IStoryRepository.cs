using DatabaseAccess.Entity;
using DatabaseAccess.Enums;

namespace DatabaseAccess.Repositories
{
	public interface IStoryRepository
	{
		Task<IEnumerable<Story>> GetStoriesBySessionAsync(Guid sessionId);
		Task<Story?> GetStoryByIdAsync(Guid id, bool includeVotes = false);
		Task<Story> CreateStoryAsync(Story story);
		Task<bool> UpdateStoryStatusAsync(Guid id, StoryStatus status, string moderatorToken);
	}
}
