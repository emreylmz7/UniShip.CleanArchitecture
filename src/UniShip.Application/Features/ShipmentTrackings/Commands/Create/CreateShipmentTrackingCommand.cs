using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using UniShip.Domain.Shipments;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.Application.Features.ShipmentTrackings.Commands.Create;
public sealed record class CreateShipmentTrackingCommand(
    Guid ShipmentId,
    DateTime DateTime,
    string Location,
    string? Description,
    ShipmentStatus Status
    ) : IRequest<Result<string>>;

internal sealed class ShipmentTrackingCreateCommandHandler(IShipmentTrackingRepository ShipmentTrackingRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateShipmentTrackingCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateShipmentTrackingCommand request, CancellationToken cancellationToken)
    {
        ShipmentTracking tracking = request.Adapt<ShipmentTracking>();

        ShipmentTrackingRepository.Add(tracking);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Shipment tracking record successfully created.";
    }
} 