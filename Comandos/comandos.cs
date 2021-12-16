using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.app.Comandos
{
    public class PlayersCommands : ModuleBase<SocketCommandContext>
    {

        /*[Command("say")]
        [Summary("Repete sua mensagem.")]
        public Task SayAsync([Remainder][Summary("O texto escrito é ")] string echo)
            => ReplyAsync(echo);

        [Command("hello")]
        public async Task HelloCommand()
        {
            var sb = new StringBuilder();

            var user = Context.User;

            sb.AppendLine($"Seu nick é -> " + user);
            sb.AppendLine("Seja muito bem vindo!");

            await ReplyAsync(sb.ToString());
        }*/

        [Command("perguntar")]
        [Alias("ask")]
        public async Task AskEightBall([Remainder] string args = null)
        {
            var sb = new StringBuilder();

            var embed = new EmbedBuilder();

            var replies = new List<string>();

            replies.Add("sim");
            replies.Add("não");
            replies.Add("obvio");
            replies.Add("talvez....");


            embed.WithColor(new Color(0, 255, 0));
            embed.Title = "Bem vindo ao Pergunte para o BOT!";

            sb.AppendLine($"");
            sb.AppendLine();

            if (args == null)
            {
                sb.AppendLine("Desculpe mas você não fez uma pergunta!");
            }
            else
            {
                var answer = replies[new Random().Next(replies.Count - 1)];

                sb.AppendLine($"Você perguntou: " + args);
                sb.AppendLine();
                sb.AppendLine($"resposta: "+ answer);


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


            embed.Description = sb.ToString();

            await ReplyAsync(null, false, embed.Build());
        }

        [Command("servidor")]
        public async Task sobreServer()
        {
            var sb = new StringBuilder();
            var users = Context.Guild.Users.Count;

            IGuild guild = Context.Client.GetGuild(822875098843185182); //Put your discord server id
            IGuildUser[] userson = guild.GetUsersAsync().Result.ToArray();
            int totalUsers = userson.Where(userson => userson.IsBot == false).Count();
            int online = 0;
            int offline = 0;
            int bots = 0;
            foreach (IGuildUser user in userson)
            {
                if (user.IsBot) bots++;
                else if (user.Status == UserStatus.Online) online++;
                else if (user.Status == UserStatus.Offline) offline++;
            }
            sb.AppendLine($"Membros: ```{users}```");
            sb.AppendLine($"Status Bot: ```{Context.Client.ConnectionState}```");
            sb.AppendLine($"Bots no Servidor: ```{bots}```");
            sb.AppendLine($"Usuários Online: ```{online}```");
            sb.AppendLine($"Usuários Offline: ```{offline}```");
            var bd = new EmbedBuilder
            {
                Title = $"sobre servidor : {Context.Guild.Name}",
                Description = sb.ToString()
            };

            bd.WithColor(Color.Blue);



            await Context.Message.DeleteAsync();
            await ReplyAsync("", false, bd.Build());
        }
        [Command("ajuda")]
        public async Task CommandsList()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Olá {Context.User.Mention} :smile: , meu nome é {Context.Client.CurrentUser} sou responsável por cuidar do servidor, estou sempre ligado o que ja por dentro, " +
                $"minha função é manter vocês interagidos com minhas funcionalidades de interação e é claro, moderação, estarei te notificando de promoções ativas, de novos eventos que vão ocorrer" +
                $"então chega de enrolação e vamos aos comandos :smile:");
            sb.AppendLine("Comandos de Interação: \n" +
                "***piada*** \n" +
                "```quer dar gargalhadas?! então faça eu contar uma dois 8 piadas que tenho aqui armazenado, lembrando que não prometo que todos são do seu gosto em. ``` \n" +
                "***Perguntar <pergunta>*** \n ``` Pergunte algo para o bot e ele irá te responder (sim, não, óbvio ou talves) ```\n" +
                "***avatar <name>*** \n ```Amplie a imagem do seu amigo``` \n");
            sb.AppendLine("Comando Info \n" +
                "***servidor*** ```puxe informações gerais do servidor.```");

            var bd = new EmbedBuilder
            {
                Title = $"Lista de comandos",
                Description = sb.ToString()
            };
            
            var usuario = Context.User;
            bd.WithTitle("Lista de comandos");
            bd.WithColor(Color.Blue);


            await ReplyAsync("", false, bd.Build());
        }
        [Command("piada")]
        public async Task contaPiada()
        {

            ulong ids = Context.Channel.Id;
            switch (ids)
            {
                case 822905099911299103: piadas(); break;
                case 823190100258717736: piadas(); break;
                default:
                    await Context.Message.DeleteAsync();
                    const int delay = 5000;
                    var m = await this.ReplyAsync($"{Context.User.Mention},:x: Opa! você não pode executar este comando aqui, por favor execute no Chat comandos. :smile: ");
                    await Task.Delay(delay);
                    await m.DeleteAsync(); break;
            }




        }
        public async void piadas()
        {
            Random rdn = new Random();

            int random = rdn.Next(0, 8);
            switch (random)
            {
                case 0:
                    await ReplyAsync("O que tem quatro patas e um braço? \n – Um pit - bull feliz. ?????? ");
                    break;
                case 1:
                    await ReplyAsync("O que um tijolo falou pro outro? \n – Há um ciumento entre nos. ? ");
                    break;
                case 2:
                    await ReplyAsync("O sujeito chegou naquela cidade e ficou sabendo que o José queria vender um burrinho. Achando o bichinho muito simpático, ele perguntou: - Qual é o nome dele? - Num sei, não... - Como não sabe? O bicho não é seu? E o caipira: - Só qui eu num sei qual é o nome dele... eu chamo ele de Zeca, sô.");
                    break;
                case 3:
                    await ReplyAsync("Por que o Manoel ficou duas horas olhando fixamente pra lata de suco de laranja? \n – Porque estava escrito “concentrado”. ?????? ");
                    break;
                case 4:
                    await ReplyAsync("Qual e a parte do corpo da mulher que cheira bacalhau? \n - O nariz. ??? ");
                    break;
                case 5: await ReplyAsync("Sabe por que o italiano não come churrasco? \n– Porque o macarrão não cabe no espeto. ?? "); break;

                case 6:
                    await ReplyAsync("A mãe pergunta ao Joãozinho: “Joãozinho, porque é que já não passas tempo com o teu amigo Marco?”\n" +
                "Joãozinho: “Mãe, tu gostas de passar tempo com alguém que fume, beba e diga palavrões?”\n" +
                "Mãe: “Claro que não, Joãozinho!”\n" +
                "Joãozinho: “Pois, o Marco também não gosta.” ????");
                    break;
                case 7: await ReplyAsync("O garoto apanhou da vizinha, e a mãe furiosa foi tomar satisfação: Por que a senhora bateu no meu filho? Ele foi mal-educado, e me chamou de gorda. E a senhora acha que vai emagrecer batendo nele?"); break;

                case 8: await ReplyAsync("Um advogado e sua sogra estão em um edifício em chamas. Você só tem tempo pra salvar um dos dois. O que você faz? Você vai almoçar ou vai ao cinema?"); break;

            }
        }

        [Command("avatar")]
        public async Task avatar(SocketUser user)
        {

            try
            {
                var usuario = Context.Guild.GetUser(user.Id);
                EmbedBuilder builds = new EmbedBuilder();
                builds.WithTitle(":camera_with_flash:Abrir a foto em uma nova guia");
                builds.WithColor(139, 0, 139);
                builds.WithAuthor($"Foto de {usuario.Username}");
                builds.WithUrl($"{usuario.GetAvatarUrl(size: 2048)}");
                builds.WithImageUrl($"{usuario.GetAvatarUrl(size: 2048)}");

                await ReplyAsync("", false, builds.Build());
            }
            catch (Exception ex)
            {

                await Context.Message.DeleteAsync();
                const int delay = 5000;
                var m = await this.ReplyAsync($"{Context.User.Mention}, Hmmmm :frowning:, houve algum erro ao encontrar este usuário, tente pegar pelo id dele :smile: ");
                await Task.Delay(delay);
                await m.DeleteAsync();
                Console.WriteLine(ex);
            }
        }
    }
}