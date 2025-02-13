using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Vehicles;

namespace UniShip.Application.Features.Vehicles.Commands.Delete;
public sealed record class DeleteVehicleCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class VehicleDeleteCommandHandler(IVehicleRepository VehicleRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteVehicleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await VehicleRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (vehicle == null)
        {
            return Result<string>.Failure("Vehicle not found.");
        }

        VehicleRepository.Delete(vehicle);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Vehicle record successfully deleted.";
    }
} 