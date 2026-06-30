using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.Dashboard;

public sealed record GetDashboardExecutiveQuery(Guid WorkspaceId, DateOnly ReferenceDate);

public sealed class DashboardQueryHandler(IDashboardReadRepository dashboard)
{
    public async Task<Result<DashboardExecutiveResponse>> GetExecutiveAsync(GetDashboardExecutiveQuery query, CancellationToken cancellationToken)
    {
        if (query.WorkspaceId == Guid.Empty)
            return Result<DashboardExecutiveResponse>.Failure(new Error("Dashboard.WorkspaceRequired", "Workspace obrigatório."));

        var response = await dashboard.GetExecutiveAsync(query.WorkspaceId, query.ReferenceDate, cancellationToken);
        return Result<DashboardExecutiveResponse>.Success(response);
    }
}
