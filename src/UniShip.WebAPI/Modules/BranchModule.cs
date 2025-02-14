using MediatR;
using TS.Result;
using UniShip.Application.Features.Branchs.Commands.Create;
using UniShip.Application.Features.Branchs.Commands.Update;
using UniShip.Application.Features.Branchs.Commands.Delete;
using UniShip.Application.Features.Branchs.Queries.GetById;
using UniShip.Application.Features.Branchs.Queries.GetList;
using UniShip.Domain.Branchs;

namespace UniShip.WebAPI.Modules;

public static class BranchModule
{
    public static void RegisterBranchRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder groupBuilder = app.MapGroup("/branchs")
            .WithTags("Branchs");

        // Şube Oluşturma (POST)
        groupBuilder.MapPost(string.Empty,
            async (ISender sender, CreateBranchCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Tüm Şubeleri Getir (GET)
        groupBuilder.MapGet(string.Empty,
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetListBranchQuery(), cancellationToken);
                return Results.Ok(response);
            })
            .Produces<Result<IQueryable<GetListBranchQueryResponse>>>();

        // ID'ye Göre Şube Getir (GET /{id})
        groupBuilder.MapGet("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetByIdBranchQuery(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.NotFound(response);
            })
            .Produces<Result<Branch>>()
            .WithName("GetBranchById");

        // Şube Güncelleme (PUT /{id})
        groupBuilder.MapPut("/{id}",
            async (ISender sender, Guid id, UpdateBranchCommand request, CancellationToken cancellationToken) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID uyuşmuyor.");

                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Şube Silme (DELETE /{id})
        groupBuilder.MapDelete("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new DeleteBranchCommand(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}
