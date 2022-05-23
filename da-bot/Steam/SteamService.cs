using da_bot.Steam.Models;
using Newtonsoft.Json;

namespace da_bot.Steam
{
    public class SteamService
    {
        private readonly string apiKey;

        public SteamService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<Player?> GetPlayerInfo(string playerId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0001/?key={apiKey}&steamids={playerId}");
                var message = await response.Content.ReadAsStringAsync();
                var playerInfo = JsonConvert.DeserializeObject<PlayerResponse>(message);
                return playerInfo.Response?.Players?.Player?.FirstOrDefault();
            }
        }
    }
}
