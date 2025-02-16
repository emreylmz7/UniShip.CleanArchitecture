using MediatR;
using TS.Result;
using UniShip.Application.Features.Customers.Commands.Create;
using UniShip.Application.Features.Customers.Commands.Update;
using UniShip.Application.Features.Customers.Commands.Delete;
using UniShip.Application.Features.Customers.Queries.GetById;
using UniShip.Application.Features.Customers.Queries.GetList;
using UniShip.Domain.Customers;

namespace UniShip.WebAPI.Modules;

public static class CustomerModule
{
    public static void RegisterCustomerRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder groupBuilder = app.MapGroup("/customers")
            .WithTags("Customers");

        // Müşteri Oluşturma (POST)
        groupBuilder.MapPost(string.Empty,
            async (ISender sender, CreateCustomerCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Tüm Müşterileri Getir (GET)
        groupBuilder.MapGet(string.Empty,
            async (ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetListCustomerQuery(), cancellationToken);
                return Results.Ok(response);
            })
            .Produces<Result<IQueryable<GetListCustomerQueryResponse>>>();

        // ID'ye Göre Müşteri Getir (GET /{id})
        groupBuilder.MapGet("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new GetByIdCustomerQuery(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.NotFound(response);
            })
            .Produces<Result<GetByIdCustomerQueryResponse>>()
            .WithName("GetCustomerById");

        // Müşteri Güncelleme (PUT /{id})
        groupBuilder.MapPut("/{id}",
            async (ISender sender, Guid id, UpdateCustomerCommand request, CancellationToken cancellationToken) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("ID uyuşmuyor.");

                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        // Müşteri Silme (DELETE /{id})
        groupBuilder.MapDelete("/{id}",
            async (ISender sender, Guid id, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(new DeleteCustomerCommand(id), cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}