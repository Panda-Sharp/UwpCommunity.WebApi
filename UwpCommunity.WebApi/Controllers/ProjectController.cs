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
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ILaunchService _launchService;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService,
            IUserService userService, ILaunchService launchService)
        {
            _logger = logger;
            _projectService = projectService;
            _userService = userService;
            _launchService = launchService;
        }

        [HttpPost("{discordId}")]
        public ActionResult<Project> Add(Project project, string discordId)
        {
            var userResult = _userService.SingleByDiscordId(discordId);

            if (userResult.Success)
            {
                project.Category = new Category { Name = "category" };
                project.LaunchProjects = new List<LaunchProject>
                {
                    new LaunchProject
                    {
                        Launch =  new Launch{ Year = "2020"}
                    }
                };
                project.UserProjects = new List<UserProject>
                {
                    new UserProject
                    {
                        Role = new Role{ Name="role"},
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
        public ActionResult<IEnumerable<Project>> Launch(string year)
        {
            var result = _launchService.GetProjectsByLaunchYear(year);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet("[action]/{discordId}")]
        public ActionResult<IEnumerable<Project>> DiscordId(string discordId)
        {
            var result = _userService.GetProjectsByByDiscordId(discordId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
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
