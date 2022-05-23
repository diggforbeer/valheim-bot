using JNogueira.Discord.Webhook.Client;


namespace da_bot.Discord
{
    internal class DiscordService
    {
        public readonly DiscordWebhookClient client;

        public DiscordService(string webhookUrl)
        {
            client = new DiscordWebhookClient(webhookUrl);
        }

        public async void PostMessage(string text)
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
