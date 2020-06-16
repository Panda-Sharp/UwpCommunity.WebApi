using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.Services
{
    public class DiscordBotService : IDiscordBotService
    {
        private readonly DiscordSettings discordSettings;

        public DiscordBotService(DiscordSettings discordSettings)
        {
            this.discordSettings = discordSettings;
        }
    }
}
