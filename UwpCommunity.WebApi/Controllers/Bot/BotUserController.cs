using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UwpCommunity.WebApi.Interfaces;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Area("bot")]
    [Route("[area]/[action]")]
    [EnableCors]
    public class BotUserController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IDiscordBotService _discordBotService;

        public BotUserController(ILogger<CategoriesController> logger, IDiscordBotService discordBotServic)
        {
            _logger = logger;
            _discordBotService = discordBotServic;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<DSharpPlus.Entities.DiscordUser>> UserAsync(string userId)
        {
            var result = await _discordBotService.GetUser(userId);

            return (result != null) ? Ok(result)
                : (ActionResult)NotFound();
        }
    }
}
