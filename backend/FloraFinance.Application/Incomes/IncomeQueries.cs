using FloraFinance.Application.Abstractions;

namespace FloraFinance.Application.Incomes;

public sealed class IncomeQueryHandler(IIncomeRepository incomes)
{
    public async Task<IReadOnlyList<IncomeResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken)
        => await incomes.ListAsync(workspaceId, from, to, cancellationToken);
}
