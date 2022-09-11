namespace ScheduleBot.Common.Schedule
{
    public static class Options
    {
        public static readonly string[] Days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

        public static readonly Dictionary<string, string> IconTime = new Dictionary<string, string>()
            {
                { "08","🕣"},
                { "10","🕙"},
                { "12","🕛"},
                { "13","🕜"},
                { "15","🕒"},
                { "16","🕔"},
                { "17","🕔"},
                { "19","🕢"},
            };
    }
}

