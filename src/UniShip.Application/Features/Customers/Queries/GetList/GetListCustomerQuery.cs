using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniShip.Domain.Customers;

namespace UniShip.Application.Features.Customers.Queries.GetList;

public sealed record class GetListCustomerQuery() : IRequest<IQueryable<GetListCustomerQueryResponse>>;

public sealed class GetListCustomerQueryResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public CustomerType CustomerType { get; set; }
    public int ShipmentCount { get; set; }
}

internal sealed class GetListCustomerQueryHandler(ICustomerRepository customerRepository) 
    : IRequestHandler<GetListCustomerQuery, IQueryable<GetListCustomerQueryResponse>>
{
    public Task<IQueryable<GetListCustomerQueryResponse>> Handle(GetListCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = customerRepository.GetAll()
            .Include(c => c.Shipments)
            .Select(c => new GetListCustomerQueryResponse
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                CustomerType = c.CustomerType,
                ShipmentCount = c.Shipments != null ? c.Shipments.Count : 0
            });

        return Task.FromResult(response);
    }
} 