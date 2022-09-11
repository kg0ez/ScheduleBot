using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.Helper.Handler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ScheduleBot.Controllers
{
	public class BotController
	{
        private MessegeHandler _messageHendler;

		public BotController(IScheduleService scheduleService)
		{
            _messageHendler = new MessegeHandler(scheduleService);
		}

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

