using JNogueira.Discord.Webhook.Client;


namespace da_bot.Discord
{
    public interface IDiscordService
    {
        Task PostMessage(string text);
    }


    internal class DiscordService : IDiscordService
    {
        public readonly DiscordWebhookClient client;

        public DiscordService(string webhookUrl)
        {
            client = new DiscordWebhookClient(webhookUrl);
        }

        public async Task PostMessage(string text)
        {
            // Create your DiscordMessage with all parameters of your message.
            var message = new DiscordMessage($"{text}",
                username: "valheim-bot",
                tts: false);

            // Send the message!
            await client.SendToDiscord(message);
        }
    }
}
