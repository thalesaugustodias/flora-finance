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
        group.MapGet("", async (Guid userId, WorkspaceQueryHandler handler, CancellationToken ct) => Results.Ok(await handler.ListAsync(userId, ct)));
        group.MapGet("{id:guid}", async (Guid id, WorkspaceQueryHandler handler, CancellationToken ct) =>
        {
            var result = await handler.GetAsync(id, ct);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
        });
        group.MapPut("{id:guid}", async (Guid id, UpdateWorkspaceRequest request, UpdateWorkspaceHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new UpdateWorkspaceCommand(id, request.Name), ct);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
        });
        group.MapDelete("{id:guid}", async (Guid id, ArchiveWorkspaceHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(new ArchiveWorkspaceCommand(id), ct);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
        });
        return app;
    }
}
