using DatabaseAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Entity
{
	public class Story
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Guid SessionId { get; set; }
		public StoryStatus Status { get; set; }

		public virtual Session Session { get; set; }
		public virtual ICollection<Vote> Votes { get; set; }

		public Story()
		{
			Id = Guid.NewGuid();
			Status = StoryStatus.NotStarted;
			Votes = new List<Vote>();
		}
	}
}
