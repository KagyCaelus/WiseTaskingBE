using Data.Models.DTOs;

namespace Application.Interfaces {
    public interface IWorkspaceService {
        Task<IEnumerable<WorkspaceListDto>> GetAllUserWorkspaces(int userId);
        Task<bool> Create(WorkspaceUserDto workspace);
        Task<WorkspaceDto> GetWorkspaceById(int workspaceId);
        Task<bool> UpdateWorkspace(int workspaceId, UpdateWorkspaceDto updateWorkspaceDto);
        Task<bool> DeleteWorkspace(int workspaceId);
    }
}
