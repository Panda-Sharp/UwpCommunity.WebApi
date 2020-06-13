using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class Project : BaseEntity
    {
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
        [JsonIgnore]
        public Category Category { get; set; }

        public List<UserProject> UserProjects { get; set; } = new List<UserProject>();

        public List<LaunchProject> LaunchProjects { get; set; } = new List<LaunchProject>();
    }
}
