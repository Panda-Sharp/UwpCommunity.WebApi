using System.Threading.Tasks;

namespace UwpCommunity.WebApi.Interfaces
{
    public interface IDiscordBotService
    {
        Task<DSharpPlus.Entities.DiscordUser> GetUser(string _userId);
    }
}
