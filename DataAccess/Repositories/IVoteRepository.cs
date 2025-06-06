using DatabaseAccess.Entity;

namespace DatabaseAccess.Repositories
{
	public interface IVoteRepository
	{
		Task<bool> SubmitVoteAsync(Vote vote);
		Task<IEnumerable<Vote>> GetVotesByStoryAsync(Guid storyId);
		Task<double> GetAverageVoteForStoryAsync(Guid storyId);
		Task<double> GetMedianVoteForStoryAsync(Guid storyId);
		Task<int> GetTotalVotesForStoryAsync(Guid storyId);
	}
}
