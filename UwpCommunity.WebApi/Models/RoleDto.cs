using System;

namespace UwpCommunity.WebApi.Models
{
    public class RoleDto : BaseDto
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
    }
}
