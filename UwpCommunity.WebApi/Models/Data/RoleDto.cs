using System;
using UwpCommunity.Data.Models;

namespace UwpCommunity.WebApi.Models.Data
{
    public class RoleDto : BaseDto
    {
        public RoleDto(Role role)
        {
            Index = role.Index;
            IsDeleted = role.IsDeleted;
            Created = role.Created;
            LastUpdated = role.LastUpdated;
            ClientLastUpdated = role.ClientLastUpdated;
            RoleId = role.RoleId;
            Name = role.Name;
        }

        public Guid RoleId { get; set; }
        public string Name { get; set; }
    }
}
