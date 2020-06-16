using DSharpPlus;
using System.Text.Json;
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

            InitPingPong();

            InitUser();

            await discord.ConnectAsync();
        }

        private void InitPingPong()
        {
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                }
            };
        }

        private void InitUser()
        {
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!user"))
                {
                    var resultJson = await GetUser("586466393969655819");

                    //var result = JsonSerializer.Serialize(resultJson);

                    var message = (resultJson != null) ? resultJson.Username : "not found";

                    await e.Message.RespondAsync(message);                     
                }
            };
        }

        public async Task<DSharpPlus.Entities.DiscordUser> GetUser(string _userId)
        {
            var isSuccess = ulong.TryParse(_userId, out ulong userId);
            return isSuccess ? await discord.GetUserAsync(userId) : null;
        }
    }
}
