using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.Accounts;

public sealed class AccountQueryHandler(IAccountRepository accounts)
{
    public async Task<IReadOnlyList<AccountResponse>> ListAsync(Guid workspaceId, CancellationToken cancellationToken)
        => await accounts.ListAsync(workspaceId, cancellationToken);

    public async Task<Result<AccountResponse>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await accounts.GetAsync(id, cancellationToken);
        return account is null
            ? Result<AccountResponse>.Failure(new Error("Account.NotFound", "Conta não encontrada."))
            : Result<AccountResponse>.Success(account);
    }
}
