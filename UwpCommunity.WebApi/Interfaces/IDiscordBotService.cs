using System.Threading.Tasks;

namespace UwpCommunity.WebApi.Interfaces
{
    public interface IDiscordBotService
    {
        Task<DSharpPlus.Entities.DiscordUser> GetUser(string _userId);

        Task<DSharpPlus.Entities.DiscordGuild> GetGuild();

        Task<DSharpPlus.Entities.DiscordMember> GetGuild(ulong _userId);
    }
}
