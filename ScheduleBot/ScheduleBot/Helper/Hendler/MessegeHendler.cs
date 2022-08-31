using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Common.Schedule;
using ScheduleBot.Model.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ScheduleBot.Helper.Hendler
{
	public class MessegeHendler
	{
        private ReplyKeyboardMarkup _mainKeyboard { get; }
        private IScheduleService _scheduleService;

        public MessegeHendler(IScheduleService scheduleService)
		{
            _scheduleService = scheduleService;
             _mainKeyboard =  new(new[]
                    { new KeyboardButton[] { "🧊 Ледовая арена", "🏊‍♀️ Бассейн" },
                new KeyboardButton[] { "🏃‍♀️ Стадион", "🏋️ Тренажёрный зал" } })
             { ResizeKeyboard = true};

        }
        public async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "hi",replyMarkup: _mainKeyboard);
                return;
            }
            if (message.Text == "🧊 Ледовая арена")
            {
                var visitingTimes =  _scheduleService.Get(FacilityID.IceArena);
            }
            if (message.Text == "🏊‍♀️ Бассейн")
            {
                var visitingTimes = _scheduleService.Get(FacilityID.SwimmingPool);

            }
            if (message.Text == "🏃‍♀️ Стадион")
            {
                var visitingTimes = _scheduleService.Get(FacilityID.Stadium);

            }
            if (message.Text == "🏋️ Тренажёрный зал")
            {
                var visitingTimes = _scheduleService.Get(FacilityID.Gym);

            }
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Команда: " + message.Text + "не найдена");
        }
    }
}

