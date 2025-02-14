using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using UniShip.Domain.Branchs;

namespace UniShip.Application.Features.Branchs.Queries.GetById;
public sealed record class GetByIdBranchQuery(Guid Id) : IRequest<Result<GetByIdBranchQueryResponse>>;

public sealed class GetByIdBranchQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<string> Staff { get; set; } = default!;
    public List<string> Vehicles { get; set; } = default!;
}

internal sealed class GetByIdBranchQueryHandler(IBranchRepository BranchRepository) : IRequestHandler<GetByIdBranchQuery, Result<GetByIdBranchQueryResponse>>
{
    public async Task<Result<GetByIdBranchQueryResponse>> Handle(GetByIdBranchQuery request, CancellationToken cancellationToken)
    {
        var branch = await BranchRepository
            .Where(x => x.Id == request.Id)
            .Include(b => b.Staff)
            .Include(b => b.Vehicles)
            .Select(branch => new GetByIdBranchQueryResponse
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                PhoneNumber = branch.PhoneNumber,
                Email = branch.Email,
                Staff = branch.Staff!.Select(s => s.FirstName + " " + s.LastName).ToList(), 
                Vehicles = branch.Vehicles!.Select(v => v.PlateNumber).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (branch == null)
        {
            return Result<GetByIdBranchQueryResponse>.Failure("Branch with this ID was not found.");
        }

        return branch;
    }
}