using System;
using AutoMapper;
using ScheduleBot.Common.Dto;
using ScheduleBot.Model.Models;

namespace ScheduleBot.BusinessLogic.Helper.Mapper
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<Facility, FacilityDto>().ReverseMap();
			CreateMap<VisitingTime, VisitingTimeDto>().ReverseMap();
		}
	}
}

