using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Entities;
public class CategoryConfig : IEntityTypeConfiguration<Category> {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.CategoryId);
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.HexColor).HasMaxLength(10);

        builder.HasOne(c => c.Workspace)
            .WithMany(w => w.Categories)
            .HasForeignKey(c => c.WorkspaceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

