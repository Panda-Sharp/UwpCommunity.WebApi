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
        public ActionResult<Role> Add(Role role)
        {
            var result = _roleService.Add(role);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
        {
            var result = _roleService.Get();

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet("{roleId}")]
        public ActionResult<IEnumerable<Role>> Get(Guid roleId)
        {
            var result = _roleService.Single(roleId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpPut]
        public ActionResult<Role> Update(Role role)
        {
            var result = _roleService.UpdateDetachedEntity(role, role.RoleId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpDelete("{roleId}")]
        public ActionResult<IEnumerable<Role>> Delete(Guid roleId)
        {
            var result = _roleService.Delete(roleId);

            return result.Success ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
