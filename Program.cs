using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.app.config;
using Discord.app.JoinLeft;

namespace Discord.app
{
	public class Program
    {
        public static DiscordSocketClient _client;

        private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
		{
            Config cfg = new Config();
            EntrouSaiu join = new EntrouSaiu();

            _client = new DiscordSocketClient();
            cfg._commands = new CommandService();



            await cfg.Client_Ready();
            await cfg.InstallCommandsAsync();
            /*_client.MessageReceived += cfg.HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);*/

            _client.Log += Log;
            _client.UserJoined += join.AnnounceJoinedUser;
            //_client.UserBanned += AnnounceBannedUser;
            _client.UserLeft += join.AnnounceLeftUser;
            cfg._commands.CommandExecuted += cfg.CommandExecutedAsync;

            
            var token = "ODIyOTgzMzU4OTc1NzA1MTE5.YFaM-w.17obPN8rc60gALJ4jflslCp4cnk";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
               

            // Block this task until the program is closed.
            await Task.Delay(-1);
          
        }
    }
}
