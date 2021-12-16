using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Linq;

namespace Discord.app.AdminCommands
{
    public class AdminCommands : ModuleBase<SocketCommandContext>
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
        [Command("ban")]
        public async Task banirUser(SocketUser name, [Remainder] string rz = "não específicado")
        {
            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "ADMINISTRADOR");

            if (User.Roles.Contains(role))
            {
                try
                {

                    var user = Context.Guild.GetUser(name.Id);

                    EmbedBuilder builder = new EmbedBuilder();

                    if (name.IsBot)
                    {
                        await Context.Message.DeleteAsync();
                        EmbedBuilder bd = new EmbedBuilder();
                        bd.WithColor(Color.Red);
                        bd.WithDescription($"{Context.User.Mention},:x: ***Erro***: Você não pode banir bots. :smile: ");
                        await ReplyAsync("", false, bd.Build());
                    }
                    else
                    {


                        builder.WithTitle("Mensagem do Servidor");
                        builder.WithColor(Color.Red);
                        builder.WithDescription("Você foi banido violando as regras do servidor, caso haja algum engano por favor relatar aos staffs do servidor. \n" +
                            $"***Motivo do banimento*** ```{rz}```");


                        // var amount = Context.Guild.GetUser(name.Id);
                        // var messages = await this.Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();
                        // await this.Context.Channel.DeleteMessagesAsync(messages);
                        await user.SendMessageAsync("", false, builder.Build());
                        await Context.Guild.AddBanAsync(user);
                        await Context.Message.DeleteAsync();
                        const int delay = 5000;
                        var m = await this.ReplyAsync($"{Context.User.Mention}, :white_check_mark:  {Context.User.Mention} usuário banido com sucesso!  ");
                        await Task.Delay(delay);
                        await m.DeleteAsync();

                        var canalTribunal = Context.Guild.GetTextChannel(912747154966196284);
                        builder.WithAuthor($"Banido por : {Context.Message.Author}");
                        builder.WithTitle($":x: {user.Username} foi banido!");
                        builder.WithColor(139, 0, 139);
                        builder.WithThumbnailUrl($"{user.GetAvatarUrl(size: 2048)}");
                        builder.WithDescription($"***Motivo***``` {rz} ```\n" +
                            $"***ID*** : ```{user.Id}``` ");

                        await canalTribunal.SendMessageAsync("", false, builder.Build());
                    }
                }
                catch (Exception ex)
                {
                    EmbedBuilder builder = new EmbedBuilder();
                    builder.WithColor(Color.Red);
                    builder.WithDescription($"{Context.User.Mention},:x: Houve um erro ao encontar este usuário, no caso tente procura-lo por ID. :smile: ");
                    await Context.Message.DeleteAsync();
                    const int delay = 5000;
                    var m = await this.ReplyAsync("", false, builder.Build());
                    await Task.Delay(delay);
                    await m.DeleteAsync();


                }
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{User.Mention} você não é administrado e não pode executar este comando!r");
                await ReplyAsync(sb.ToString());
            }
        }
    }
}

