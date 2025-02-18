using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniShip.Domain.Shipments;

namespace UniShip.Infrastructure.Configurations;
internal class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.ToTable("Shipments");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.TrackingNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.SenderId)
            .IsRequired();

        builder.Property(s => s.BranchId)
            .IsRequired();

        builder.Property(s => s.AssignedVehicleId);

        builder.Property(s => s.AssignedCourierId);

        builder.Property(s => s.DeliveryDate);

        builder.Property(s => s.Weight)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.Status)
            .IsRequired();

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.ReceiverName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.ReceiverAddress)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(s => s.ReceiverPhone)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(s => s.Sender)
            .WithMany(c => c.Shipments)
            .HasForeignKey(s => s.SenderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Branch)
            .WithMany()
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.AssignedVehicle)
            .WithMany()
            .HasForeignKey(s => s.AssignedVehicleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(s => s.AssignedCourier)
            .WithMany()
            .HasForeignKey(s => s.AssignedCourierId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(s => s.TrackingHistory)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
