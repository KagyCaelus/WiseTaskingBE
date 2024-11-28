using Microsoft.AspNetCore.Identity;

namespace Data.Models.Entities;
public class User 
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; } = false;
    public string Biography { get; set; }
    public ICollection<UserWorkspace> UserWorkspaces { get; set; }
    public ICollection<Task> TasksCreated { get; set; }
    public ICollection<Task> TasksAssigned { get; set; }
}

