using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.Application.Features.ShipmentTrackings.Commands.Delete;
public sealed record class DeleteShipmentTrackingCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class ShipmentTrackingDeleteCommandHandler(IShipmentTrackingRepository ShipmentTrackingRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteShipmentTrackingCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteShipmentTrackingCommand request, CancellationToken cancellationToken)
    {
        var tracking = await ShipmentTrackingRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tracking == null)
        {
            return Result<string>.Failure("Shipment tracking not found.");
        }

        ShipmentTrackingRepository.Delete(tracking);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Shipment tracking record successfully deleted.";
    }
} 