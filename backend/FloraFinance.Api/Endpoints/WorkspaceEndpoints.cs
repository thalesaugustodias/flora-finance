using FloraFinance.Application.Workspaces;

namespace FloraFinance.Api.Endpoints;

public static class WorkspaceEndpoints
{
    public static IEndpointRouteBuilder MapWorkspaceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/workspaces").WithTags("Workspaces");
        group.MapPost("bootstrap", async (RegisterUserWorkspaceBootstrapCommand command, RegisterUserWorkspaceBootstrapHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(command, ct);
            return result.IsSuccess ? Results.Created($"/api/v1/workspaces/{result.Value}", new { id = result.Value }) : Results.BadRequest(result.Errors);
        });
        return app;
    }
}
