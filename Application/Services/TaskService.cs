using Application.Interfaces;
using Data.Models.DTOs;
using Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;
public class TaskService : ITaskService {
    private readonly WiseTaskingDbContext _dbContext;

    public TaskService(WiseTaskingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(int workspaceId)
    {
        return await _dbContext.Tasks
            .Select(task => new TaskDto
            {
                TaskId = task.TaskId,
                Name = task.Name,
                Description = task.Description,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UserCreatedId = task.UserCreatedId,
                UserAssignedId = task.UserAssignedId,
                CategoryId = task.CategoryId,
                WorkspaceId = task.WorkspaceId
            }).
            Where(x => x.WorkspaceId == workspaceId)
            .ToListAsync();
    }

    public async Task<TaskDto> GetTaskByIdAsync(int id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        if (task == null)
            return null;

        return new TaskDto
        {
            TaskId = task.TaskId,
            Name = task.Name,
            Description = task.Description,
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate,
            UserCreatedId = task.UserCreatedId,
            UserAssignedId = task.UserAssignedId,
            CategoryId = task.CategoryId,
            WorkspaceId = task.WorkspaceId
        };
    }

    public async Task<bool> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        var task = new Data.Models.Entities.Task
        {
            Name = createTaskDto.Name,
            Description = createTaskDto.Description,
            CreatedDate = DateTime.UtcNow,
            DueDate = createTaskDto.DueDate,
            UserCreatedId = createTaskDto.UserCreatedId,
            UserAssignedId = createTaskDto.UserAssignedId,
            CategoryId = createTaskDto.CategoryId,
            WorkspaceId = createTaskDto.WorkspaceId
        };

        _dbContext.Tasks.Add(task);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        if (task == null)
            return false;

        task.Name = updateTaskDto.Name;
        task.Description = updateTaskDto.Description;
        task.DueDate = updateTaskDto.DueDate;
        task.UserAssignedId = updateTaskDto.UserAssignedId;
        task.CategoryId = updateTaskDto.CategoryId;
        task.WorkspaceId = updateTaskDto.WorkspaceId;

        _dbContext.Tasks.Update(task);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        if (task == null)
            return false;

        _dbContext.Tasks.Remove(task);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}

