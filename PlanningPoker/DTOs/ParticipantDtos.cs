namespace PlanningPoker.DTOs
{
	public class ParticipantCreateDto
	{
		public string Name { get; set; }
		public Guid SessionId { get; set; }
	}

	public class ParticipantDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid SessionId { get; set; }
		public DateTime JoinedAt { get; set; }
	}
}
