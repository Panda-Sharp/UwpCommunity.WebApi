using System;
using UwpCommunity.Data.Models;

namespace UwpCommunity.WebApi.Models.Data
{
    public class ProjectDto : BaseDto
    {
        public ProjectDto(Project project)
        {
            Index = project.Index;
            IsDeleted = project.IsDeleted;
            Created = project.Created;
            LastUpdated = project.LastUpdated;
            ClientLastUpdated = project.ClientLastUpdated;
            ProjectId = project.ProjectId;
            AppName = project.AppName;
            Description = project.Description;
            IsPrivate = project.IsPrivate;
            DownloadLink = project.DownloadLink;
            GithubLink = project.GithubLink;
            ExternalLink = project.ExternalLink;
            AwaitingLaunchApproval = project.AwaitingLaunchApproval;
            NeedsManualReview = project.NeedsManualReview;
            LookingForRoles = project.LookingForRoles;
            HeroImage = project.HeroImage;

            CategoryId = project.CategoryId;

            if (project.Category != null)
            {
                Category = new CategoryDto(project.Category);
            }
        }

        public Guid ProjectId { get; set; }
        public string AppName { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public string DownloadLink { get; set; }
        public string GithubLink { get; set; }
        public string ExternalLink { get; set; }
        public bool AwaitingLaunchApproval { get; set; }
        public bool NeedsManualReview { get; set; }
        public string LookingForRoles { get; set; }
        public string HeroImage { get; set; }

        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public bool IsOwner { get; set; }
    }
}