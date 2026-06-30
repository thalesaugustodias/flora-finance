using FloraFinance.Application.Transfers;

namespace FloraFinance.Api.Endpoints;

public static class TransferEndpoints
{
    public static IEndpointRouteBuilder MapTransferEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/transfers").WithTags("Transfers");
        group.MapPost("", async (CreateTransferCommand command, CreateTransferHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(command, ct);
            return result.IsSuccess ? Results.Created($"/api/v1/transfers/{result.Value}", new { id = result.Value }) : Results.BadRequest(result.Errors);
        });
        group.MapGet("", async (Guid workspaceId, DateOnly? from, DateOnly? to, TransferQueryHandler handler, CancellationToken ct) => Results.Ok(await handler.ListAsync(workspaceId, from, to, ct)));
        return app;
    }
}
