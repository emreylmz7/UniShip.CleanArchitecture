using UniShip.Domain.Abstractions;
using UniShip.Domain.Users;
using UniShip.Domain.Vehicles;

namespace UniShip.Domain.Branchs;
public class Branch : Entity
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public virtual ICollection<AppUser>? Staff { get; set; }
    public virtual ICollection<Vehicle>? Vehicles { get; set; }
}
