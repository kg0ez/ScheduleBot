using Parser;
using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Common.Dto;

namespace ScheduleBot.BusinessLogic.Services.Implementations
{
	public class ScheduleService: IScheduleService
	{
		public ScheduleDto Get(string Namefacility)
        {
			var schedule = ScheduleParser.GetShedule();
            foreach (var facility in schedule)
            {
                if (facility.NameFacility.Contains(Namefacility))
                {
					return facility;
                }
            }
			return null;
        }
	}
}

