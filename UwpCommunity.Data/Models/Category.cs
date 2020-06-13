using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class Category : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
