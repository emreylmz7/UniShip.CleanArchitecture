using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using UniShip.Domain.Branchs;

namespace UniShip.Application.Features.Branchs.Commands.Create;
public sealed record class CreateBranchCommand(
    string Name,
    string Address,
    string PhoneNumber,
    string Email,
    bool IsActive
    ) : IRequest<Result<string>>;

internal sealed class BranchCreateCommandHandler(IBranchRepository BranchRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBranchCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var BranchExist = await BranchRepository.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (BranchExist)
        {
            return Result<string>.Failure("This email has already been registered.");
        }

        Branch Branch = request.Adapt<Branch>();

        BranchRepository.Add(Branch);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Branch record successfully created.";
    }
}