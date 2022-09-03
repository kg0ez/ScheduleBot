using System;
using Microsoft.EntityFrameworkCore;
using ScheduleBot.Model.Models;

namespace ScheduleBot.Model.Data
{
	public class ApplicationContext:DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Facility> Facilities  { get; set; } = null!;
		public DbSet<VisitingTime> VisitingTimes  { get; set; } = null!;
	}
}

