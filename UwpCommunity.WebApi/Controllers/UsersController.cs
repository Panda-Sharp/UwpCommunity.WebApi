using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using UwpCommunity.WebApi.Models;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<UserDto> Add(User user)
        {
            var result = _userService.Add(user);

            return result.Success ? Ok(new UserDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var result = _userService.Get();

            if (result.Success)
            {
                List<UserDto> users = new List<UserDto>();
                foreach (var user in result.Value)
                {
                    users.Add(new UserDto(user));
                }
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{userId}")]
        public ActionResult<UserDto> Get(Guid userId)
        {
            var result = _userService.Single(userId);

            return result.Success ? Ok(new UserDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpGet("[action]/{discordId}")]
        public ActionResult<UserDto> DiscordId(string discordId)
        {
            var result = _userService.SingleByDiscordId(discordId);

            return result.Success ? Ok(new UserDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpPut]
        public ActionResult<UserDto> Update(User user)
        {
            var result = _userService.UpdateDetachedEntity(user, user.UserId);

            return result.Success ? Ok(new UserDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpDelete("{userId}")]
        public ActionResult Delete(Guid userId)
        {
            var result = _userService.Delete(userId);

            return result.Success ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
