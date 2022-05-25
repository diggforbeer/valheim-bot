// See https://aka.ms/new-console-template for more information
using da_bot.Services;
using Microsoft.Extensions.Configuration;
using System.Collections;


var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var config = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables()
            .Build();

Console.WriteLine("GetEnvironmentVariables: ");
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
    Console.WriteLine("  {0} = {1}", de.Key, de.Value);

Console.WriteLine("Hello, World!");

var directory = new DirectoryInfo(config["logFolder"]);

var latestFile = directory.GetFiles("valheim-server-stdout*.log").OrderByDescending(x => x.LastWriteTime).First();
Console.WriteLine($"latest log file is {latestFile.FullName}");

var service = new ValheimEventDetectorService(config);
service.Start(latestFile);


Console.WriteLine("Press any key to exit");
Console.ReadLine();
