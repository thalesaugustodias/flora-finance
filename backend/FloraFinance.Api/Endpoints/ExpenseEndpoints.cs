using FloraFinance.Application.Expenses;

namespace FloraFinance.Api.Endpoints;

public static class ExpenseEndpoints
{
    public static IEndpointRouteBuilder MapExpenseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/expenses").WithTags("Expenses");
        group.MapPost("", async (CreateExpenseCommand command, CreateExpenseHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(command, ct);
            return result.IsSuccess ? Results.Created($"/api/v1/expenses/{result.Value}", new { id = result.Value }) : Results.BadRequest(result.Errors);
        });
        group.MapGet("", async (Guid workspaceId, DateOnly? from, DateOnly? to, ExpenseQueryHandler handler, CancellationToken ct) => Results.Ok(await handler.ListAsync(workspaceId, from, to, ct)));
        return app;
    }
}
