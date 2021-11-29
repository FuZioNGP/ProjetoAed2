using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Discord.app.AdminCommands
{
    [Group("ADMINISTRADOR")]
    public class AdminCommands : ModuleBase
    {
        [Command("teste")]
        public async Task AdminCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // get user info from the Context
            var user = Context.User;

            // build out the reply
            sb.AppendLine($"Seu nick é -> " + user);
            sb.AppendLine("E você é administrador");

            // send simple string reply
            await ReplyAsync(sb.ToString());
        }
    }
}

