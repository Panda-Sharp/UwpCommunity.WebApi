using System;
using System.Text.Json.Serialization;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class LaunchProject : BaseEntity
    {
        public Guid LaunchId { get; set; }
        [JsonIgnore]
        public Launch Launch { get; set; }

        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
    }
}
