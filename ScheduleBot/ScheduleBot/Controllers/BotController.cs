using System;
using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Helper.Hendler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ScheduleBot.Controllers
{
	public class BotController
	{
		public BotController(IScheduleService scheduleService)
		{
            _messageHendler = new MessegeHendler(scheduleService);
		}

        private MessegeHendler _messageHendler;

        public async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Type == UpdateType.Message && update?.Message?.Text != null)
                {
                    await _messageHendler.HandleMessage(botClient, update.Message);
                    return;
                }
            }
            catch { }
        }
    }
}

