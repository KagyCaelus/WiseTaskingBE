using Application.Interfaces;
using Data.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WiseTaskingBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("workspace/{workspaceId}")]
        public async Task<IActionResult> GetAllTasks(int workspacesId)
        {
            var tasks = await _taskService.GetAllTasksAsync(workspacesId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto createTaskDto)
        {
            var success = await _taskService.CreateTaskAsync(createTaskDto);
            if (!success)
                return BadRequest("Failed to create task.");

            return Ok("Task created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {
            var success = await _taskService.UpdateTaskAsync(id, updateTaskDto);
            if (!success)
                return NotFound("Task not found.");

            return Ok("Task updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success)
                return NotFound("Task not found.");

            return Ok("Task deleted successfully.");
        }
    }

}

