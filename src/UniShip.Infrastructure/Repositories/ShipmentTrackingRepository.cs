using GenericRepository;
using UniShip.Domain.ShipmentTrackings;
using UniShip.Infrastructure.Context;

namespace UniShip.Infrastructure.Repositories;
internal sealed class ShipmentTrackingRepository : Repository<ShipmentTracking, ApplicationDbContext>, IShipmentTrackingRepository
{
    public ShipmentTrackingRepository(ApplicationDbContext context) : base(context)
    {
    }
}
