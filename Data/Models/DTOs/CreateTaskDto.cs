﻿namespace Data.Models.DTOs;
public class CreateTaskDto {
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int UserCreatedId { get; set; }
    public int? UserAssignedId { get; set; }
    public int CategoryId { get; set; }
    public int WorkspaceId { get; set; }
}