using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Factories;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Bot;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.BotCommands
{
    public class UserBotCommand : IBotCommand
    {
        private readonly IDiscordBotService _discordBotService;

        public UserBotCommand()
        {
            _discordBotService = ServiceProviderFactory.ServiceProvider.GetService<IDiscordBotService>();
        }

        public async Task<string> Execute(DiscordBotCommand discordBotCommand)
        {
            return await User(discordBotCommand.Parameters[0]);
        }

        private async Task<string> User(string userId)
        {
            var resultJson = await _discordBotService.GetUserByDiscordId(userId);
            var discordUser = new DiscordUserDto(resultJson);

            return (discordUser != null)
                ? JsonSerializer.Serialize(discordUser) : "not found";
        }
    }
}
