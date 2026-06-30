namespace FloraFinance.Application.CashFlow;

public sealed record CashFlowDayResponse(DateOnly Date, decimal Income, decimal Expenses, decimal Net, decimal ProjectedBalance);
public sealed record MonthlyCashFlowResponse(Guid WorkspaceId, int Year, int Month, decimal OpeningBalance, IReadOnlyList<CashFlowDayResponse> Days);
