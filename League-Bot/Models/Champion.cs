using System.Collections.Generic;
using RiotSharp.Endpoints.StaticDataEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion.Enums;

namespace League_Bot.Models
{
    public class Champion
    {
        public List<string> AllyTips { get; set; }

        public string Blurb { get; set; }

        public List<string> EnemyTips { get; set; }

        public int Id { get; set; }

        public ImageStatic Image { get; set; }

        public InfoStatic Info { get; set; }

        public string Key { get; set; }

        public string Lore { get; set; }

        public string Name { get; set; }

        public string Partype { get; set; }

        public PassiveStatic Passive { get; set; }

        public List<RecommendedStatic> RecommendedItems { get; set; }

        public List<SkinStatic> Skins { get; set; }

        public List<ChampionSpellStatic> Spells { get; set; }

        public ChampionStatsStatic Stats { get; set; }

        public List<TagStatic> Tags { get; set; }

        public string Title { get; set; }
    }
}