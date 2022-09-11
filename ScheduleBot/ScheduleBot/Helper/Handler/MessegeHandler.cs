using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Common.Schedule;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ScheduleBot.Helper.Handler
{
	public class MessegeHandler
	{
        private ReplyKeyboardMarkup _mainKeyboard { get; }
        private ReplyKeyboardMarkup _backKeyboard { get; }
        private IScheduleService _scheduleService;

        public MessegeHandler(IScheduleService scheduleService)
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
                string schedule = ScheduleHandler.Show(FacilityName.IceArena,_scheduleService);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏊‍♀️ Бассейн")
            {
                string schedule = ScheduleHandler.Show(FacilityName.SwimmingPool, _scheduleService);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏃‍♀️ Стадион")
            {
                string schedule = ScheduleHandler.Show(FacilityName.Stadium, _scheduleService);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏋️ Тренажёрный зал")
            {
                string schedule = ScheduleHandler.Show(FacilityName.Gym, _scheduleService);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🏋️ ТЗ (Гребная база)")
            {
                string schedule = ScheduleHandler.Show(FacilityName.GymBase, _scheduleService);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            if (message.Text == "🤽 Тренировочное поле")
            {
                string schedule = ScheduleHandler.Show(FacilityName.TraningField, _scheduleService);
                await botClient.SendTextMessageAsync(message.Chat.Id, schedule, Telegram.Bot.Types.Enums.ParseMode.Html, replyMarkup: _backKeyboard);
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Команда: " + message.Text + " не найдена");
        }
    }
}

