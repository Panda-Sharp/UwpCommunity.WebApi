using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UwpCommunity.Data.Interfaces;
using UwpCommunity.Data.Models;

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
        public ActionResult<Category> Add(Category category)
        {
            var result = _categoryService.Add(category);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var result = _categoryService.Get();

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpGet("{categoryId}")]
        public ActionResult<IEnumerable<Category>> Get(Guid categoryId)
        {
            var result = _categoryService.Single(categoryId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpPut]
        public ActionResult<Category> Update(Category category)
        {
            var result = _categoryService.UpdateDetachedEntity(category, category.CategoryId);

            return result.Success ? Ok(result.Value)
                : (ActionResult)NotFound();
        }

        [HttpDelete("{categoryId}")]
        public ActionResult<IEnumerable<Category>> Delete(Guid categoryId)
        {
            var result = _categoryService.Delete(categoryId);

            return result.Success ? Ok()
                : (ActionResult)NotFound();
        }
    }
}
