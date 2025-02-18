using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using UniShip.Domain.Shipments;

namespace UniShip.Application.Features.Shipments.Commands.Create;
public sealed record class CreateShipmentCommand(
    string TrackingNumber,
    Guid SenderId,
    Guid BranchId,
    Guid? AssignedVehicleId,
    Guid? AssignedCourierId,
    DateTime? DeliveryDate,
    decimal Weight,
    decimal Price,
    ShipmentStatus Status,
    string? Description,
    string ReceiverName,
    string ReceiverAddress,
    string ReceiverPhone
    ) : IRequest<Result<string>>;

internal sealed class ShipmentCreateCommandHandler(IShipmentRepository ShipmentRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateShipmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipmentExists = await ShipmentRepository.AnyAsync(x => x.TrackingNumber == request.TrackingNumber, cancellationToken);

        if (shipmentExists)
        {
            return Result<string>.Failure("A shipment with this tracking number already exists.");
        }

        Shipment shipment = request.Adapt<Shipment>();

        ShipmentRepository.Add(shipment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Shipment record successfully created.";
    }
} 