using Microsoft.AspNetCore.Identity;
using UniShip.Domain.Branchs;
using UniShip.Domain.Shipments;

namespace UniShip.Domain.Users;
public class AppUser : IdentityUser<Guid>
{
    public AppUser()
    {
        Id = Guid.CreateVersion7();
    }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string Address { get; set; } = default!;
    public Guid BranchId { get; set; } = default!;
    public virtual Branch Branch { get; set; } = default!;
    public virtual ICollection<Shipment>? Shipments { get; set; }


    #region Audit Log
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    #endregion
}
