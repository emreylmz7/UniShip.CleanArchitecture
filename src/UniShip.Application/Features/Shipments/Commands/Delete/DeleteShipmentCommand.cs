using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Shipments;

namespace UniShip.Application.Features.Shipments.Commands.Delete;
public sealed record class DeleteShipmentCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class ShipmentDeleteCommandHandler(IShipmentRepository ShipmentRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteShipmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipment = await ShipmentRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (shipment == null)
        {
            return Result<string>.Failure("Shipment not found.");
        }

        ShipmentRepository.Delete(shipment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Shipment record successfully deleted.";
    }
} 