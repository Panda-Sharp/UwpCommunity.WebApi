using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.BotCommands
{
    public class UserBotCommand : IBotCommand
    {
        private readonly IDiscordBotService _discordBotService;

        public UserBotCommand(IDiscordBotService discordBotService)
        {
            _discordBotService = discordBotService;
        }

        public async Task<string> Execute(DiscordBotCommand discordBotCommand)
        {
            return await User(discordBotCommand.Parameters[0]);
        }

        private async Task<string> User(string userId)
        {
            var resultJson = await _discordBotService.GetUser(userId);
            //var result = JsonSerializer.Serialize(resultJson);

            return (resultJson != null) ? resultJson.Username : "not found";
        }
    }
}
