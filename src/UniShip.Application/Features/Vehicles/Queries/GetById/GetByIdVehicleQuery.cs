using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using UniShip.Domain.Vehicles;

namespace UniShip.Application.Features.Vehicles.Queries.GetById;
public sealed record class GetByIdVehicleQuery(Guid Id) : IRequest<Result<GetByIdVehicleQueryResponse>>;

public sealed class GetByIdVehicleQueryResponse
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; } = default!;
    public string Model { get; set; } = default!;
    public VehicleType Type { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = default!;
    public bool IsActive { get; set; }
}

internal sealed class GetByIdVehicleQueryHandler(IVehicleRepository VehicleRepository) 
    : IRequestHandler<GetByIdVehicleQuery, Result<GetByIdVehicleQueryResponse>>
{
    public async Task<Result<GetByIdVehicleQueryResponse>> Handle(GetByIdVehicleQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await VehicleRepository.GetAll()
            .Include(v => v.Branch)
            .Select(v => new GetByIdVehicleQueryResponse
            {
                Id = v.Id,
                PlateNumber = v.PlateNumber,
                Model = v.Model,
                Type = v.Type,
                BranchId = v.BranchId,
                BranchName = v.Branch.Name,
                IsActive = v.IsActive
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (vehicle == null)
        {
            return Result<GetByIdVehicleQueryResponse>.Failure("Vehicle not found.");
        }

        return vehicle;
    }
} 