using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entity
{
	public class Participant
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid SessionId { get; set; }
		public bool IsActive { get; set; }
		public DateTime JoinedAt { get; set; }

		public virtual Session Session { get; set; }

		public Participant()
		{
			Id = Guid.NewGuid();
			JoinedAt = DateTime.UtcNow;
			IsActive = true;
		}
	}
}
