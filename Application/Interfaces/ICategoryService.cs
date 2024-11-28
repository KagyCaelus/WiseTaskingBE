using Data.Models.DTOs;

namespace Application.Interfaces;
public interface ICategoryService {
    Task<IEnumerable<CategoryDto>> GetAllCategoriesByWorkspaceIdAsync(int workspaceId);
    Task<CategoryDto> GetCategoryByIdAsync(int id);
    Task<bool> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
    Task<bool> DeleteCategoryAsync(int id);
}

