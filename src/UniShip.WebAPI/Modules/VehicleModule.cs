using MediatR;
using TS.Result;
using UniShip.Application.Features.Vehicles.Commands.Create;
using UniShip.Application.Features.Vehicles.Commands.Delete;
using UniShip.Application.Features.Vehicles.Commands.Update;
using UniShip.Application.Features.Vehicles.Queries.GetById;
using UniShip.Application.Features.Vehicles.Queries.GetList;
using UniShip.Domain.Vehicles;

namespace UniShip.WebAPI.Modules;

public static class VehicleModule
{
    public static void RegisterVehicleRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder groupBuilder = app.MapGroup("/vehicles")
            .WithTags("Vehicles");

        // Araç Oluşturma (POST)
        groupBuilder.MapPost(string.Empty,
            async (ISender sender, CreateVehicleCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Tüm Araçları Getir (GET)
        groupBuilder.MapGet(string.Empty,
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetListVehicleQuery(), cancellationToken);
                return Results.Ok(response);
            })
            .Produces<Result<IQueryable<GetListVehicleQueryResponse>>>();

        // ID'ye Göre Araç Getir (GET /{id})
        groupBuilder.MapGet("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetByIdVehicleQuery(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.NotFound(response);
            })
            .Produces<Result<Vehicle>>()
            .WithName("GetVehicleById");

        // Araç Güncelleme (PUT /{id})
        groupBuilder.MapPut("/{id}",
            async (ISender sender, Guid id, UpdateVehicleCommand request, CancellationToken cancellationToken) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID uyuşmuyor.");

                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Araç Silme (DELETE /{id})
        groupBuilder.MapDelete("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new DeleteVehicleCommand(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}
