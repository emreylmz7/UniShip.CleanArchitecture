using GenericRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UniShip.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniShip.Domain.Abstractions;
using UniShip.Domain.Branchs;
using UniShip.Domain.Customers;
using UniShip.Domain.Shipments;
using UniShip.Domain.ShipmentTrackings;
using UniShip.Domain.Vehicles;

namespace UniShip.Infrastructure.Context;
internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Branch> Branches { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShipmentTracking> ShipmentTrackings { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        HttpContextAccessor httpContextAccessor = new();
        string userIdString = httpContextAccessor.HttpContext!.User.Claims.First(p => p.Type == "user-id").Value;
        Guid userId = Guid.Parse(userIdString);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedDate)
                    .CurrentValue = DateTime.Now;
                entry.Property(p => p.CreatedBy)
                    .CurrentValue = userId;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeletedDate)
                    .CurrentValue = DateTime.Now;
                }
                else
                {
                    entry.Property(p => p.LastModifiedDate)
                    .CurrentValue = DateTime.Now;

                    entry.Property(p => p.LastModifiedBy)
                    .CurrentValue = userId;
                }
            }

            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Soft delete is not allowed");
            }

        }


        return base.SaveChangesAsync(cancellationToken);
    }
}

