using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using UniShip.Domain.Customers;

namespace UniShip.Application.Features.Customers.Commands.Create;
public sealed record class CreateCustomerCommand(
    string FirstName,
    string LastName,
    string? Email,
    string PhoneNumber,
    string Address,
    CustomerType CustomerType
    ) : IRequest<Result<string>>;

internal sealed class CustomerCreateCommandHandler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request.Email != null)
        {
            var CustomerExist = await CustomerRepository.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (CustomerExist)
            {
                return Result<string>.Failure("This email has already been registered.");
            }
        }

        Customer Customer = request.Adapt<Customer>();

        CustomerRepository.Add(Customer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Customer record successfully created.";
    }
} 