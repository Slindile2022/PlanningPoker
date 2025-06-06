using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entity
{
	public class Vote
	{
		public Guid Id { get; set; }
		public Guid StoryId { get; set; }
		public Guid ParticipantId { get; set; }
		public int Value { get; set; }
		public DateTime SubmittedAt { get; set; }

		public virtual Story Story { get; set; }
		public virtual Participant Participant { get; set; }

		public Vote()
		{
			Id = Guid.NewGuid();
			SubmittedAt = DateTime.UtcNow;
		}
	}
}
