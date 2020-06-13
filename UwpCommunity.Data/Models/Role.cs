using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }

        public List<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
