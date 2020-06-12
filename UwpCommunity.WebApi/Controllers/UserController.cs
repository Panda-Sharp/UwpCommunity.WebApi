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

            //return new Project[]
            //{
            //    new Project{ AppName = "aaa"},
            //    new Project{ AppName = "sss"},
            //    new Project{ AppName = "ddd"}
            //};
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<User> Add()
        {
            var result = _userService.Add(new User
            {
                Name = "user"
            });

            return result.Success ? Ok(result.Value) 
                : (ActionResult)NotFound();
        }
    }
}
