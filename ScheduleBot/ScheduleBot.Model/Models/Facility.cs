using System;
namespace ScheduleBot.Model.Models
{
	public class Facility
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public List<VisitingTime> VisitingTimes { get; set; }
	}
}

