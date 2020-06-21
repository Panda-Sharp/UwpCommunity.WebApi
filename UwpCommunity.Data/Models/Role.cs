using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }

        public List<UserProjectRole> UserProjectRoles { get; set; } = new List<UserProjectRole>();
    }
}
