using Application.Interfaces;
using Data.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WiseTaskingBE.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("workspace/{workspaceId}")]
        public async Task<IActionResult> GetAllCategoriesByWorkspaceId(int workspaceId)
        {
            var categories = await _categoryService.GetAllCategoriesByWorkspaceIdAsync(workspaceId);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var success = await _categoryService.CreateCategoryAsync(createCategoryDto);
            if (!success)
                return BadRequest("Failed to create category.");

            return Ok("Category created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var success = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            if (!success)
                return NotFound("Category not found.");

            return Ok("Category updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
                return NotFound("Category not found.");

            return Ok("Category deleted successfully.");
        }
    }

}
