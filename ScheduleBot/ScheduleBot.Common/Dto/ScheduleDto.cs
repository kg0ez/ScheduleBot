using System;
namespace ScheduleBot.Common.Dto
{
    public class ScheduleDto
    {
        public string NameFacility { get; set; } = null!;
        public List<List<string>> Schedule { get; set; }
    }
}

