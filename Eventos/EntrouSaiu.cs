using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord.app.JoinLeft
{
    public class EntrouSaiu : Program
    {
        public async Task AnnounceLeftUser(SocketGuildUser user)
        {
            var channel = _client.GetChannel(912747154966196284) as SocketTextChannel;
            var embed = new EmbedBuilder
            {
                Title = "Saiu do Servidor :'(",
                Description = $"Até mais {user.Mention}, esperamos que tenha se divertido"
            };

            embed.WithColor(new Color(255, 0, 0));
            embed.WithFooter(footer => footer.Text = $"Id do usuário: {user.Id}");
            embed.WithCurrentTimestamp();
            embed.WithAuthor(user);
            embed.WithThumbnailUrl(user.GetAvatarUrl());

            await channel.SendMessageAsync("", false, embed.Build());
        }
        /*private async Task AnnounceBannedUser(SocketUser user)
        {
            var channel = _client.GetChannel(912747154966196284) as SocketTextChannel; // Gets the channel to send the message i
            await channel.SendMessageAsync($"Usuario banido {user.Mention}"); //Welcomes the new user
        }*/

        public async Task AnnounceJoinedUser(SocketGuildUser user)
        {
            var channel = _client.GetChannel(912747154966196284) as SocketTextChannel;
            var embed = new EmbedBuilder
            {
                Title = "Entrou no Servidor :)",
                Description = $"Seja bem vindo ao Servidor War em Los Santos {user.Mention}"
            };

            embed.WithColor(new Color(0, 255, 0));
            embed.WithFooter(footer => footer.Text = $"Id do usuário: {user.Id}");
            embed.WithCurrentTimestamp();
            embed.WithAuthor(user);
            embed.WithThumbnailUrl(user.GetAvatarUrl());

            await channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
