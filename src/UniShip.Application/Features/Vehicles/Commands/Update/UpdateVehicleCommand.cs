using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Vehicles;

namespace UniShip.Application.Features.Vehicles.Commands.Update;
public sealed record class UpdateVehicleCommand(
    Guid Id,
    string PlateNumber,
    string Model,
    VehicleType Type,
    Guid BranchId,
    bool IsActive
) : IRequest<Result<string>>;

internal sealed class VehicleUpdateCommandHandler(IVehicleRepository VehicleRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateVehicleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await VehicleRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (vehicle == null)
        {
            return Result<string>.Failure("Vehicle not found.");
        }

        if (vehicle.PlateNumber != request.PlateNumber)
        {
            var plateExists = await VehicleRepository.AnyAsync(x => x.PlateNumber == request.PlateNumber && x.Id != request.Id, cancellationToken);
            if (plateExists)
            {
                return Result<string>.Failure("A vehicle with this plate number already exists.");
            }
        }

        vehicle.PlateNumber = request.PlateNumber;
        vehicle.Model = request.Model;
        vehicle.Type = request.Type;
        vehicle.BranchId = request.BranchId;
        vehicle.IsActive = request.IsActive;

        VehicleRepository.Update(vehicle);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Vehicle record successfully updated.";
    }
} 