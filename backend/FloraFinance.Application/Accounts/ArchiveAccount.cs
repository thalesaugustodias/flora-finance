using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.Accounts;

public sealed record ArchiveAccountCommand(Guid Id);

public sealed class ArchiveAccountHandler(IAccountRepository accounts, IUnitOfWork unitOfWork)
{
    public async Task<Result> Handle(ArchiveAccountCommand command, CancellationToken cancellationToken)
    {
        var account = await accounts.GetTrackedAsync(command.Id, cancellationToken);
        if (account is null) return Result.Failure(new Error("Account.NotFound", "Conta não encontrada."));
        account.Archive();
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
