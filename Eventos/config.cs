using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discord.app.config
{
    public class Config : Program
    {
        public CommandService _commands;
        public async Task Client_Ready()
        {
            await _client.SetGameAsync("WLS SERVER", type: ActivityType.Playing);
        }
        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }
        public async Task HandleCommandAsync(SocketMessage messageParam)
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
