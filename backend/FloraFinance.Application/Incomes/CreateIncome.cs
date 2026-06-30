using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;
using FloraFinance.Domain.Incomes;

namespace FloraFinance.Application.Incomes;

public sealed record CreateIncomeCommand(Guid WorkspaceId, Guid AccountId, Guid CategoryId, string Description, decimal Amount, string Currency, DateOnly ReceivedDate, string? Observation);

public sealed class CreateIncomeHandler(IIncomeRepository incomes, IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CreateIncomeCommand command, CancellationToken cancellationToken)
    {
        var income = Income.Create(command.WorkspaceId, command.AccountId, command.CategoryId, command.Description, command.Amount, command.Currency, command.ReceivedDate, command.Observation);
        if (income.IsFailure) return Result<Guid>.Failure([.. income.Errors]);
        await incomes.AddAsync(income.Value!, cancellationToken);
        var saved = await unitOfWork.SaveChangesAsync(cancellationToken);
        return saved.IsFailure ? Result<Guid>.Failure([.. saved.Errors]) : Result<Guid>.Success(income.Value!.Id);
    }
}
