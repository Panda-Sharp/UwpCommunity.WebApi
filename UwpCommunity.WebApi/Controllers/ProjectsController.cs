using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ILaunchService _launchService;
        private readonly ICategoryService _categoryService;
        private readonly IRoleService _roleService;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService,
            IUserService userService, ILaunchService launchService, ICategoryService categoryService, 
            IRoleService roleService)
        {
            _logger = logger;
            _projectService = projectService;
            _userService = userService;
            _launchService = launchService;
            _categoryService = categoryService;
            _roleService = roleService;
        }

        [HttpPost("{discordId}")]
        public ActionResult<Project> Add(string discordId, Guid? categoryId, string year, Guid? roleId, Project project)
        {
            var userResult = _userService.SingleByDiscordId(discordId);

            var categoryResult = categoryId != null 
                ? _categoryService.Single(categoryId) 
                : _categoryService.Single(1);

            var launchResult = !string.IsNullOrEmpty(year)
                ? _launchService.SingleByLaunchYear(year)
                : _launchService.Single(1); 

            var roleResult = roleId != null
                ? _roleService.Single(roleId)
                : _roleService.Single(1); 

            if (userResult.Success && categoryResult.Success 
                && launchResult.Success && roleResult.Success)
            {
                project.CategoryId = categoryResult.Value.CategoryId;
                project.LaunchProjects = new List<LaunchProject>
                {
                    new LaunchProject { LaunchId =  launchResult.Value.LaunchId }
                };
                project.UserProjects = new List<UserProject>
                {
                    new UserProject
                    {
                        RoleId = roleResult.Value.RoleId,
                        User = userResult.Value
                    }
                };

                var projectResult = _projectService.Add(project);

                return projectResult.Success ? Ok(projectResult.Value)
                    : (ActionResult)NotFound();
            }

            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> Get()
        {
            var result = _projectService.Get();

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet("[action]/{year}")]
        public ActionResult<LaunchDto> Launch(string year)
        {
            var result = _launchService.GetProjectsByLaunchYear(year);

            if (result.Success)
            {
                var launchDto = new LaunchDto(result.Value.First());
                return Ok(launchDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("[action]/{discordId}")]
        public ActionResult<UserDto> DiscordId(string discordId)
        {
            var result = _userService.GetProjectsByByDiscordId(discordId);

            if (result.Success)
            {
                var userDto = new UserDto(result.Value.First());
                return Ok(userDto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public ActionResult<Project> Update(Project project)
        {
            var result = _projectService.UpdateDetachedEntity(project, project.ProjectId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpDelete("{projectId}")]
        public ActionResult<IEnumerable<Project>> Delete(Guid projectId)
        {
            var result = _projectService.Delete(projectId);

            return result.Success ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
