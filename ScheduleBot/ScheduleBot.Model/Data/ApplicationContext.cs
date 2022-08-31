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

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server=localhost;Database=Schedule;User Id=sa;Password=Valuetech@123;");
		//}

		public DbSet<Facility> Facilities  { get; set; } = null!;
		public DbSet<VisitingTime> VisitingTimes  { get; set; } = null!;
	}
}

