using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniShip.Domain.Vehicles;

namespace UniShip.Application.Features.Vehicles.Queries.GetList;

public sealed record class GetListVehicleQuery() : IRequest<IQueryable<GetListVehicleQueryResponse>>;

public sealed class GetListVehicleQueryResponse
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; } = default!;
    public string Model { get; set; } = default!;
    public VehicleType Type { get; set; }
    public string BranchName { get; set; } = default!;
    public bool IsActive { get; set; }
}

internal sealed class GetListVehicleQueryHandler(IVehicleRepository vehicleRepository) 
    : IRequestHandler<GetListVehicleQuery, IQueryable<GetListVehicleQueryResponse>>
{
    public Task<IQueryable<GetListVehicleQueryResponse>> Handle(GetListVehicleQuery request, CancellationToken cancellationToken)
    {
        var response = vehicleRepository.GetAll()
            .Include(v => v.Branch)
            .Select(v => new GetListVehicleQueryResponse
            {
                Id = v.Id,
                PlateNumber = v.PlateNumber,
                Model = v.Model,
                Type = v.Type,
                BranchName = v.Branch.Name,
                IsActive = v.IsActive
            });

        return Task.FromResult(response);
    }
} 