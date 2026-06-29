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
        return app;
    }
}
