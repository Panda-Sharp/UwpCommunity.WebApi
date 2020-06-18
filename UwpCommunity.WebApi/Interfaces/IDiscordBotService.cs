using System.Threading.Tasks;

namespace UwpCommunity.WebApi.Interfaces
{
    public interface IDiscordBotService
    {
        Task<DSharpPlus.Entities.DiscordUser> GetUserByDiscordId(string discordId);
        Task<DSharpPlus.Entities.DiscordUser> GetUserByUsername(string username);

        Task<DSharpPlus.Entities.DiscordGuild> GetGuild();
        Task<DSharpPlus.Entities.DiscordMember> GetGuild(ulong discordId);


        Task<DSharpPlus.Entities.DiscordChannel> GetChannelGeneral();
        Task<DSharpPlus.Entities.DiscordChannel> GetChannel(string channelName);
        Task<DSharpPlus.Entities.DiscordChannel> GetChannel(ulong channelId);
    }
}
