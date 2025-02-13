namespace UniShip.Domain.Shipments;
public enum ShipmentStatus
{
    Created,
    InTransit,
    OutForDelivery,
    Delivered,
    Failed,
    Returned
}
