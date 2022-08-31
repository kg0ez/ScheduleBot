using AutoMapper;
using ScheduleBot.Common.Dto;

namespace ScheduleBot.BusinessLogic.Services.Interfaces
{
	public interface IScheduleService
	{
		List<VisitingTimeDto> Get(int id);
		IMapper Mapper { get; set; }
	}
}

