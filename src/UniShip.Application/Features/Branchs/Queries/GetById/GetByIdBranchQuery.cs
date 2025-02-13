using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using UniShip.Domain.Branchs;

namespace UniShip.Application.Features.Branchs.Queries.GetById;
public sealed record class GetByIdBranchQuery(Guid Id) : IRequest<Result<Branch>>;

internal sealed class GetByIdBranchQueryHandler(IBranchRepository BranchRepository) : IRequestHandler<GetByIdBranchQuery, Result<Branch>>
{
    public async Task<Result<Branch>> Handle(GetByIdBranchQuery request, CancellationToken cancellationToken)
    {
        var branch = await BranchRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (branch == null)
        {
            return Result<Branch>.Failure("Branch with this ID was not found.");
        }

        return branch;
    }
}