using UniShip.Domain.Abstractions;
using UniShip.Domain.Shipments;

namespace UniShip.Domain.ShipmentTrackings;
public class ShipmentTracking : Entity
{
    public Guid ShipmentId { get; set; }
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = default!;
    public string? Description { get; set; }
    public ShipmentStatus Status { get; set; }
    public virtual Shipment Shipment { get; set; } = default!;
}
