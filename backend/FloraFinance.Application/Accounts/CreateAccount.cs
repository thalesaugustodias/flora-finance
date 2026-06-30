using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Accounts;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.Accounts;

public sealed record CreateAccountCommand(Guid WorkspaceId, string Name, string? Bank, AccountType Type, decimal InitialBalance, string Currency, string? Color, string? Icon);

public sealed class CreateAccountHandler(IAccountRepository accounts, IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var account = FinancialAccount.Create(command.WorkspaceId, command.Name, command.Bank, command.Type, command.InitialBalance, command.Currency, command.Color, command.Icon);
        if (account.IsFailure) return Result<Guid>.Failure([.. account.Errors]);
        await accounts.AddAsync(account.Value!, cancellationToken);
        var saved = await unitOfWork.SaveChangesAsync(cancellationToken);
        return saved.IsFailure ? Result<Guid>.Failure([.. saved.Errors]) : Result<Guid>.Success(account.Value!.Id);
    }
}
