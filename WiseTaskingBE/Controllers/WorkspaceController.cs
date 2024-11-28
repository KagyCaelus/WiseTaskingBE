using Application.Interfaces;
using Data.Models.DTOs;
using Data.Models.Entities;
using Data.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WiseTaskingBE.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController : ControllerBase {
        private readonly IWorkspaceService _workspaceService;

        public WorkspaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateWorkspace(WorkspaceUserDto workspace)
        {
            try
            {
                if (await _workspaceService.Create(workspace))
                    return Ok(new Response { Success = true, Message = "Workspace created successfully" });
                else
                    return Ok(new Response { Success = false, Message = "There has been an error, please try again" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("getList/{userId}")]
        public async Task<IActionResult> GetWorkspaces(int userId)
        {
            try
            {
                var userWorkspaces = await _workspaceService.GetAllUserWorkspaces(userId);
                return Ok(userWorkspaces);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet("{workspaceId}")]
        public async Task<IActionResult> GetWorkspaceById(int workspaceId)
        {
            try
            {
                var workspace = await _workspaceService.GetWorkspaceById(workspaceId);
                if (workspace == null)
                    return NotFound(new Response { Success = false, Message = "Workspace not found" });

                return Ok(workspace);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPut("{workspaceId}")]
        public async Task<IActionResult> UpdateWorkspace(int workspaceId, UpdateWorkspaceDto updateWorkspaceDto)
        {
            try
            {
                var success = await _workspaceService.UpdateWorkspace(workspaceId, updateWorkspaceDto);
                if (!success)
                    return NotFound(new Response { Success = false, Message = "Workspace not found" });

                return Ok(new Response { Success = true, Message = "Workspace updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpDelete("{workspaceId}")]
        public async Task<IActionResult> DeleteWorkspace(int workspaceId)
        {
            try
            {
                var success = await _workspaceService.DeleteWorkspace(workspaceId);
                if (!success)
                    return NotFound(new Response { Success = false, Message = "Workspace not found" });

                return Ok(new Response { Success = true, Message = "Workspace deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
