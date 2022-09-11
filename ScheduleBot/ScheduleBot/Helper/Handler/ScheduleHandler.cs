using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Common.Schedule;

namespace ScheduleBot.Helper.Handler
{
    public static class ScheduleHandler
    {
        public static string Show(string facility, IScheduleService scheduleService)
        {
            var obj = scheduleService.Get(facility);

            if (obj == null)
                return "Cамоотработки нет";

            if (obj.NameFacility.Contains("Тренажерный зал (Гребная база №1)")
                && facility == "Тренажерный зал")
                return "Cамоотработки нет";

            string schedule = $"<b>{facility}</b>" + Environment.NewLine;
            schedule += Environment.NewLine;
            int index = 0;
            foreach (var days in obj.Schedule)
            {
                var isSchedule = true;
                foreach (var day in days)
                {
                    if (days.Count == 1 && day.Length == 1)
                        break;

                    if (isSchedule)
                    {
                        schedule += $"<b>{Options.Days[index]}</b>" + Environment.NewLine;
                        isSchedule = false;
                    }

                    var hour = day.Substring(0, 2);

                    foreach (var icon in Options.IconTime)
                        if (icon.Key == hour)
                            hour = icon.Value;

                    schedule += $"<i>{hour} {day}</i>" + Environment.NewLine;
                }

                if (!isSchedule)
                    schedule += Environment.NewLine;
                index++;
            }
            return schedule;
        }
    }
}

