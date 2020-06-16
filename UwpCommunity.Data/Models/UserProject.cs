using System;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class UserProject : BaseEntity
    {
        public bool IsOwner { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
