using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniShip.Domain.Users;

namespace UniShip.Infrastructure.Configurations;
internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
               .HasColumnType("varchar(50)")
               .IsRequired();

        builder.Property(u => u.LastName)
               .HasColumnType("varchar(50)")
               .IsRequired();

        builder.Property(u => u.UserName)
               .HasColumnType("varchar(15)")
               .IsRequired();

        builder.Property(u => u.Email)
               .HasColumnType("varchar(50)")
               .IsRequired();

        builder.HasOne(u => u.Branch)
               .WithMany(b => b.Staff)
               .HasForeignKey(u => u.BranchId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

    }
}