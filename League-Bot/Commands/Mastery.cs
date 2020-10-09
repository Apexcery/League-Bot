using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using RiotSharp;
using RiotSharp.Misc;

namespace League_Bot.Commands
{
    public class Mastery : ModuleBase<SocketCommandContext>
    {
        private const string ApiKey = @""; //Add Riot API-Key here.
        private readonly RiotApi _api = RiotApi.GetDevelopmentInstance(ApiKey);

        [Command("mastery")]
        public async Task CommandMastery(string summonerName)
        {
            var client = new HttpClient();
            var dataDragonVersions =
                JsonConvert.DeserializeObject<List<string>>(
                    await (await client.GetAsync("https://ddragon.leagueoflegends.com/api/versions.json")).Content.ReadAsStringAsync());

            var latestDataDragonVersion = dataDragonVersions.First();

            var summoner = await _api.Summoner.GetSummonerByNameAsync(Region.Euw, summonerName);
            var top5Mastery = (await _api.ChampionMastery.GetChampionMasteriesAsync(Region.Euw, summoner.Id))
                .OrderByDescending(x => x.ChampionPoints)
                .Take(5);

            var champions = (await _api.StaticData.Champions.GetAllAsync(latestDataDragonVersion)).Champions.Values
                .Where(x => top5Mastery.Any(z => z.ChampionId == x.Id))
                .ToList();

            var topChampionImage = champions.FirstOrDefault(x => x.Id == top5Mastery.FirstOrDefault()?.ChampionId)?.Image;

            var embed = new EmbedBuilder()
                .WithTitle("Top 5 Champion Mastery's");

            if (topChampionImage != null)
            {
                var topChampionDataDragonImageUrl =
                    $"http://ddragon.leagueoflegends.com/cdn/{latestDataDragonVersion}/img/champion/{topChampionImage.Full}";
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
