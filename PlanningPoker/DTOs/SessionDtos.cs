namespace PlanningPoker.DTOs
{
	public class SessionCreateDto
	{
		public string Name { get; set; }
	}

	public class SessionDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public string ModeratorToken { get; set; }
	}
}
