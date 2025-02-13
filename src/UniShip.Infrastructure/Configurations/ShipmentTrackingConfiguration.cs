using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.Infrastructure.Configurations;
internal class ShipmentTrackingConfiguration : IEntityTypeConfiguration<ShipmentTracking>
{
    public void Configure(EntityTypeBuilder<ShipmentTracking> builder)
    {
        builder.ToTable("ShipmentTrackings");

        builder.HasKey(st => st.Id);

        builder.Property(st => st.ShipmentId)
            .IsRequired();

        builder.Property(st => st.DateTime)
            .IsRequired();

        builder.Property(st => st.Location)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(st => st.Description)
            .HasMaxLength(500);

        builder.Property(st => st.Status)
            .IsRequired();

        builder.HasOne(st => st.Shipment)
            .WithMany(s => s.TrackingHistory)
            .HasForeignKey(st => st.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}