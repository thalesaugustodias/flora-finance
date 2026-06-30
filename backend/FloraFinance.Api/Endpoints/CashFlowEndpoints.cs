using FloraFinance.Application.CashFlow;

namespace FloraFinance.Api.Endpoints;

public static class CashFlowEndpoints
{
    public static IEndpointRouteBuilder MapCashFlowEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/cash-flow").WithTags("Cash Flow");
        group.MapGet("monthly", async (Guid workspaceId, int year, int month, CashFlowQueryHandler handler, CancellationToken ct) =>
        {
            var result = await handler.GetMonthlyAsync(new GetMonthlyCashFlowQuery(workspaceId, year, month), ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
        });
        return app;
    }
}
