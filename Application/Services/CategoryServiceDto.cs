using Application.Interfaces;
using Data.Models.DTOs;
using Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;
public class CategoryService : ICategoryService {
    private readonly WiseTaskingDbContext _dbContext;

    public CategoryService(WiseTaskingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesByWorkspaceIdAsync(int workspaceId)
    {
        return await _dbContext.Categories
            .Where(c => c.WorkspaceId == workspaceId)
            .Select(category => new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                HexColor = category.HexColor,
                WorkspaceId = category.WorkspaceId
            })
            .ToListAsync();
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null)
            return null;

        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            HexColor = category.HexColor,
            WorkspaceId = category.WorkspaceId
        };
    }

    public async Task<bool> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = new Category
        {
            Name = createCategoryDto.Name,
            HexColor = createCategoryDto.HexColor,
            WorkspaceId = createCategoryDto.WorkspaceId
        };

        _dbContext.Categories.Add(category);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null)
            return false;

        category.Name = updateCategoryDto.Name;
        category.HexColor = updateCategoryDto.HexColor;

        _dbContext.Categories.Update(category);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null)
            return false;

        _dbContext.Categories.Remove(category);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}

