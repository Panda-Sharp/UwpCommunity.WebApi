using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;
using UwpCommunity.WebApi.Attributes;
using UwpCommunity.WebApi.Models.Data;

namespace UwpCommunity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpPost]
        [DiscordRequirement]
        public ActionResult<CategoryDto> Add(Category category)
        {
            var result = _categoryService.Add(category);

            return result.IsSuccess ? Ok(new CategoryDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            var result = _categoryService.Get();

            if (result.IsSuccess)
            {
                List<CategoryDto> categories = new List<CategoryDto>();
                foreach (var category in result.Value)
                {
                    categories.Add(new CategoryDto(category));
                }
                return Ok(categories);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{categoryId}")]
        public ActionResult<CategoryDto> Get(Guid categoryId)
        {
            var result = _categoryService.Single(categoryId);

            return result.IsSuccess ? Ok(new CategoryDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpPut]
        [DiscordRequirement]
        public ActionResult<CategoryDto> Update(Category category)
        {
            var result = _categoryService.UpdateDetachedEntity(category, category.CategoryId);

            return result.IsSuccess ? Ok(new CategoryDto(result.Value))
                : (ActionResult)NotFound();
        }

        [HttpDelete("{categoryId}")]
        [DiscordRequirement]
        public ActionResult Delete(Guid categoryId)
        {
            var result = _categoryService.Delete(categoryId);

            return result.IsSuccess ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
