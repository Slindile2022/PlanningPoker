using DatabaseAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DatabaseContext
{
	public class PlanningPokerDbContext : DbContext
	{
		public PlanningPokerDbContext(DbContextOptions<PlanningPokerDbContext> options)
			: base(options)
		{
		}

		public DbSet<Session> Sessions { get; set; }
		public DbSet<Participant> Participants { get; set; }
		public DbSet<Story> Stories { get; set; }
		public DbSet<Vote> Votes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure relationships and constraints
			modelBuilder.Entity<Participant>()
				.HasOne(p => p.Session)
				.WithMany(s => s.Participants)
				.HasForeignKey(p => p.SessionId);

			modelBuilder.Entity<Story>()
				.HasOne(s => s.Session)
				.WithMany(s => s.Stories)
				.HasForeignKey(s => s.SessionId);

			modelBuilder.Entity<Vote>()
				.HasOne(v => v.Story)
				.WithMany(s => s.Votes)
				.HasForeignKey(v => v.StoryId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Vote>()
				.HasOne(v => v.Participant)
				.WithMany()
				.HasForeignKey(v => v.ParticipantId);
		}
	}
}
