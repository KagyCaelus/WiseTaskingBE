using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Entities;
public class UserWorkspaceConfig : IEntityTypeConfiguration<UserWorkspace> {
    public void Configure(EntityTypeBuilder<UserWorkspace> builder)
    {
        builder.HasKey(x => new { x.UserId, x.WorkspaceId});
        builder.HasOne(uw => uw.User)
            .WithMany(u => u.UserWorkspaces)
            .HasForeignKey(uw => uw.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(uw => uw.Workspace)
            .WithMany(w => w.UserWorkspaces)
            .HasForeignKey(uw => uw.WorkspaceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
