using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using UwpCommunity.WebApi.Attributes;
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
        [DiscordRequirement]
        public ActionResult<ProjectDto> Add(string discordId, Guid? categoryId, string year, Guid? roleId, Project project)
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

            if (userResult.IsSuccess && categoryResult.IsSuccess
                && launchResult.IsSuccess && roleResult.IsSuccess)
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

                var result = _projectService.Add(project);

                return result.IsSuccess ? Ok(new ProjectDto(result.Value)) 
                    : (ActionResult)NotFound();
            }

            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProjectDto>> Get()
        {
            var result = _projectService.GetWithCategory();

            if (result.IsSuccess)
            {
                List<ProjectDto> projects = new List<ProjectDto>();
                foreach(var project in result.Value)
                {
                    projects.Add(new ProjectDto(project));
                }
                return Ok(projects);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("[action]/{year}")]
        public ActionResult<LaunchDto> Launch(string year)
        {
            var result = _launchService.GetProjectsByLaunchYear(year);

            if (result.IsSuccess)
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
        [DiscordRequirement]
        public ActionResult<UserDto> DiscordId(string discordId)
        {
            var result = _userService.GetProjectsByByDiscordId(discordId);

            if (result.IsSuccess)
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
        [DiscordRequirement]
        public ActionResult<ProjectDto> Update(Project project)
        {
            var result = _projectService.UpdateDetachedEntity(project, project.ProjectId);

            return result.IsSuccess ? Ok(new ProjectDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpDelete("{projectId}")]
        [DiscordRequirement]
        public ActionResult Delete(Guid projectId)
        {
            var result = _projectService.Delete(projectId);

            return result.IsSuccess ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
