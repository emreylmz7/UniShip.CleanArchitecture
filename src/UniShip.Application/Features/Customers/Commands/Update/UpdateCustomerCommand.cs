using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Customers;

namespace UniShip.Application.Features.Customers.Commands.Update;
public sealed record class UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? Email,
    string PhoneNumber,
    string Address,
    CustomerType CustomerType
) : IRequest<Result<string>>;

internal sealed class CustomerUpdateCommandHandler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var Customer = await CustomerRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (Customer == null)
        {
            return Result<string>.Failure("Customer not found.");
        }

        if (request.Email != null && Customer.Email != request.Email)
        {
            var emailExists = await CustomerRepository.AnyAsync(x => x.Email == request.Email && x.Id != request.Id, cancellationToken);
            if (emailExists)
            {
                return Result<string>.Failure("This email is already registered to another customer.");
            }
        }

        Customer.FirstName = request.FirstName;
        Customer.LastName = request.LastName;
        Customer.Email = request.Email;
        Customer.PhoneNumber = request.PhoneNumber;
        Customer.Address = request.Address;
        Customer.CustomerType = request.CustomerType;

        CustomerRepository.Update(Customer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Customer record successfully updated.";
    }
} 