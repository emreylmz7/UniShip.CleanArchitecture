using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using UniShip.Domain.Vehicles;

namespace UniShip.Application.Features.Vehicles.Commands.Create;
public sealed record class CreateVehicleCommand(
    string PlateNumber,
    string Model,
    VehicleType Type,
    Guid BranchId,
    bool IsActive
    ) : IRequest<Result<string>>;

internal sealed class VehicleCreateCommandHandler(IVehicleRepository VehicleRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateVehicleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicleExists = await VehicleRepository.AnyAsync(x => x.PlateNumber == request.PlateNumber, cancellationToken);

        if (vehicleExists)
        {
            return Result<string>.Failure("A vehicle with this plate number already exists.");
        }

        Vehicle vehicle = request.Adapt<Vehicle>();

        VehicleRepository.Add(vehicle);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Vehicle record successfully created.";
    }
} 