using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.Workspaces;

public sealed record UpdateWorkspaceCommand(Guid Id, string Name);

public sealed class UpdateWorkspaceHandler(IWorkspaceRepository workspaces, IUnitOfWork unitOfWork)
{
    public async Task<Result> Handle(UpdateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var workspace = await workspaces.GetTrackedAsync(command.Id, cancellationToken);
        if (workspace is null) return Result.Failure(new Error("Workspace.NotFound", "Workspace não encontrado."));
        var renamed = workspace.Rename(command.Name);
        if (renamed.IsFailure) return renamed;
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
