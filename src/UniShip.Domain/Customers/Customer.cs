using UniShip.Domain.Abstractions;
using UniShip.Domain.Shipments;

namespace UniShip.Domain.Customers;
public class Customer : Entity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; } 
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public CustomerType CustomerType { get; set; }
    public virtual ICollection<Shipment>? Shipments { get; set; }
}
