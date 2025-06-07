using DatabaseAccess.Enums;

namespace PlanningPoker.DTOs
{
	public class StoryCreateDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public Guid SessionId { get; set; }
		public string ModeratorToken { get; set; }
	}

	public class StoryDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Guid SessionId { get; set; }
		public StoryStatus Status { get; set; }
	}

	public class StoryStatusUpdateDto
	{
		public StoryStatus Status { get; set; }
		public string ModeratorToken { get; set; }
	}
}
