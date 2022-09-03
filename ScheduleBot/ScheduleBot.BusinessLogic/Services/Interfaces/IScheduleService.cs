using AutoMapper;
using ScheduleBot.Common.Dto;

namespace ScheduleBot.BusinessLogic.Services.Interfaces
{
	public interface IScheduleService
	{
		List<VisitingTimeDto> Get(int id);
        ScheduleDto Get(string Namefacility);
		IMapper Mapper { get; set; }
	}
}

