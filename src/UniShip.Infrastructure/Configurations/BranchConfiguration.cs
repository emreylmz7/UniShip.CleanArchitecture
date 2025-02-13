using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniShip.Domain.Branchs;

namespace UniShip.Infrastructure.Configurations;
internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(b => b.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.IsActive)
            .IsRequired();

        builder.HasMany(b => b.Staff)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Vehicles)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}