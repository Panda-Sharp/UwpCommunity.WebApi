using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DiscordId { get; set; }

        public List<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
