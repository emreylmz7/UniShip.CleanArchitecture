using GenericRepository;
using UniShip.Domain.Branchs;
using UniShip.Infrastructure.Context;

namespace UniShip.Infrastructure.Repositories;
internal sealed class BranchRepository : Repository<Branch, ApplicationDbContext>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
    }
}
