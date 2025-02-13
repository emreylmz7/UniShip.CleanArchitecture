using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Shipments;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.Application.Features.ShipmentTrackings.Commands.Update;
public sealed record class UpdateShipmentTrackingCommand(
    Guid Id,
    DateTime DateTime,
    string Location,
    string? Description,
    ShipmentStatus Status
) : IRequest<Result<string>>;

internal sealed class ShipmentTrackingUpdateCommandHandler(IShipmentTrackingRepository ShipmentTrackingRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateShipmentTrackingCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateShipmentTrackingCommand request, CancellationToken cancellationToken)
    {
        var tracking = await ShipmentTrackingRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tracking == null)
        {
            return Result<string>.Failure("Shipment tracking not found.");
        }

        tracking.DateTime = request.DateTime;
        tracking.Location = request.Location;
        tracking.Description = request.Description;
        tracking.Status = request.Status;

        ShipmentTrackingRepository.Update(tracking);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Shipment tracking record successfully updated.";
    }
} 