using System;
using Yugen.Toolkit.Standard.Data;

namespace UwpCommunity.Data.Models
{
    public class Project : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public string AppName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
