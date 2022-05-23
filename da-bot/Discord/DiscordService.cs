using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JNogueira.Discord.Webhook.Client;


namespace da_bot.Discord
{
    internal static class DiscordService
    {
        public async static void PostMessage(string text)
        {
            var client = new DiscordWebhookClient("");

            // Create your DiscordMessage with all parameters of your message.
            var message = new DiscordMessage($"{text}",
                username: "valheim-bot",
                tts: false);

            // Send the message!
            await client.SendToDiscord(message);
        }
    }
}
