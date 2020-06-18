using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Bot;

namespace UwpCommunity.WebApi.BotCommands
{
    public class PingBotCommand : IBotCommand
    {
        public Task<string> Execute(DiscordBotCommand discordBotCommand)
        {
            return Task.FromResult("pong!");
        }
    }
}
