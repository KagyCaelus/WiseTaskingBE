using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Entities;
public class WorkspaceConfig : IEntityTypeConfiguration<Workspace> {
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasKey(x => x.WorkspaceId);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(200);
    }
}
