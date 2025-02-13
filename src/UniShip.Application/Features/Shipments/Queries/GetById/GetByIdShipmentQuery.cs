using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using UniShip.Domain.Shipments;

namespace UniShip.Application.Features.Shipments.Queries.GetById;
public sealed record class GetByIdShipmentQuery(Guid Id) : IRequest<Result<GetByIdShipmentQueryResponse>>;

public sealed class GetByIdShipmentQueryResponse
{
    public Guid Id { get; set; }
    public string TrackingNumber { get; set; } = default!;
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = default!;
    public DateTime? DeliveryDate { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
    public ShipmentStatus Status { get; set; }
    public string? Description { get; set; }
    public string ReceiverName { get; set; } = default!;
    public string ReceiverAddress { get; set; } = default!;
    public string ReceiverPhone { get; set; } = default!;
}

internal sealed class GetByIdShipmentQueryHandler(IShipmentRepository ShipmentRepository) 
    : IRequestHandler<GetByIdShipmentQuery, Result<GetByIdShipmentQueryResponse>>
{
    public async Task<Result<GetByIdShipmentQueryResponse>> Handle(GetByIdShipmentQuery request, CancellationToken cancellationToken)
    {
        var shipment = await ShipmentRepository.GetAll()
            .Include(s => s.Sender)
            .Select(s => new GetByIdShipmentQueryResponse
            {
                Id = s.Id,
                TrackingNumber = s.TrackingNumber,
                SenderId = s.SenderId,
                SenderName = s.Sender.FirstName + " " + s.Sender.LastName,
                DeliveryDate = s.DeliveryDate,
                Weight = s.Weight,
                Price = s.Price,
                Status = s.Status,
                Description = s.Description,
                ReceiverName = s.ReceiverName,
                ReceiverAddress = s.ReceiverAddress,
                ReceiverPhone = s.ReceiverPhone
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (shipment == null)
        {
            return Result<GetByIdShipmentQueryResponse>.Failure("Shipment not found.");
        }

        return shipment;
    }
} 