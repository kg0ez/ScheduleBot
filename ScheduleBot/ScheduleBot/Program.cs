using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScheduleBot.BusinessLogic.Helper.Mapper;
using ScheduleBot.BusinessLogic.Services.Implementations;
using ScheduleBot.BusinessLogic.Services.Interfaces;
using ScheduleBot.BusinessLogic.Telegram.Services.Implementations;
using ScheduleBot.BusinessLogic.Telegram.Services.Interfaces;
using ScheduleBot.Controllers;
using ScheduleBot.Model.Data;
using Telegram.Bot;
using Telegram.Bot.Polling;

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddSingleton<IScheduleService, ScheduleService>()
    .AddSingleton<IErrorService, ErrorService>()
    .AddDbContext<ApplicationContext>(opt => opt.UseSqlServer("Server=localhost;Database=Schedule;User Id=sa;Password=Valuetech@123;"
            , x => x.MigrationsAssembly("ScheduleBot")))
    .BuildServiceProvider();

var mapperConfiguration = new MapperConfiguration(x =>
{
    x.AddProfile<MappingProfile>();
});

mapperConfiguration.AssertConfigurationIsValid();
IMapper mapper = mapperConfiguration.CreateMapper();

var errorService = serviceProvider.GetService<IErrorService>();
var scheduleService = serviceProvider.GetService<IScheduleService>();

scheduleService.Mapper = mapper;

var botController = new BotController(scheduleService);

var botClient = new TelegramBotClient("5486041835:AAEbtji4oRTHwDnH6SYd22gveg30MHN3Z-Q");

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