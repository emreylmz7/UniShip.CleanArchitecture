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

        builder.Property(u => u.Address)
               .HasColumnType("varchar(250)")
               .IsRequired();

        builder.Property(u => u.Role)
               .IsRequired();

        builder.Property(u => u.IsActive)
               .HasDefaultValue(true);

        builder.Property(u => u.CreatedDate)
               .IsRequired();

        builder.Property(u => u.CreatedBy)
               .IsRequired();

        builder.Property(u => u.LastModifiedDate);

        builder.Property(u => u.LastModifiedBy);

        builder.Property(u => u.DeletedDate);

        builder.Property(u => u.IsDeleted)
               .HasDefaultValue(false);

        builder.HasOne(u => u.Branch)
               .WithMany(b => b.Staff)
               .HasForeignKey(u => u.BranchId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Shipments)
               .WithOne(s => s.AssignedCourier)
               .HasForeignKey(s => s.AssignedCourierId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}