using DSharpPlus;
using System.Linq;
using System.Threading.Tasks;
using UwpCommunity.WebApi.BotCommands;
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
                    var botCommand = new DiscordBotCommand(e.Message.Content);
                    IBotCommand botCommand1 = null;

                    switch (botCommand.Command)
                    {
                        case Commands.Ping:
                            botCommand1 = new PingBotCommand();
                            break;
                        case Commands.User:
                            botCommand1 = new UserBotCommand(this);
                            break;
                    }

                    if(botCommand1 != null)
                    {
                        response = await botCommand1.Execute(botCommand);
                    }

                    await e.Message.RespondAsync(response);
                }
            };

            await discord.ConnectAsync();
        }

        public async Task<DSharpPlus.Entities.DiscordUser> GetUser(string _userId)
        {
            var isSuccess = ulong.TryParse(_userId, out ulong userId);
            return isSuccess ? await discord.GetUserAsync(userId) : null;
        }

        public async Task<DSharpPlus.Entities.DiscordGuild> GetGuild()
        {
            var isSuccess = ulong.TryParse(discordSettings.GuildId, out ulong guildId);
            return isSuccess ? await discord.GetGuildAsync(guildId) : null;
        }

        public async Task<DSharpPlus.Entities.DiscordMember> GetGuild(ulong _userId)
        {
            var guildResult = await GetGuild();

            return guildResult.Members.FirstOrDefault(x => x.Id.Equals(_userId));
        }
    }
}
