using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using League_Bot.Models;
using League_Bot.Services;
using Newtonsoft.Json;
using RiotSharp;
using RiotSharp.Misc;
using Champion = League_Bot.Services.Champion;

namespace League_Bot.Commands
{
    public class Mastery : ModuleBase<SocketCommandContext>
    {
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("riot-api-key");
        private readonly RiotApi _api = RiotApi.GetDevelopmentInstance(ApiKey);

        [Command("mastery")]
        public async Task CommandMastery(string region, string summonerName)
        {
            Enum.TryParse(typeof(Region), region, true, out var regionEnum);
            if (regionEnum == null)
                throw new Exception("Region could not be parsed");
            
            var latestDataDragonVersion = await DataDragon.GetLatestVersion();

            var summoner = await _api.Summoner.GetSummonerByNameAsync((Region) regionEnum, summonerName);
            var top5Mastery = await Summoner.GetMasteries((Region) regionEnum, summoner);

            if (!top5Mastery.Any())
            {
                await ReplyAsync("No mastery details could be found.");
                return;
            }

            var champions = await Champion.GetAllChampions(latestDataDragonVersion);

            var embed = new EmbedBuilder()
                .WithTitle("Top 5 Champion Mastery's");

            var topChampionImage = champions.FirstOrDefault(x => x.Id == top5Mastery.FirstOrDefault()?.ChampionId)?.Image;
            if (topChampionImage != null)
            {
                var topChampionDataDragonImageUrl = Champion.GetImageUrl(latestDataDragonVersion, topChampionImage.Full);
                embed = embed.WithThumbnailUrl(topChampionDataDragonImageUrl);
            }

            foreach (var championMastery in top5Mastery)
            {
                var champion = champions.FirstOrDefault(x => x.Id == championMastery.ChampionId);
                if (champion == null)
                    break;

                embed = embed.AddField(champion.Name,$"{championMastery.ChampionPoints:n0} Points");
            }

            await ReplyAsync("", false, embed.Build());
        }
    }
}
