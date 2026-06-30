using FloraFinance.Application.Accounts;

namespace FloraFinance.Api.Endpoints;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/accounts").WithTags("Accounts");
        group.MapPost("", async (CreateAccountCommand command, CreateAccountHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(command, ct);
            return result.IsSuccess ? Results.Created($"/api/v1/accounts/{result.Value}", new { id = result.Value }) : Results.BadRequest(result.Errors);
        });
        group.MapGet("", async (Guid workspaceId, AccountQueryHandler handler, CancellationToken ct) => Results.Ok(await handler.ListAsync(workspaceId, ct)));
        group.MapGet("{id:guid}", async (Guid id, AccountQueryHandler handler, CancellationToken ct) =>
        {
            var result = await handler.GetAsync(id, ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
        });
        group.MapDelete("{id:guid}", async (Guid id, ArchiveAccountHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new ArchiveAccountCommand(id), ct);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
        });
        return app;
    }
}
