using DatabaseAccess.DatabaseContext;
using DatabaseAccess.Implementations;
using DatabaseAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Services
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Register DbContext
			services.AddDbContext<PlanningPokerDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			// Register repositories
			services.AddScoped<ISessionRepository, SessionRepository>();
			services.AddScoped<IParticipantRepository, ParticipantRepository>();
			services.AddScoped<IStoryRepository, StoryRepository>();
			services.AddScoped<IVoteRepository, VoteRepository>();

			return services;
		}
	}
}
