namespace Data.Models.Entities;
public class Workspace {
    public int WorkspaceId { get; set; }
    public string Name { get; set;}
    public string Description { get; set;}
    public string HashedPassword { get; set;}
    public ICollection<UserWorkspace> UserWorkspaces { get; set; }
    public ICollection<Task> Tasks { get; set; }
    public ICollection<Category> Categories { get; set; }
}
