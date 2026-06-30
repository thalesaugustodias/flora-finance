using FloraFinance.Application.Dashboard;

namespace FloraFinance.Api.Endpoints;

public static class DashboardEndpoints
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/dashboard").WithTags("Dashboard");
        group.MapGet("executive", async (Guid workspaceId, DateOnly? referenceDate, DashboardQueryHandler handler, CancellationToken ct) =>
        {
            var result = await handler.GetExecutiveAsync(new GetDashboardExecutiveQuery(workspaceId, referenceDate ?? DateOnly.FromDateTime(DateTime.UtcNow)), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
        });
        return app;
    }
}
