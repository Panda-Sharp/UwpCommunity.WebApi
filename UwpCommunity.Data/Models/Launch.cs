using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class Launch : BaseEntity
    {
        public Guid LaunchId { get; set; }
        public string Year { get; set; }

        public List<LaunchProject> LaunchProjects { get; set; } = new List<LaunchProject>();
    }
}
