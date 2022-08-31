using Telegram.Bot;

namespace ScheduleBot.BusinessLogic.Telegram.Services.Interfaces
{
	public interface IErrorService
	{
		Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken);
	}
}

