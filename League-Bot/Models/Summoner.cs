using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RiotSharp;
using RiotSharp.Misc;

namespace League_Bot.Models
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
    }
}
