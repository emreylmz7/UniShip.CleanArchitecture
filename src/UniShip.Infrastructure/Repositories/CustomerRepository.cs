using GenericRepository;
using UniShip.Domain.Customers;
using UniShip.Infrastructure.Context;

namespace UniShip.Infrastructure.Repositories;
internal sealed class CustomerRepository : Repository<Customer, ApplicationDbContext>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }
}
