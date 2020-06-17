using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Area("bot")]
    [Route("[area]/user/[action]")]
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
        public async Task<ActionResult<DSharpPlus.Entities.DiscordGuild>> RolesAsync(string userId)
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
    }
}
