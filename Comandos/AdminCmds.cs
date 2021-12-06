using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Linq;

namespace Discord.app.AdminCommands
{
    public class AdminCommands : ModuleBase
    {
        [Command("teste")]
        public async Task AdminCommand()
        {
            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "ADMINISTRADOR");

            if (User.Roles.Contains(role))
            {
                var sb = new StringBuilder();

                // get user info from the Context

                // build out the reply
                sb.AppendLine($"Seu nick é -> " + User);
                sb.AppendLine("E você é administrador");

                // send simple string reply
                await ReplyAsync(sb.ToString());
            }
            else
            {
                var sb = new StringBuilder();

                // get user info from the Context

                // build out the reply
                sb.AppendLine($"Seu nick é -> " + User);
                sb.AppendLine("E você não é administrador");

                // send simple string reply
                await ReplyAsync(sb.ToString());
            }
            // initialize empty string builder for reply

        }
    }
}

