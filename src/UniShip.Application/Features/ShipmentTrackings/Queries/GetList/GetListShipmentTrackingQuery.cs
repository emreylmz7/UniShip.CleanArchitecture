using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniShip.Domain.Shipments;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.Application.Features.ShipmentTrackings.Queries.GetList;

public sealed record class GetListShipmentTrackingQuery() : IRequest<IQueryable<GetListShipmentTrackingQueryResponse>>;

public sealed class GetListShipmentTrackingQueryResponse
{
    public Guid Id { get; set; }
    public string ShipmentTrackingNumber { get; set; } = default!;
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = default!;
    public string? Description { get; set; }
    public ShipmentStatus Status { get; set; }
}

internal sealed class GetListShipmentTrackingQueryHandler(IShipmentTrackingRepository shipmentTrackingRepository) 
    : IRequestHandler<GetListShipmentTrackingQuery, IQueryable<GetListShipmentTrackingQueryResponse>>
{
    public Task<IQueryable<GetListShipmentTrackingQueryResponse>> Handle(GetListShipmentTrackingQuery request, CancellationToken cancellationToken)
    {
        var response = shipmentTrackingRepository.GetAll()
            .Include(t => t.Shipment)
            .Select(t => new GetListShipmentTrackingQueryResponse
            {
                Id = t.Id,
                ShipmentTrackingNumber = t.Shipment.TrackingNumber,
                DateTime = t.DateTime,
                Location = t.Location,
                Description = t.Description,
                Status = t.Status
            });

        return Task.FromResult(response);
    }
} 