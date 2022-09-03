using AutoMapper;
using Parser;
using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Common.Dto;
using ScheduleBot.Model.Data;

namespace ScheduleBot.BusinessLogic.Services.Implementations
{
	public class ScheduleService: IScheduleService
	{
		private ApplicationContext _context;
		public IMapper Mapper { get; set; }

		public ScheduleService(ApplicationContext context)
		{
			_context = context;
		}

		public List<VisitingTimeDto> Get(int id)
        {
			var query = _context.VisitingTimes.Where(x => x.FacilityId == id);

			var visitingTime = query.ToList();

			var visitingTimeDto = Mapper.Map<List<VisitingTimeDto>>(visitingTime);

			return visitingTimeDto;
        }

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

