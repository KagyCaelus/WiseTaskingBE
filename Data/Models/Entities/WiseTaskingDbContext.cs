
using Microsoft.EntityFrameworkCore;

namespace Data.Models.Entities;
public class WiseTaskingDbContext : DbContext
{
    public WiseTaskingDbContext(DbContextOptions<WiseTaskingDbContext> options) : base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<UserWorkspace> UserWorkspaces { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfig())
            .ApplyConfiguration(new CategoryConfig())
            .ApplyConfiguration(new TaskConfig())
            .ApplyConfiguration(new WorkspaceConfig())
            .ApplyConfiguration(new UserWorkspaceConfig())
            ;

        base.OnModelCreating(builder);
    }
}
