using System.Threading.Tasks;
using UwpCommunity.WebApi.Models.Bot;

namespace UwpCommunity.WebApi.Interfaces
{
    public interface IBotCommand
    {
        Task<string> Execute(DiscordBotCommand discordBotCommand);
    }
}
