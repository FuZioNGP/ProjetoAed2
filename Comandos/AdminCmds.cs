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
        [Command("ban")]
        public async Task banirUser(SocketGuildUser name, [Remainder] string rz = "não específicado")
        {
            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "ADMINISTRADOR");

            if (User.Roles.Contains(role))
            {
                try
                {

                    var userban = Context.Guild.GetUser(name.Id);

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
                        await userban.SendMessageAsync("", false, builder.Build());
                        Console.WriteLine(userban);
                        //await Context.Guild.AddBanAsync(userban);
                        await userban.BanAsync();
                        await Context.Message.DeleteAsync();
                        const int delay = 5000;
                        var m = await this.ReplyAsync($"{Context.User.Mention}, :white_check_mark:  {Context.User.Mention} usuário banido com sucesso!  ");
                        await Task.Delay(delay);
                        await m.DeleteAsync();

                        var canalTribunal = Context.Guild.GetTextChannel(921024159360905266);
                        builder.WithAuthor($"Banido por : {Context.Message.Author}");
                        builder.WithTitle($":x: {userban.Username} foi banido!");
                        builder.WithColor(139, 0, 139);
                        builder.WithThumbnailUrl($"{userban.GetAvatarUrl(size: 2048)}");
                        builder.WithDescription($"***Motivo***``` {rz} ```\n" +
                            $"***ID*** : ```{userban.Id}``` ");

                        await canalTribunal.SendMessageAsync("", false, builder.Build());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
                sb.AppendLine($"{User.Mention} você não é administrador e não pode executar este comando!");
                await ReplyAsync(sb.ToString());
            }
        }
        [Command("kick")]
        public async Task kickUsuario(SocketGuildUser username, [Remainder] string razao = "motivo não específicado")
        {
            var User = Context.User as SocketGuildUser;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "ADMINISTRADOR");

            if (User.Roles.Contains(role))
            {
                try
                {
                    if (razao.Equals(""))
                    {

                        razao = "Nenhuma razão específicada";

                    }

                    EmbedBuilder builder = new EmbedBuilder();

                    var usuario = Context.Guild.GetUser(username.Id);

                    builder.WithTitle("Mensagem do Servidor");
                    builder.WithColor(Color.Red);
                    builder.WithDescription("Você foi Expulso por violar as regras do servidor, caso não tenha violado entre em contato com um dos adm \n\n" +
                        $"Você foi expulso pela seguinte razão : ```{razao}```");



                    await usuario.SendMessageAsync("", false, builder.Build());

                    var canalPunicao = Context.Guild.GetTextChannel(921024159360905266);



                    builder.WithAuthor($"Expulso por : {Context.Message.Author}");
                    builder.WithTitle($":x: {usuario.Username} foi expulso!");
                    builder.WithColor(139, 0, 139);
                    builder.WithThumbnailUrl($"{usuario.GetAvatarUrl(size: 2048)}");
                    builder.WithDescription($"***Motivo***``` {razao} ```\n" +
                        $"***ID*** : ```{usuario.Id}``` ");

                    await canalPunicao.SendMessageAsync("", false, builder.Build());
                    await usuario.KickAsync();


                    await Context.Message.DeleteAsync();
                    const int delay = 5000;
                    var m = await this.ReplyAsync($"{Context.User.Mention}, Usuário foi expulso com sucesso!:white_check_mark: ");
                    await Task.Delay(delay);
                    await m.DeleteAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
                sb.AppendLine($"{User.Mention} você não é administrador e não pode executar este comando!");
                await ReplyAsync(sb.ToString());
            }
        }
        [Command("mutar")]
        public async Task mutar(SocketGuildUser name, [Remainder] string motivo = "nenhum motivo")
        {
            var User = Context.User as SocketGuildUser;
            var roleadm = Context.Guild.Roles.FirstOrDefault(x => x.Name == "ADMINISTRADOR");

            if (User.Roles.Contains(roleadm))
            {

                try
                {
                    var username = Context.Guild.GetUser(name.Id);
                    //var username = await Context.Guild.GetUserAsync(name);
                    var roles = Context.Guild.Roles.FirstOrDefault(x => x.Name == "PLAYER");
                    var role = Context.Guild.Roles.FirstOrDefault(y => y.Name == "Silenciado");
                    Console.WriteLine(role);
                    Console.WriteLine(roles);
                    await username.RemoveRoleAsync(roles);
                    await username.AddRoleAsync(role);
                    await Context.Message.DeleteAsync();
                    const int delay = 5000;
                    var m = await this.ReplyAsync($"{Context.User.Mention}, usuario  {name} mutado com suceso! :white_check_mark: ");
                    await Task.Delay(delay);
                    await m.DeleteAsync();
                    EmbedBuilder bd = new EmbedBuilder();
                    var canalTribunal = Context.Guild.GetTextChannel(921024159360905266);
                    bd.WithAuthor($"Mutado por   : {Context.Message.Author}");
                    bd.WithTitle($":x: {username.Username} foi mutado!");
                    bd.WithColor(139, 0, 139);
                    bd.WithThumbnailUrl($"{username.GetAvatarUrl(size: 2048)}");
                    bd.WithDescription($"***Motivo***``` {motivo} ```\n" +
                        $"***ID*** : ```{username.Id}``` ");

                    await canalTribunal.SendMessageAsync("", false, bd.Build());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    EmbedBuilder builder = new EmbedBuilder();
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
                sb.AppendLine($"{User.Mention} você não é administrador e não pode executar este comando!");
                await ReplyAsync(sb.ToString());
            }
        }

        [Command("desmutar")]
        public async Task desmutar(SocketGuildUser name, [Remainder] string motivo = "nenhum motivo")
        {
            var User = Context.User as SocketGuildUser;
            var roleadm = Context.Guild.Roles.FirstOrDefault(x => x.Name == "ADMINISTRADOR");

            if (User.Roles.Contains(roleadm))
            {

                try
                {
                   // var username = Context.Guild.GetUser(name.Id);
                    
                    var username = Context.Guild.GetUser(name.Id);
                    var roles = Context.Guild.Roles.FirstOrDefault(x => x.Name == "PLAYER");
                    var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Silenciado");

                    await username.AddRoleAsync(roles);
                    await username.RemoveRoleAsync(role);
                    await Context.Message.DeleteAsync();
                    const int delay = 5000;
                    var m = await this.ReplyAsync($"{Context.User.Mention}, usuario  {name} desmutado com suceso! :white_check_mark: ");
                    await Task.Delay(delay);
                    await m.DeleteAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    EmbedBuilder builder = new EmbedBuilder();
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
                sb.AppendLine($"{User.Mention} você não é administrador e não pode executar este comando!");
                await ReplyAsync(sb.ToString());
            }
        }
    }
}

