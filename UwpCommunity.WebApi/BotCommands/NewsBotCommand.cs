using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Bot;

namespace UwpCommunity.WebApi.BotCommands
{
    public class NewsBotCommand : IBotCommand
    {
        public async Task<string> Execute(DiscordBotCommand discordBotCommand)
        {
            if (discordBotCommand.Parameters.Count() > 1)
            {
                var message = string.Join(" ", discordBotCommand.Parameters.Skip(1));
                return await GetLink(discordBotCommand.Parameters[0], message);
            }
            else
            {
                return await GetLink(discordBotCommand.Parameters[0]);
            }
        }

        private async Task<string> GetLink(string link)
        {
            return $"user" +
                   Environment.NewLine +
                   $"shared: {link}";
        }

        private async Task<string> GetLink(string link, string message)
        {
            return $"user" +
                   Environment.NewLine +
                   $"shared: {link}" +
                   Environment.NewLine +
                   $"and says: {message}";
        }
    }
}
