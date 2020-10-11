using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace League_Bot.Commands
{
    public class Info : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            var prefix = Environment.GetEnvironmentVariable("prefix");

            var embed = new EmbedBuilder()
                .WithTitle("Usage")
                .AddField("Mastery", $"{prefix}mastery <region> <Summoner Name>\nView the top 5 mastery's for the specified summoner.")
                .WithColor(Color.Blue)
                .Build();

            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}
