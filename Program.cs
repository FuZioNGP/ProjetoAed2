using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Discord.app
{
	public class Program
    {
        private DiscordSocketClient _client;
        private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
		public static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
            _client = new DiscordSocketClient();

            _client.Log += Log;

            var token = "ODIyOTgzMzU4OTc1NzA1MTE5.YFaM-w.Us8v9__0qjZg4qdw9p2khQ5CASM";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
	}
}
