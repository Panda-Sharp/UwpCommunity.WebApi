using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Factories;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Bot;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.BotCommands
{
    public class RoleInfoBotCommand : IBotCommand
    {
        private readonly IDiscordBotService _discordBotService;

        public RoleInfoBotCommand()
        {
            _discordBotService = ServiceProviderFactory.ServiceProvider.GetService<IDiscordBotService>();
        }

        public async Task<string> Execute(DiscordBotCommand discordBotCommand)
        {
            return await User(discordBotCommand.Parameters[0]);
        }

        private async Task<string> User(string role)
        {
            var guild = await _discordBotService.GetGuild();

            var discordRole = guild.Roles.FirstOrDefault(x => x.Name.Equals(role, StringComparison.OrdinalIgnoreCase));

            if(discordRole == null)
            {
                return "not found";
            }

            //var numberOfMembers = discordRole.mem.size;
            var dateRoleCreated = discordRole.CreationTimestamp.ToString();
            var mentionable = discordRole.IsMentionable;

            return $"dateRoleCreated: {dateRoleCreated}" +
                   Environment.NewLine +
                   $"mentionable: {mentionable}";
        }
    }
}
