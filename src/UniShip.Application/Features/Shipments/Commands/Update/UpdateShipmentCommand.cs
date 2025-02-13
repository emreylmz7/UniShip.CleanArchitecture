using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Shipments;

namespace UniShip.Application.Features.Shipments.Commands.Update;
public sealed record class UpdateShipmentCommand(
    Guid Id,
    DateTime? DeliveryDate,
    decimal Weight,
    decimal Price,
    ShipmentStatus Status,
    string? Description,
    string ReceiverName,
    string ReceiverAddress,
    string ReceiverPhone
) : IRequest<Result<string>>;

internal sealed class ShipmentUpdateCommandHandler(IShipmentRepository ShipmentRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateShipmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipment = await ShipmentRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (shipment == null)
        {
            return Result<string>.Failure("Shipment not found.");
        }

        shipment.DeliveryDate = request.DeliveryDate;
        shipment.Weight = request.Weight;
        shipment.Price = request.Price;
        shipment.Status = request.Status;
        shipment.Description = request.Description;
        shipment.ReceiverName = request.ReceiverName;
        shipment.ReceiverAddress = request.ReceiverAddress;
        shipment.ReceiverPhone = request.ReceiverPhone;

        ShipmentRepository.Update(shipment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Shipment record successfully updated.";
    }
} 