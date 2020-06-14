using System;
using System.Collections.Generic;
using UwpCommunity.Data.Models;

namespace UwpCommunity.WebApi.Models
{
    public class UserDto : BaseDto
    {
        public UserDto(User user)
        {
            Index = user.Index;
            IsDeleted = user.IsDeleted;
            Created = user.Created;
            LastUpdated = user.LastUpdated;
            ClientLastUpdated = user.ClientLastUpdated;
            UserId = user.UserId;
            Name = user.Name;
            Email = user.Email;
            DiscordId = user.DiscordId;

            foreach (var userProject in user.UserProjects)
            {
                var projectDto = new ProjectDto(userProject.Project)
                {
                    IsOwner = userProject.IsOwner
                };
                Projects.Add(projectDto);
            }
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DiscordId { get; set; }

        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
    }
}
