using da_bot.Steam.Models;
using Newtonsoft.Json;

namespace da_bot.Steam
{
    public class SteamService
    {
        private readonly string _apiKey;
        private static List<Player> _cachedPlayers = new List<Player>();

        public SteamService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<Player?> GetPlayerInfo(string playerId)
        {

            var cachedPlayer = _cachedPlayers.FirstOrDefault(x => x.Steamid == playerId);
            if (cachedPlayer != null)
                return cachedPlayer;


            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0001/?key={_apiKey}&steamids={playerId}");
                var message = await response.Content.ReadAsStringAsync();
                var playerInfo = JsonConvert.DeserializeObject<PlayerResponse>(message);
                var currentPlayer = playerInfo.Response?.Players?.Player?.FirstOrDefault();
                
                //Add to static if its not null so the cache can retreive it.
                if ( currentPlayer!= null)
                    _cachedPlayers.Add(currentPlayer);
                return currentPlayer;
            }
        }
    }
}
