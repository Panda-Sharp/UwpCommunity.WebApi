using DSharpPlus;
using Neleus.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Bot;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.Services
{
    public class DiscordBotService : IDiscordBotService
    {
        private readonly DiscordSettings _discordSettings;
        private readonly IServiceByNameFactory<IBotCommand> _factory;

        private DiscordClient discordClient;

        public DiscordBotService(DiscordSettings discordSettings, IServiceByNameFactory<IBotCommand> factory)
        {
            _discordSettings = discordSettings;
            _factory = factory;
        }

        public async Task Init()
        {
            discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = _discordSettings.BotToken,
                TokenType = TokenType.Bot
            });

            discordClient.MessageCreated += async e =>
            {
                if (e.Message.Content.StartsWith("!"))
                {
                    var response = "";
                    var discordBotCommand = new DiscordBotCommand(e.Message.Content);

                    try
                    {
                        IBotCommand botCommand = _factory.GetByName(discordBotCommand.Command);

                        if (botCommand != null)
                        {
                            response = await botCommand.Execute(discordBotCommand);
                        }
                    }
                    catch { }

                    await e.Message.RespondAsync(response);
                }
            };

            await discordClient.ConnectAsync();
        }

        public async Task<DSharpPlus.Entities.DiscordUser> GetUserByDiscordId(string discordId)
        {
            var isSuccess = ulong.TryParse(discordId, out ulong userId);
            return isSuccess ? await discordClient.GetUserAsync(userId) : null;
        }

        public async Task<DSharpPlus.Entities.DiscordUser> GetUserByUsername(string username)
        {
            var guild = await GetGuild();
            return guild.Members.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }


        public async Task<DSharpPlus.Entities.DiscordGuild> GetGuild()
        {
            var isSuccess = ulong.TryParse(_discordSettings.GuildId, out ulong guildId);
            return isSuccess ? await discordClient.GetGuildAsync(guildId) : null;
        }

        public async Task<DSharpPlus.Entities.DiscordMember> GetGuild(ulong discordId)
        {
            var guild = await GetGuild();
            return guild.Members.FirstOrDefault(x => x.Id.Equals(discordId));
        }


        public async Task<DSharpPlus.Entities.DiscordChannel> GetChannelGeneral()
        {
            return await GetChannel("General");
        }

        public async Task<DSharpPlus.Entities.DiscordChannel> GetChannel(string channelName)
        {
            var guild = await GetGuild();
            return guild.Channels.FirstOrDefault(x => x.Name.Equals(channelName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<DSharpPlus.Entities.DiscordChannel> GetChannel(ulong channelId)
        {
            return await discordClient.GetChannelAsync(channelId);
        }
    }
}
