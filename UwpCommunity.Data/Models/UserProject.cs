using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class UserProject : BaseEntity
    {
        public Guid UserProjectId { get; set; }
        public bool IsOwner { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public List<UserProjectRole> UserProjectRoles { get; set; } = new List<UserProjectRole>();
    }
}
