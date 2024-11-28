using Data.Models.DTOs;

namespace Application.Interfaces;
public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllTasksAsync(int workspaceId);
    Task<TaskDto> GetTaskByIdAsync(int id);
    Task<bool> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<bool> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto);
    Task<bool> DeleteTaskAsync(int id);
}

