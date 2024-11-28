namespace Data.Models.Entities;
public class Category {
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string HexColor { get; set; }
    public Workspace Workspace { get; set; }
    public int WorkspaceId { get; set; }
    public ICollection<Task> Tasks { get; set; }
}
