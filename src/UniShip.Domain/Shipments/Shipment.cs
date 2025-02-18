using UniShip.Domain.Abstractions;
using UniShip.Domain.Branchs;
using UniShip.Domain.Customers;
using UniShip.Domain.ShipmentTrackings;
using UniShip.Domain.Users;
using UniShip.Domain.Vehicles;

namespace UniShip.Domain.Shipments;
public class Shipment : Entity
{
    public string TrackingNumber { get; set; } = default!;
    public Guid SenderId { get; set; }
    public Guid BranchId { get; set; }  // Yeni eklenen alan: Hangi şubeye ait?
    public Guid? AssignedVehicleId { get; set; } // Atanan araç
    public Guid? AssignedCourierId { get; set; } // Atanan kurye
    public DateTime? DeliveryDate { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public ShipmentStatus Status { get; set; }
    public string? Description { get; set; }
    public string ReceiverName { get; set; } = default!;
    public string ReceiverAddress { get; set; } = default!;
    public string ReceiverPhone { get; set; } = default!;
    public virtual Branch Branch { get; set; } = default!;
    public virtual Vehicle AssignedVehicle { get; set; } = default!;
    public virtual AppUser AssignedCourier { get; set; } = default!;
    public virtual Customer Sender { get; set; } = default!;
    public virtual ICollection<ShipmentTracking>? TrackingHistory { get; set; }
}
