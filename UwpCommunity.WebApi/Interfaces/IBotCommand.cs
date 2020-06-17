using System.Threading.Tasks;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.Interfaces
{
    public interface IBotCommand
    {
        Task<string> Execute(DiscordBotCommand discordBotCommand);
    }
}
