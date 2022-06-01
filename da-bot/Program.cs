// See https://aka.ms/new-console-template for more information
using da_bot;
using da_bot.Discord;
using da_bot.Services;
using da_bot.Steam;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections;


var host = CreateHostBuilder(args).Build();

var settings = host.Services.GetService<ISettings>();

if (settings == null)
    throw new Exception("Settings cannnot be null");

Console.WriteLine("GetEnvironmentVariables: ");
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
    Console.WriteLine("  {0} = {1}", de.Key, de.Value);

var directory = new DirectoryInfo(settings.LogFolder);

var latestFile = directory.GetFiles("valheim-server-stdout*.log").OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
if (latestFile == null)
    throw new Exception("LatestFile cannnot be null");

Console.WriteLine($"latest log file is {latestFile.FullName}");

var valheimService = host.Services.GetService<IValheimEventDetectorService>();

if (valheimService == null)
    throw new Exception("ValheimService cannnot be null");
valheimService.Start(latestFile);


Console.WriteLine($"Exiting - was watching file {latestFile.FullName}");
Console.ReadLine();


static IHostBuilder CreateHostBuilder(string[] args)
{
    var settings = new Settings();

    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            services.AddSingleton<IDiscordService>(x => new DiscordService(settings.DiscordWebHook));
            services.AddSingleton<ISteamService>(x => new SteamService(settings.SteamApiKey));
            services.AddSingleton<ISettings, Settings>();
            services.AddSingleton<IValheimEventDetectorService, ValheimEventDetectorService>();
        });

    return hostBuilder;
}