using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiotSharp;

namespace League_Bot.Services
{
    public static class Champion
    {
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("riot-api-key");
        private static readonly RiotApi Api = RiotApi.GetDevelopmentInstance(ApiKey);

        public static async Task<List<League_Bot.Models.Champion>> GetAllChampions(string version)
        {
            var champions = (await Api.StaticData.Champions.GetAllAsync(version)).Champions.Values.Select(x => new League_Bot.Models.Champion
            {
                Blurb = x.Blurb,
                Id = x.Id,
                Image = x.Image,
                Info = x.Info,
                Key = x.Key,
                Lore = x.Lore,
                Name = x.Name,
                Partype = x.Partype,
                Passive = x.Passive,
                Skins = x.Skins,
                Spells = x.Spells,
                Stats = x.Stats,
                Tags = x.Tags,
                Title = x.Title,
                AllyTips = x.AllyTips,
                EnemyTips = x.EnemyTips,
                RecommendedItems = x.RecommendedItems
            }).ToList();
            
            return champions;
        }

        public static string GetImageUrl(string version, string name)
        {
            var url = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/champion/{name}";

            return url;
        }
    }
}
