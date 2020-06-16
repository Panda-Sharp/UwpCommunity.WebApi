using DSharpPlus;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.Services
{
    public class DiscordBotService : IDiscordBotService
    {
        private readonly DiscordSettings discordSettings;
        private DiscordClient discord;

        public DiscordBotService(DiscordSettings discordSettings)
        {
            this.discordSettings = discordSettings;

            Init();
        }

        private async void Init()
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = discordSettings.BotToken,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.StartsWith("!"))
                {
                    var response = "";
                    var command = new DiscordBotCommand(e.Message.Content);
                    switch (command.Command)
                    {
                        case Commands.Ping:
                            response = PingPong();
                            break;
                        case Commands.User:
                            response = await User(command.Parameter);
                            break;
                    }
                    await e.Message.RespondAsync(response);
                }
            };

            await discord.ConnectAsync();
        }


        private string PingPong()
        {
            return "pong!";
        }

        private async Task<string> User(string userId)
        {
            var resultJson = await GetUser(userId);
            //var result = JsonSerializer.Serialize(resultJson);

            return (resultJson != null) ? resultJson.Username : "not found";
        }

        public async Task<DSharpPlus.Entities.DiscordUser> GetUser(string _userId)
        {
            var isSuccess = ulong.TryParse(_userId, out ulong userId);
            return isSuccess ? await discord.GetUserAsync(userId) : null;
        }
    }
}
