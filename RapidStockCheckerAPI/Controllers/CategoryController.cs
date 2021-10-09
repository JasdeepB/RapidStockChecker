using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Dtos;
using RSC.DataAccess.Services;
using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidStockCheckerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = this.categoryRepository.GetCategories().ToList();

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var categoryList = new List<CategoryDto>();

            foreach (var cat in categories)
            {
                categoryList.Add(new CategoryDto
                {
                    Id = cat.Id,
                    Name = cat.Name
                });
            }
            return Ok(categoryList);
        }

        [HttpGet("{categoryId}", Name = "GetCategory")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public IActionResult GetCategory(int categoryId)
        {
            if (this.categoryRepository.CategoryExists(categoryId) == false)
            {
                return NotFound();
            }

            var category = this.categoryRepository.GetCategory(categoryId);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,

            };

            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCategory([FromBody]Category categoryToCreate)
        {
            if (categoryToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var category = this.categoryRepository
                .GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryToCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", $"Category {categoryToCreate.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.categoryRepository.CreateCategory(categoryToCreate) == false)
            {
                ModelState.AddModelError("", $"Something went wrong saving {categoryToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryId = categoryToCreate.Id }, categoryToCreate);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] Category updatedCategoryInfo)
        {
            if (updatedCategoryInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (categoryId != updatedCategoryInfo.Id)
            {
                return BadRequest(ModelState);
            }

            if (this.categoryRepository.CategoryExists(categoryId) == false)
            {
                return NotFound();
            }

            if (this.categoryRepository.IsDuplicateCategory(categoryId, updatedCategoryInfo.Name) == true)
            {
                ModelState.AddModelError("", $"Category {updatedCategoryInfo.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.categoryRepository.UpdateCategory(updatedCategoryInfo) == false)
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedCategoryInfo.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCountry(int categoryId)
        {
            if (this.categoryRepository.CategoryExists(categoryId) == false)
            {
                return NotFound();
            }

            var categoryToDelete = this.categoryRepository.GetCategory(categoryId);

            if (this.categoryRepository.GetAllTypesForCategory(categoryId).Count > 0)
            {
                ModelState.AddModelError("", $"Category {categoryToDelete.Name} cannot be deleted because it is used by at least one type");
                return StatusCode(409, ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.categoryRepository.DeleteCategory(categoryToDelete) == false)
            {
                ModelState.AddModelError("", $"Something went wrong deleting {categoryToDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
