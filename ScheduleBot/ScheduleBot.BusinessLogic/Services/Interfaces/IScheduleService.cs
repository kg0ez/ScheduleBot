using ScheduleBot.Common.Dto;

namespace ScheduleBot.BusinessLogic.Services.Interfaces
{
	public interface IScheduleService
	{
        ScheduleDto Get(string Namefacility);
	}
}

