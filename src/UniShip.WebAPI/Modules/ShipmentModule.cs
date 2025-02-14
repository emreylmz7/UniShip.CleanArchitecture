using MediatR;
using TS.Result;
using UniShip.Application.Features.Shipments.Commands.Create;
using UniShip.Application.Features.Shipments.Commands.Delete;
using UniShip.Application.Features.Shipments.Commands.Update;
using UniShip.Application.Features.Shipments.Queries.GetById;
using UniShip.Application.Features.Shipments.Queries.GetList;
using UniShip.Domain.Shipments;

namespace UniShip.WebAPI.Modules;

public static class ShipmentModule
{
    public static void RegisterShipmentRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder groupBuilder = app.MapGroup("/shipments")
            .WithTags("Shipments");

        // Kargo Oluşturma (POST)
        groupBuilder.MapPost(string.Empty,
            async (ISender sender, CreateShipmentCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Tüm Kargoları Getir (GET)
        groupBuilder.MapGet(string.Empty,
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetListShipmentQuery(), cancellationToken);
                return Results.Ok(response);
            })
            .Produces<Result<IQueryable<GetListShipmentQueryResponse>>>();

        // ID'ye Göre Kargo Getir (GET /{id})
        groupBuilder.MapGet("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetByIdShipmentQuery(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.NotFound(response);
            })
            .Produces<Result<Shipment>>()
            .WithName("GetShipmentById");

        // Kargo Güncelleme (PUT /{id})
        groupBuilder.MapPut("/{id}",
            async (ISender sender, Guid id, UpdateShipmentCommand request, CancellationToken cancellationToken) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID uyuşmuyor.");

                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Kargo Silme (DELETE /{id})
        groupBuilder.MapDelete("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new DeleteShipmentCommand(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}
