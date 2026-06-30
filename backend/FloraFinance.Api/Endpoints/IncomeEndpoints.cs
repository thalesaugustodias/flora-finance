using FloraFinance.Application.Incomes;

namespace FloraFinance.Api.Endpoints;

public static class IncomeEndpoints
{
    public static IEndpointRouteBuilder MapIncomeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/incomes").WithTags("Incomes");
        group.MapPost("", async (CreateIncomeCommand command, CreateIncomeHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(command, ct);
            return result.IsSuccess ? Results.Created($"/api/v1/incomes/{result.Value}", new { id = result.Value }) : Results.BadRequest(result.Errors);
        });
        group.MapGet("", async (Guid workspaceId, DateOnly? from, DateOnly? to, IncomeQueryHandler handler, CancellationToken ct) => Results.Ok(await handler.ListAsync(workspaceId, from, to, ct)));
        return app;
    }
}
