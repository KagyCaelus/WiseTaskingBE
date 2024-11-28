using Application.Interfaces;
using Application.Interfaces.Repositories;
using Data.Models.DTOs;
using Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;
public class WorkspaceService : IWorkspaceService {
    private readonly WiseTaskingDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public WorkspaceService(WiseTaskingDbContext context, IPasswordHasher passwordHasher)
    {
        _dbContext = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<bool> Create(WorkspaceUserDto workspace)
    {
        try
        {
            
            Workspace workspaceEntity = new()
            {
                Description = workspace.Description,
                Name = workspace.Name,
                HashedPassword = _passwordHasher.HashPassword(workspace.Password)
            };

            await _dbContext.Workspaces.AddAsync(workspaceEntity);
            await _dbContext.SaveChangesAsync();

            var workspaceId = await _dbContext.Workspaces.Where(x => x.HashedPassword == workspaceEntity.HashedPassword).Select(x => x.WorkspaceId).FirstOrDefaultAsync();

            if(!await AddUserToWorkspace(workspace.UserId, workspaceId))
                return false;
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IEnumerable<WorkspaceListDto>> GetAllUserWorkspaces(int userId)
    {
        try
        {
            var workspaces = await _dbContext.Workspaces
            .Include(x => x.UserWorkspaces)
            .Where(x => x.UserWorkspaces.Any(uw => uw.UserId == userId))
            .ToListAsync();

            return workspaces.Select(workspace => new WorkspaceListDto
            {
                WorkspaceId = workspace.WorkspaceId,
                Name = workspace.Name,
                Description = workspace.Description
            });

        } catch (Exception ex)
        {
            return [];
        }
    }

    public async Task<bool> AddUserToWorkspace(int userId, int workspaceId)
    {
        try
        {
            UserWorkspace userWorkspace = new()
            {
                WorkspaceId = workspaceId,
                UserId = userId
            };

            await _dbContext.UserWorkspaces.AddAsync(userWorkspace);

            await _dbContext.SaveChangesAsync();

            return true;

        } catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<WorkspaceDto> GetWorkspaceById(int workspaceId)
    {
        try
        {
            var workspace = await _dbContext.Workspaces
                .Include(w => w.UserWorkspaces)
                .FirstOrDefaultAsync(w => w.WorkspaceId == workspaceId);

            if (workspace == null)
                return null;

            return new WorkspaceDto
            {
                WorkspaceId = workspace.WorkspaceId,
                Name = workspace.Name,
                Description = workspace.Description
            };
        }
        catch (Exception ex)
        {
            // Log the exception
            return null;
        }
    }

    public async Task<bool> UpdateWorkspace(int workspaceId, UpdateWorkspaceDto updateWorkspaceDto)
    {
        try
        {
            var workspace = await _dbContext.Workspaces.FindAsync(workspaceId);

            if (workspace == null)
                return false;

            workspace.Name = updateWorkspaceDto.Name;
            workspace.Description = updateWorkspaceDto.Description;

            _dbContext.Workspaces.Update(workspace);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            // Log the exception
            return false;
        }
    }

    public async Task<bool> DeleteWorkspace(int workspaceId)
    {
        try
        {
            var workspace = await _dbContext.Workspaces.FindAsync(workspaceId);

            if (workspace == null)
                return false;

            _dbContext.Workspaces.Remove(workspace);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            // Log the exception
            return false;
        }
    }
}
  