using System;
using UwpCommunity.Data.Models;

namespace UwpCommunity.WebApi.Models
{
    public class CategoryDto : BaseDto
    {
        public CategoryDto(Category category)
        {
            CategoryId = category.CategoryId;
            Name = category.Name;
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}
