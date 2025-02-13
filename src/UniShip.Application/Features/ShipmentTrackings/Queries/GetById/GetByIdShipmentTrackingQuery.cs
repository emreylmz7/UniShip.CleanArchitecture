using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using UniShip.Domain.Shipments;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.Application.Features.ShipmentTrackings.Queries.GetById;
public sealed record class GetByIdShipmentTrackingQuery(Guid Id) : IRequest<Result<GetByIdShipmentTrackingQueryResponse>>;

public sealed class GetByIdShipmentTrackingQueryResponse
{
    public Guid Id { get; set; }
    public Guid ShipmentId { get; set; }
    public string ShipmentTrackingNumber { get; set; } = default!;
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = default!;
    public string? Description { get; set; }
    public ShipmentStatus Status { get; set; }
}

internal sealed class GetByIdShipmentTrackingQueryHandler(IShipmentTrackingRepository ShipmentTrackingRepository) 
    : IRequestHandler<GetByIdShipmentTrackingQuery, Result<GetByIdShipmentTrackingQueryResponse>>
{
    public async Task<Result<GetByIdShipmentTrackingQueryResponse>> Handle(GetByIdShipmentTrackingQuery request, CancellationToken cancellationToken)
    {
        var tracking = await ShipmentTrackingRepository.GetAll()
            .Include(t => t.Shipment)
            .Select(t => new GetByIdShipmentTrackingQueryResponse
            {
                Id = t.Id,
                ShipmentId = t.ShipmentId,
                ShipmentTrackingNumber = t.Shipment.TrackingNumber,
                DateTime = t.DateTime,
                Location = t.Location,
                Description = t.Description,
                Status = t.Status
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tracking == null)
        {
            return Result<GetByIdShipmentTrackingQueryResponse>.Failure("Shipment tracking not found.");
        }

        return tracking;
    }
} 