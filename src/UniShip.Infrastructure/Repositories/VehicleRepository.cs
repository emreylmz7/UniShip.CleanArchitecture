using GenericRepository;
using UniShip.Domain.Vehicles;
using UniShip.Infrastructure.Context;

namespace UniShip.Infrastructure.Repositories;
internal sealed class VehicleRepository : Repository<Vehicle, ApplicationDbContext>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
