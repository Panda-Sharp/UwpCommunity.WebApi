using System;
using System.Collections.Generic;
using UwpCommunity.Data.Models;

namespace UwpCommunity.WebApi.Models
{
    public class LaunchDto : BaseDto
    {
        public LaunchDto(Launch launch)
        {
            Index = launch.Index;
            IsDeleted = launch.IsDeleted;
            Created = launch.Created;
            LastUpdated = launch.LastUpdated;
            ClientLastUpdated = launch.ClientLastUpdated;
            LaunchId = launch.LaunchId;
            Year = launch.Year;

            foreach (var launchProject in launch.LaunchProjects)
            {
                var projectDto = new ProjectDto(launchProject.Project);
                Projects.Add(projectDto);
            }
        }

        public Guid LaunchId { get; set; }
        public string Year { get; set; }

        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    }
}
