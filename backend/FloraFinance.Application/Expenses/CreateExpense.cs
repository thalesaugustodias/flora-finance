using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;
using FloraFinance.Domain.Expenses;

namespace FloraFinance.Application.Expenses;

public sealed record CreateExpenseCommand(Guid WorkspaceId, Guid AccountId, Guid CategoryId, string Description, decimal Amount, string Currency, DateOnly DueDate, DateOnly? PaidDate, string? Observation, bool IsRecurring, int? InstallmentNumber, int? InstallmentTotal);

public sealed class CreateExpenseHandler(IExpenseRepository expenses, IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
    {
        var expense = Expense.Create(command.WorkspaceId, command.AccountId, command.CategoryId, command.Description, command.Amount, command.Currency, command.DueDate, command.PaidDate, command.Observation, command.IsRecurring, command.InstallmentNumber, command.InstallmentTotal);
        if (expense.IsFailure) return Result<Guid>.Failure([.. expense.Errors]);
        await expenses.AddAsync(expense.Value!, cancellationToken);
        var saved = await unitOfWork.SaveChangesAsync(cancellationToken);
        return saved.IsFailure ? Result<Guid>.Failure([.. saved.Errors]) : Result<Guid>.Success(expense.Value!.Id);
    }
}
