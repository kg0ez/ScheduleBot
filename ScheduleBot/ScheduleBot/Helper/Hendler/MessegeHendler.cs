using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Common.Schedule;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ScheduleBot.Helper.Hendler
{
	public class MessegeHendler
	{
        private ReplyKeyboardMarkup _mainKeyboard { get; }
        private ReplyKeyboardMarkup _backKeyboard { get; }
        private IScheduleService _scheduleService;

        private string[] _days = { "Понедельник", "Вторник", "Среда","Четверг","Пятница","Суббота"};
        private Dictionary<string, string> _iconTime = new Dictionary<string, string>()
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

        public MessegeHendler(IScheduleService scheduleService)
		{
            _scheduleService = scheduleService;
             _mainKeyboard =  new(new[]
                    { new KeyboardButton[] { "🧊 Ледовая арена", "🏊‍♀️ Бассейн" },
                new KeyboardButton[] { "🏋️ Тренажёрный зал", "🏋️ ТЗ (Гребная база)" } ,
                new KeyboardButton[] { "🏃‍♀️ Стадион","🤽 Тренировочное поле" }})
             { ResizeKeyboard = true};

            _backKeyboard = new(
                    new KeyboardButton[] { "🔙 Назад" })
            { ResizeKeyboard = true };

        }
        public async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start" || message.Text == "🔙 Назад")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите объект",replyMarkup: _mainKeyboard);
                return;
            }
            if (message.Text == "🧊 Ледовая арена")
            {
                string schedule = ShowShedul(FacilityName.IceArena);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏊‍♀️ Бассейн")
            {
                string schedule = ShowShedul(FacilityName.SwimmingPool);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏃‍♀️ Стадион")
            {
                string schedule = ShowShedul(FacilityName.Stadium);

                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏋️ Тренажёрный зал")
            {
                string schedule = ShowShedul("Тренажерный зал (УСЗ №21,22)");
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏋️ ТЗ (Гребная база)")
            {
                string schedule = ShowShedul("Тренажерный зал (Гребная база №1)");
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🤽 Тренировочное поле")
            {
                string schedule = ShowShedul(FacilityName.TraningField);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Команда: " + message.Text + " не найдена");
        }
        private string ShowShedul(string facility)
        {
            var obj = _scheduleService.Get(facility);

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
                        schedule += $"<b>{_days[index]}</b>" + Environment.NewLine;
                        isSchedule = false;
                    }

                    var hour = day.Substring(0,2);

                    foreach (var icon in _iconTime)
                        if (icon.Key == hour)
                            hour = icon.Value;

                    schedule += $"<i>{hour} {day}</i>" + Environment.NewLine;
                }

                if(!isSchedule)
                    schedule += Environment.NewLine;
                index++;
            }
            return schedule;
        }
    }
}

