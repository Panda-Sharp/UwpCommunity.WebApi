using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public List<Project> Projects { get; } = new List<Project>();
    }
}
