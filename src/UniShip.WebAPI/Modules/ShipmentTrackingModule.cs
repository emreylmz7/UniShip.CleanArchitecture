using MediatR;
using TS.Result;
using UniShip.Application.Features.ShipmentTrackings.Commands.Create;
using UniShip.Application.Features.ShipmentTrackings.Commands.Delete;
using UniShip.Application.Features.ShipmentTrackings.Commands.Update;
using UniShip.Application.Features.ShipmentTrackings.Queries.GetById;
using UniShip.Application.Features.ShipmentTrackings.Queries.GetList;
using UniShip.Domain.ShipmentTrackings;

namespace UniShip.WebAPI.Modules;

public static class ShipmentTrackingModule
{
    public static void RegisterShipmentTrackingRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder groupBuilder = app.MapGroup("/shipment-trackings")
            .WithTags("ShipmentTrackings");

        // Kargo Takibi Oluşturma (POST)
        groupBuilder.MapPost(string.Empty,
            async (ISender sender, CreateShipmentTrackingCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Tüm Kargo Takiplerini Getir (GET)
        groupBuilder.MapGet(string.Empty,
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetListShipmentTrackingQuery(), cancellationToken);
                return Results.Ok(response);
            })
            .Produces<Result<IQueryable<GetListShipmentTrackingQueryResponse>>>();

        // ID'ye Göre Kargo Takibini Getir (GET /{id})
        groupBuilder.MapGet("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetByIdShipmentTrackingQuery(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.NotFound(response);
            })
            .Produces<Result<ShipmentTracking>>()
            .WithName("GetShipmentTrackingById");

        // Kargo Takibini Güncelleme (PUT /{id})
        groupBuilder.MapPut("/{id}",
            async (ISender sender, Guid id, UpdateShipmentTrackingCommand request, CancellationToken cancellationToken) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID uyuşmuyor.");

                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Kargo Takibini Silme (DELETE /{id})
        groupBuilder.MapDelete("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new DeleteShipmentTrackingCommand(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}
