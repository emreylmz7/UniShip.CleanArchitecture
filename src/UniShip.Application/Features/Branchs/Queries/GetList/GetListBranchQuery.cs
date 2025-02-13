using MediatR;
using UniShip.Domain.Branchs;
using Microsoft.EntityFrameworkCore;

namespace UniShip.Application.Features.Branchs.Queries.GetList;

public sealed record class GetListBranchQuery() : IRequest<IQueryable<GetListBranchQueryResponse>>;

public sealed class GetListBranchQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<string> Staff { get; set; } = default!;
    public List<string> Vehicles { get; set; } = default!;
}

internal sealed class GetListBranchQueryHandler(
    IBranchRepository branchRepository) : IRequestHandler<GetListBranchQuery, IQueryable<GetListBranchQueryResponse>>
{
    public Task<IQueryable<GetListBranchQueryResponse>> Handle(GetListBranchQuery request, CancellationToken cancellationToken)
    {
        var response = (from branch in branchRepository.GetAll()
                        .Include(b => b.Staff!.Select(s => new { s.FirstName, s.LastName }))
                        .Include(b => b.Vehicles!.Select(v => new { v.PlateNumber }))
                        select new GetListBranchQueryResponse
                        {
                            Id = branch.Id,
                            Name = branch.Name,
                            Address = branch.Address,
                            PhoneNumber = branch.PhoneNumber,
                            Email = branch.Email,
                            Staff = branch.Staff!.Select(s => s.FirstName + " " + s.LastName).ToList(),
                            Vehicles = branch.Vehicles!.Select(v => v.PlateNumber).ToList()
                        });

        return Task.FromResult(response);
    }
}
