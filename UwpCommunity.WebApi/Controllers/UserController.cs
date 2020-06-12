using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var result = _userService.Get();

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<User>> Get(Guid userId)
        {
            var result = _userService.Single(userId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet("[action]/{discordId}")]
        public ActionResult<IEnumerable<User>> DiscordId(string discordId)
        {
            var result = _userService.SingleByDiscordId(discordId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpPost]
        public ActionResult<User> Add(User user)
        {
            //var user = new User
            //{
            //    Name = "user",
            //    UserProjects = new List<UserProject>
            //    {
            //        new UserProject()
            //        {
            //            Project = new Project { AppName = "project" }
            //        }
            //    }
            //};

            var result = _userService.Add(user);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpPut]
        public ActionResult<User> Update(User user)
        {
            var result = _userService.AddOrUpdateDetachedEntity(user);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpDelete("{userId}")]
        public ActionResult<IEnumerable<User>> Delete(Guid userId)
        {
            var result = _userService.SoftDelete(userId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }
    }
}
