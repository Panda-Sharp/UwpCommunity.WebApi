using System;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class UserProjectRole : BaseEntity
    {
        public Guid UserProjectId { get; set; }
        public UserProject UserProject { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
