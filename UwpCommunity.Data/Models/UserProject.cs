using System;
using System.Text.Json.Serialization;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class UserProject : BaseEntity
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
    }
}
