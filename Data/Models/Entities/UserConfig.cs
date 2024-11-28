using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Models.Entities;
public class UserConfig : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserName).HasMaxLength(30);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.Biography).HasMaxLength(210);
    }
}
