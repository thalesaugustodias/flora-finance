using FloraFinance.Application.Abstractions;

namespace FloraFinance.Application.Expenses;

public sealed class ExpenseQueryHandler(IExpenseRepository expenses)
{
    public async Task<IReadOnlyList<ExpenseResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken)
        => await expenses.ListAsync(workspaceId, from, to, cancellationToken);
}
