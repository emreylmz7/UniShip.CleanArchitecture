using UniShip.Domain.Abstractions;
using UniShip.Domain.Branchs;

namespace UniShip.Domain.Vehicles;
public class Vehicle : Entity
{
    public string PlateNumber { get; set; } = default!;
    public string Model { get; set; } = default!;
    public VehicleType Type { get; set; } 
    public Guid BranchId { get; set; }
    public bool IsActive { get; set; }
    public virtual Branch Branch { get; set; } = default!;
}
