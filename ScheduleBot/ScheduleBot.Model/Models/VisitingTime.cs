using System;
namespace ScheduleBot.Model.Models
{
	public class VisitingTime
	{
		public int Id { get; set; }
		public string Time { get; set; }

		public int FacilityId { get; set; }
		public Facility Facility { get; set; }
	}
}

