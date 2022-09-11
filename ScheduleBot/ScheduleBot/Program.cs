using Microsoft.Extensions.DependencyInjection;
using ScheduleBot.BusinessLogic.Services.Implementations;
using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.BusinessLogic.Telegram.Services.Implementations;
using ScheduleBot.BusinessLogic.Telegram.Services.Interfaces;
using ScheduleBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Polling;

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddSingleton<IScheduleService, ScheduleService>()
    .AddSingleton<IErrorService, ErrorService>()
    .BuildServiceProvider();

var errorService = serviceProvider.GetService<IErrorService>();
var scheduleService = serviceProvider.GetService<IScheduleService>();

var botController = new BotController(scheduleService);

var botClient = new TelegramBotClient("5726356653:AAGXjHVNFVUpenZ_lbklREZv4JagXJ2K2ek");
//var botClient = new TelegramBotClient("5486041835:AAEbtji4oRTHwDnH6SYd22gveg30MHN3Z-Q");

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};

botClient.StartReceiving(
    botController.HandleUpdatesAsync,
    errorService.HandleError,
    receiverOptions,
    cancellationToken: cts.Token);

Console.ReadLine();
cts.Cancel();