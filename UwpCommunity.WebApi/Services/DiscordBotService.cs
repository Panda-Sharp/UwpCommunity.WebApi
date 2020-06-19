using DSharpPlus;
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
        private readonly DiscordSettings discordSettings;
        private DiscordClient discord;

        public DiscordBotService(DiscordSettings discordSettings)
        {
            this.discordSettings = discordSettings;
        }

        public async Task Init()
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
                    var discordBotCommand = new DiscordBotCommand(e.Message.Content);

                    DiscordBotCommand.List.TryGetValue(discordBotCommand.Command, out IBotCommand botCommand);

                    if(botCommand != null)
                    {
                        response = await botCommand.Execute(discordBotCommand);
                    }

                    await e.Message.RespondAsync(response);
                }
            };

            await discord.ConnectAsync();
        }

        public async Task<DSharpPlus.Entities.DiscordUser> GetUserByDiscordId(string discordId)
        {
            var isSuccess = ulong.TryParse(discordId, out ulong userId);
            return isSuccess ? await discord.GetUserAsync(userId) : null;
        }

        public async Task<DSharpPlus.Entities.DiscordUser> GetUserByUsername(string username)
        {
            var guild = await GetGuild();
            return guild.Members.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }


        public async Task<DSharpPlus.Entities.DiscordGuild> GetGuild()
        {
            var isSuccess = ulong.TryParse(discordSettings.GuildId, out ulong guildId);
            return isSuccess ? await discord.GetGuildAsync(guildId) : null;
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
            return await discord.GetChannelAsync(channelId);
        }
    }
}
