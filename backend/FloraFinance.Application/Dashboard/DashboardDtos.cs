namespace FloraFinance.Application.Dashboard;

public sealed record DashboardExecutiveResponse(
    Guid WorkspaceId,
    decimal ConsolidatedBalance,
    decimal MonthlyIncome,
    decimal MonthlyExpenses,
    decimal MonthlyNet,
    decimal UpcomingDueAmount,
    int UpcomingDueCount,
    int FinancialHealthScore,
    DateOnly ReferenceDate);
