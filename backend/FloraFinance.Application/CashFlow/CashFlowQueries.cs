using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.CashFlow;

public sealed record GetMonthlyCashFlowQuery(Guid WorkspaceId, int Year, int Month);

public sealed class CashFlowQueryHandler(ICashFlowReadRepository cashFlow)
{
    public async Task<Result<MonthlyCashFlowResponse>> GetMonthlyAsync(GetMonthlyCashFlowQuery query, CancellationToken cancellationToken)
    {
        if (query.WorkspaceId == Guid.Empty) return Result<MonthlyCashFlowResponse>.Failure(new Error("CashFlow.WorkspaceRequired", "Workspace obrigatório."));
        if (query.Year < 2000 || query.Year > 2100) return Result<MonthlyCashFlowResponse>.Failure(new Error("CashFlow.YearInvalid", "Ano inválido."));
        if (query.Month is < 1 or > 12) return Result<MonthlyCashFlowResponse>.Failure(new Error("CashFlow.MonthInvalid", "Mês inválido."));
        return Result<MonthlyCashFlowResponse>.Success(await cashFlow.GetMonthlyAsync(query.WorkspaceId, query.Year, query.Month, cancellationToken));
    }
}
