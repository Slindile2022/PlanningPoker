namespace DatabaseAccess.Entity
{
	public class Session
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsActive { get; set; }
		public string ModeratorToken { get; set; }

		public virtual ICollection<Story> Stories { get; set; }
		public virtual ICollection<Participant> Participants { get; set; }

		public Session()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow.AddHours(2);
			IsActive = true;
			ModeratorToken = Guid.NewGuid().ToString();
			Stories = new List<Story>();
			Participants = new List<Participant>();
		}
	}
}
