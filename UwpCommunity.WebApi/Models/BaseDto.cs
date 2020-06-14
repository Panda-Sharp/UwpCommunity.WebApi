using System;

namespace UwpCommunity.WebApi.Models
{
    public class BaseDto
    {        
        public int Index { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTimeOffset Created { get; set; }
        
        public DateTimeOffset LastUpdated { get; set; }
        
        public DateTimeOffset ClientLastUpdated { get; set; }
    }
}
