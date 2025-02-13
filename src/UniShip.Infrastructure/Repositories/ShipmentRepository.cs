using GenericRepository;
using UniShip.Domain.Shipments;
using UniShip.Infrastructure.Context;

namespace UniShip.Infrastructure.Repositories;
internal sealed class ShipmentRepository : Repository<Shipment, ApplicationDbContext>, IShipmentRepository
{
    public ShipmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}

