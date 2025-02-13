using UniShip.Domain.Abstractions;
using UniShip.Domain.Customers;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.Domain.Shipments;
public class Shipment : Entity
{
    public string TrackingNumber { get; set; } = default!;
    public Guid SenderId { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public ShipmentStatus Status { get; set; }
    public string? Description { get; set; }
    public string ReceiverName { get; set; } = default!;
    public string ReceiverAddress { get; set; } = default!;
    public string ReceiverPhone { get; set; } = default!;
    public virtual Customer Sender { get; set; } = default!;
    public virtual ICollection<ShipmentTracking>? TrackingHistory { get; set; }
}
