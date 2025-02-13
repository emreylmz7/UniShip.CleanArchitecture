using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniShip.Domain.Vehicles;

namespace UniShip.Infrastructure.Configurations;
internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.PlateNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Type)
            .IsRequired();

        builder.Property(v => v.BranchId)
            .IsRequired();

        builder.Property(v => v.IsActive)
            .IsRequired();

        builder.HasOne(v => v.Branch)
            .WithMany(b => b.Vehicles)
            .HasForeignKey(v => v.BranchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
