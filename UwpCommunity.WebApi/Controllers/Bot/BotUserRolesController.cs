using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Attributes;
using UwpCommunity.WebApi.Interfaces;
using UwpCommunity.WebApi.Models.Discord;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Area("bot")]
    [Route("[area]/user/roles")]
    [EnableCors]
    public class BotUserRolesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IDiscordBotService _discordBotService;

        public BotUserRolesController(ILogger<CategoriesController> logger, IDiscordBotService discordBotServic)
        {
            _logger = logger;
            _discordBotService = discordBotServic;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<DSharpPlus.Entities.DiscordGuild>> GetAsync(string userId)
        {
            var userResult = await _discordBotService.GetUser(userId);

            if (userResult == null)
            {
                return NotFound();
            }

            var guildResult = await _discordBotService.GetGuild(userResult.Id);

            return (guildResult != null) ? Ok(guildResult.Roles)
                : (ActionResult)NotFound();
        }

        [HttpPut("{userId}")]
        [DiscordRequirement]
        public async Task<ActionResult<DSharpPlus.Entities.DiscordGuild>> PutAsync(string userId, DiscordRole role)
        {
            var user = await _discordBotService.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            var guildMember = await _discordBotService.GetGuild(user.Id);

            if (guildMember == null)
            {
                return NotFound();
            }

            if (userId != user.Id.ToString())
            {
                // TODO: we need another way to get the permissions or we need a better way to get the channel
                var channel = await _discordBotService.GetChannel();
                var permissions = guildMember.PermissionsIn(channel);
                // If these are mismatched but the user has permission to edit roles, allow it
                if (permissions == DSharpPlus.Permissions.ManageRoles)
                {
                    // If the user is a launch coordinator and is try to assign a launch participant, allow it
                    if (guildMember.Roles.Any(r => r.Name == "Launch Coordinator")
                        && role.Role == "Launch Participant")
                    {
                        return NotFound();
                    }
                }
            }

            var guild = await _discordBotService.GetGuild();

            if (guild == null)
            {
                return NotFound();
            }

            var _role = guild.Roles.FirstOrDefault(x => x.Name.Equals(role.Role));

            if (_role != null)
            {
                await guildMember.GrantRoleAsync(_role);
            }

            return (guildMember != null) ? Ok(guildMember.Roles)
                : (ActionResult)NotFound();
        }

        [HttpDelete("{userId}")]
        [DiscordRequirement]
        public async Task<ActionResult<DSharpPlus.Entities.DiscordGuild>> DeleteAsync(string userId, DiscordRole role)
        {
            var user = await _discordBotService.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            var guildMember = await _discordBotService.GetGuild(user.Id);

            if (guildMember == null)
            {
                return NotFound();
            }

            var guild = await _discordBotService.GetGuild();

            if (guild == null)
            {
                return NotFound();
            }

            var _role = guild.Roles.FirstOrDefault(x => x.Name.Equals(role.Role));

            if (_role != null)
            {
                await guildMember.RevokeRoleAsync(_role);
            }

            return (guildMember != null) ? Ok(guildMember.Roles)
                : (ActionResult)NotFound();
        }
    }
}
