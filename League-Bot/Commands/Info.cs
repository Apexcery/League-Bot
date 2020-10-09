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
            var prefix = "!l ";

            var embed = new EmbedBuilder()
                .WithTitle("Usage")
                .AddField("Changelog", $"{prefix}changelog\nView the most recent changes to this bot.")
                .WithColor(Color.Blue)
                .Build();

            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}
