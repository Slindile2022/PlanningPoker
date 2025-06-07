using DatabaseAccess.Enums;

namespace PlanningPoker.DTOs
{
	public class VoteSubmitDto
	{
		public Guid StoryId { get; set; }
		public Guid ParticipantId { get; set; }
		public int Value { get; set; }
	}

	public class VoteResultsDto
	{
		public Guid StoryId { get; set; }
		public int TotalVotes { get; set; }
		public double Average { get; set; }
		public double Median { get; set; }
		public StoryStatus Status { get; set; }
		public List<VoteDetailDto> Votes { get; set; } = new List<VoteDetailDto>();
	}

	public class VoteDetailDto
	{
		public Guid ParticipantId { get; set; }
		public string ParticipantName { get; set; }
		public int Value { get; set; }
	}
}
