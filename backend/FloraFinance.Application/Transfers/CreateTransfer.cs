using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;
using FloraFinance.Domain.Transfers;

namespace FloraFinance.Application.Transfers;

public sealed record CreateTransferCommand(Guid WorkspaceId, Guid SourceAccountId, Guid DestinationAccountId, decimal Amount, string Currency, DateOnly TransferDate, string? Description);

public sealed class CreateTransferHandler(ITransferRepository transfers, IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
    {
        var transfer = Transfer.Create(command.WorkspaceId, command.SourceAccountId, command.DestinationAccountId, command.Amount, command.Currency, command.TransferDate, command.Description);
        if (transfer.IsFailure) return Result<Guid>.Failure([.. transfer.Errors]);
        await transfers.AddAsync(transfer.Value!, cancellationToken);
        var saved = await unitOfWork.SaveChangesAsync(cancellationToken);
        return saved.IsFailure ? Result<Guid>.Failure([.. saved.Errors]) : Result<Guid>.Success(transfer.Value!.Id);
    }
}
