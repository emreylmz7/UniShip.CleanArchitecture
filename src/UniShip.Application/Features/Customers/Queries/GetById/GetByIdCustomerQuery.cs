using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using UniShip.Domain.Customers;

namespace UniShip.Application.Features.Customers.Queries.GetById;
public sealed record class GetByIdCustomerQuery(Guid Id) : IRequest<Result<GetByIdCustomerQueryResponse>>;

public sealed class GetByIdCustomerQueryResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public CustomerType CustomerType { get; set; }
}

internal sealed class GetByIdCustomerQueryHandler(ICustomerRepository CustomerRepository) 
    : IRequestHandler<GetByIdCustomerQuery, Result<GetByIdCustomerQueryResponse>>
{
    public async Task<Result<GetByIdCustomerQueryResponse>> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await CustomerRepository.GetAll()
            .Select(c => new GetByIdCustomerQueryResponse
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                CustomerType = c.CustomerType
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer == null)
        {
            return Result<GetByIdCustomerQueryResponse>.Failure("Customer not found.");
        }

        return customer;
    }
} 