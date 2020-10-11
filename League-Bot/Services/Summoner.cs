using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiotSharp;
using RiotSharp.Misc;

namespace League_Bot.Services
{
    public static class Summoner
    {
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("riot-api-key");
        private static readonly RiotApi Api = RiotApi.GetDevelopmentInstance(ApiKey);

        public static async Task<RiotSharp.Endpoints.SummonerEndpoint.Summoner> GetSummonerByName(Region region, string summonerName)
        {
            var summoner = await Api.Summoner.GetSummonerByNameAsync(region, summonerName);
            return summoner;
        }

        public static async Task<List<RiotSharp.Endpoints.ChampionMasteryEndpoint.ChampionMastery>> GetMasteries(Region region, RiotSharp.Endpoints.SummonerEndpoint.Summoner summoner, int limit = 5, bool descending = true)
        {
            var masteries = await Api.ChampionMastery.GetChampionMasteriesAsync(region, summoner.Id);
            masteries = descending ? masteries.OrderByDescending(x => x.ChampionPoints).ToList() : masteries.OrderBy(x => x.ChampionPoints).ToList();
            masteries = masteries.Take(limit).ToList();

            return masteries;
        }
    }
}
