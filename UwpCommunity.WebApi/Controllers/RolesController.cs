using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using UwpCommunity.WebApi.Models.Data;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IRoleService _roleService;

        public RolesController(ILogger<RolesController> logger, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "DiscordAuthentication")]
        public ActionResult<RoleDto> Add(Role role)
        {
            var result = _roleService.Add(role);

            return result.IsSuccess ? Ok(new RoleDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> Get()
        {
            var result = _roleService.Get();

            if (result.IsSuccess)
            {
                List<RoleDto> roles = new List<RoleDto>();
                foreach (var role in result.Value)
                {
                    roles.Add(new RoleDto(role));
                }
                return Ok(roles);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{roleId}")]
        public ActionResult<RoleDto> Get(Guid roleId)
        {
            var result = _roleService.Single(roleId);

            return result.IsSuccess ? Ok(new RoleDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "DiscordAuthentication")]
        public ActionResult<RoleDto> Update(Role role)
        {
            var result = _roleService.UpdateDetachedEntity(role, role.RoleId);

            return result.IsSuccess ? Ok(new RoleDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpDelete("{roleId}")]
        [Authorize(AuthenticationSchemes = "DiscordAuthentication")]
        public ActionResult Delete(Guid roleId)
        {
            var result = _roleService.Delete(roleId);

            return result.IsSuccess ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
