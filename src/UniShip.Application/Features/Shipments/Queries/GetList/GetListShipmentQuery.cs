using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniShip.Domain.Shipments;

namespace UniShip.Application.Features.Shipments.Queries.GetList;

public sealed record class GetListShipmentQuery() : IRequest<IQueryable<GetListShipmentQueryResponse>>;

public sealed class GetListShipmentQueryResponse
{
    public Guid Id { get; set; }
    public string TrackingNumber { get; set; } = default!;
    public string SenderName { get; set; } = default!;
    public string BranchName { get; set; } = default!;
    public string? AssignedVehiclePlate { get; set; }
    public string? AssignedCourierName { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public ShipmentStatus Status { get; set; }
    public string ReceiverName { get; set; } = default!;
    public string ReceiverAddress { get; set; } = default!;
    public int TrackingCount { get; set; }
}

internal sealed class GetListShipmentQueryHandler(IShipmentRepository shipmentRepository) 
    : IRequestHandler<GetListShipmentQuery, IQueryable<GetListShipmentQueryResponse>>
{
    public Task<IQueryable<GetListShipmentQueryResponse>> Handle(GetListShipmentQuery request, CancellationToken cancellationToken)
    {
        var response = shipmentRepository.GetAll()
            .Include(s => s.Sender)
            .Include(s => s.Branch)
            .Include(s => s.AssignedVehicle)
            .Include(s => s.AssignedCourier)
            .Include(s => s.TrackingHistory)
            .Select(s => new GetListShipmentQueryResponse
            {
                Id = s.Id,
                TrackingNumber = s.TrackingNumber,
                SenderName = s.Sender.FirstName + " " + s.Sender.LastName,
                BranchName = s.Branch.Name,
                AssignedVehiclePlate = s.AssignedVehicle != null ? s.AssignedVehicle.PlateNumber : null,
                AssignedCourierName = s.AssignedCourier != null ? s.AssignedCourier.FirstName + " " + s.AssignedCourier.LastName : null,
                DeliveryDate = s.DeliveryDate,
                Weight = s.Weight,
                Price = s.Price,
                Status = s.Status,
                ReceiverName = s.ReceiverName,
                ReceiverAddress = s.ReceiverAddress,
                TrackingCount = s.TrackingHistory != null ? s.TrackingHistory.Count : 0
            });

        return Task.FromResult(response);
    }
} 