using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da_bot
{
    public interface ISettings
    {
        string DiscordWebHook { get; set; }
        string SteamApiKey { get; set; }
        string LogFolder { get; set; }
    }

    public class Settings : ISettings
    {
        public string DiscordWebHook { get; set; }
        public string SteamApiKey { get; set; }
        public string LogFolder { get; set; }

        public Settings()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            .AddEnvironmentVariables()
            .Build();

            DiscordWebHook = config["discordWebHook"];
            SteamApiKey = config["steamApiKey"];
            LogFolder = config["logFolder"];

        }
    }
}
