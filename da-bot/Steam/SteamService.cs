﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using da_bot.Steam.Models;
using Newtonsoft.Json;

namespace da_bot.Steam
{
    internal static class SteamService
    {
        public static async Task<Player?> GetPlayerInfo(string playerId)
        {
            using (var client = new HttpClient())
            {
                var apiKey = "";
                var response = await client.GetAsync($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0001/?key={apiKey}&steamids={playerId}");
                var message = await response.Content.ReadAsStringAsync();
                var playerInfo = JsonConvert.DeserializeObject<PlayerResponse>(message);
                return playerInfo.Response?.Players?.Player?.FirstOrDefault();
            }
        }
    }
}
