using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Branchs;

namespace UniShip.Application.Features.Branchs.Commands.Update;
public sealed record class UpdateBranchCommand(
    string Email,
    string Name,
    string Address,
    string PhoneNumber,
    bool IsActive
) : IRequest<Result<string>>;

internal sealed class BranchUpdateCommandHandler(IBranchRepository BranchRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateBranchCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var Branch = await BranchRepository.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (Branch == null)
        {
            return Result<string>.Failure("Branch with this email was not found.");
        }

        Branch.Name = request.Name;
        Branch.Address = request.Address;
        Branch.PhoneNumber = request.PhoneNumber;
        Branch.IsActive = request.IsActive;

        BranchRepository.Update(Branch);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Branch record successfully updated.";
    }
}