using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Discord.app.Comandos
{
    // for commands to be available, and have the Context passed to them, we must inherit ModuleBase
    public class ExampleCommands : ModuleBase
    {
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
            => ReplyAsync(echo);

        [Command("hello")]
        public async Task HelloCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // get user info from the Context
            var user = Context.User;

            // build out the reply
            sb.AppendLine($"Seu nick é -> " + user);
            sb.AppendLine("Seja muito bem vindo!");

            // send simple string reply
            await ReplyAsync(sb.ToString());
        }

        [Command("perguntar")]
        [Alias("ask")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task AskEightBall([Remainder] string args = null)
        {
            // I like using StringBuilder to build out the reply
            var sb = new StringBuilder();
            // let's use an embed for this one!
            var embed = new EmbedBuilder();

            // now to create a list of possible replies
            var replies = new List<string>();

            // add our possible replies
            replies.Add("sim");
            replies.Add("não");
            replies.Add("obvio");
            replies.Add("talvez....");

            // time to add some options to the embed (like color and title)
            embed.WithColor(new Color(0, 255, 0));
            embed.Title = "Bem vindo ao Pergunte para o BOT!";

            // we can get lots of information from the Context that is passed into the commands
            // here I'm setting up the preface with the user's name and a comma
            sb.AppendLine($"");
            sb.AppendLine();

            // let's make sure the supplied question isn't null 
            if (args == null)
            {
                // if no question is asked (args are null), reply with the below text
                sb.AppendLine("Desculpe mas você não fez uma pergunta!");
            }
            else
            {
                // if we have a question, let's give an answer!
                // get a random number to index our list with (arrays start at zero so we subtract 1 from the count)
                var answer = replies[new Random().Next(replies.Count - 1)];

                // build out our reply with the handy StringBuilder
                sb.AppendLine($"Você perguntou: " + args);
                sb.AppendLine();
                sb.AppendLine($"resposta: "+ answer);

                // bonus - let's switch out the reply and change the color based on it
                switch (answer)
                {
                    case "sim":
                        {
                            embed.WithColor(new Color(0, 255, 0));
                            break;
                        }
                    case "não":
                        {
                            embed.WithColor(new Color(255, 0, 0));
                            break;
                        }
                    case "obvio":
                        {
                            embed.WithColor(new Color(255, 255, 0));
                            break;
                        }
                    case "talvez....":
                        {
                            embed.WithColor(new Color(255, 0, 255));
                            break;
                        }
                }
            }

            // now we can assign the description of the embed to the contents of the StringBuilder we created
            embed.Description = sb.ToString();

            // this will reply with the embed
            await ReplyAsync(null, false, embed.Build());
        }
    }
}