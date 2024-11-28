namespace Data.Models.Entities;
public class Task {
    public int TaskId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set;}
    public User UserCreated { get; set; }
    public int UserCreatedId { get; set; }
    public User? UserAssigned { get; set; }
    public int? UserAssignedId { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public Workspace Workspace { get; set; }
    public int WorkspaceId { get; set; }

}

