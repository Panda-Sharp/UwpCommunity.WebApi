using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Factories;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Bot;

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
            return await GetRole(discordBotCommand.Parameters[0]);
        }

        private async Task<string> GetRole(string role)
        {
            var guild = await _discordBotService.GetGuild();

            var discordRole = guild.Roles.FirstOrDefault(x => x.Name.Equals(role, StringComparison.OrdinalIgnoreCase));

            if(discordRole == null)
            {
                return "Not found";
            }
           
            var numberOfMembers = guild.Members.Count(
                                    x => x.Roles.Any(
                                        x => x.Equals(discordRole)));
            var dateRoleCreated = discordRole.CreationTimestamp.ToString();
            var mentionable = discordRole.IsMentionable;

            return $"Member count: {numberOfMembers}" +
                   Environment.NewLine + 
                   $"Date created: {dateRoleCreated}" +
                   Environment.NewLine +
                   $"Mentionable: {mentionable}";
        }
    }
}
