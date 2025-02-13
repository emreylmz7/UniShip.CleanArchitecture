using GenericRepository;
using MediatR;
using TS.Result;
using UniShip.Domain.Branchs;

namespace UniShip.Application.Features.Branchs.Commands.Delete;
public sealed record class DeleteBranchCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class BranchDeleteCommandHandler(IBranchRepository BranchRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteBranchCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var Branch = await BranchRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (Branch == null)
        {
            return Result<string>.Failure("Branch with this email was not found.");
        }

        BranchRepository.Delete(Branch);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Branch record successfully deleted.";
    }
}