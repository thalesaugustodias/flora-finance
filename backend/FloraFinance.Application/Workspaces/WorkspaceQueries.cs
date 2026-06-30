using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.Workspaces;

public sealed class WorkspaceQueryHandler(IWorkspaceRepository workspaces)
{
    public async Task<IReadOnlyList<WorkspaceResponse>> ListAsync(Guid userId, CancellationToken cancellationToken)
        => await workspaces.ListAsync(userId, cancellationToken);

    public async Task<Result<WorkspaceResponse>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var workspace = await workspaces.GetAsync(id, cancellationToken);
        return workspace is null
            ? Result<WorkspaceResponse>.Failure(new Error("Workspace.NotFound", "Workspace não encontrado."))
            : Result<WorkspaceResponse>.Success(workspace);
    }
}
