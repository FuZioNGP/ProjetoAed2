using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.app.Join;

namespace Discord.app
{
	class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;

        private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
		{
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.Log += Log;
            _client.UserJoined += AnnounceJoinedUser;
            var token = "ODIyOTgzMzU4OTc1NzA1MTE5.YFaM-w.45Iu9UOrpuu45b9Bn6xPH_bQoys";
            await Client_Ready();
            await InstallCommandsAsync();
            _commands.CommandExecuted += CommandExecutedAsync;
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
               

            // Block this task until the program is closed.
            await Task.Delay(-1);
          
        }
        private async Task Client_Ready()
        {
            await _client.SetGameAsync("WLS SERVER", type: ActivityType.Playing);
        }
        private async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }
        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);

        }
        public async Task AnnounceJoinedUser(SocketGuildUser user)
        {
            System.Console.WriteLine($"aqui passou emmmm!");
            var channel = _client.GetChannel(912747154966196284) as SocketTextChannel; // Gets the channel to send the message i
            await channel.SendMessageAsync($"Seja Bem vindo {user.Mention}"); //Welcomes the new user
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // if a command isn't found, log that info to console and exit this method
            if (!command.IsSpecified)
            {
                await context.Channel.SendMessageAsync($"Desculpe, {context.User.Username}... não foi possivel executar seu comando -> [Comando não existe!]!");
                return;
            }


            // log success to the console and exit this method
            if (result.IsSuccess)
            {
                System.Console.WriteLine($"Comando [{command.Value.Name}] executado por -> [{context.User.Username}]");
                return;
            }


            // failure scenario, let's let the user know
            await context.Channel.SendMessageAsync($"Desculpe, {context.User.Username}... Não foi possivel executar seu comando -> [{result}]!");
        }
    }
}
