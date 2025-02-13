using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Customers;

namespace UniShip.Application.Features.Customers.Commands.Delete;
public sealed record class DeleteCustomerCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class CustomerDeleteCommandHandler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var Customer = await CustomerRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (Customer == null)
        {
            return Result<string>.Failure("Customer not found.");
        }

        CustomerRepository.Delete(Customer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Customer record successfully deleted.";
    }
} 