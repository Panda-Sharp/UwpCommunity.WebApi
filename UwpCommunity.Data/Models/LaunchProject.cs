using System;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class LaunchProject : BaseEntity
    {
        public Guid LaunchId { get; set; }
        public Launch Launch { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
