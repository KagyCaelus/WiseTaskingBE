using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Models.Entities;
public class TaskConfig : IEntityTypeConfiguration<Task> {
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(x => x.TaskId);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasOne(t => t.Workspace)
            .WithMany(w => w.Tasks)
            .HasForeignKey(t => t.WorkspaceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.UserCreated)
            .WithMany(u => u.TasksCreated)
            .HasForeignKey(t => t.UserCreatedId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.UserAssigned)
            .WithMany(u => u.TasksAssigned)
            .HasForeignKey(t => t.UserAssignedId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
