using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Common;

namespace FloraFinance.Application.Workspaces;

public sealed record ArchiveWorkspaceCommand(Guid Id);

public sealed class ArchiveWorkspaceHandler(IWorkspaceRepository workspaces, IUnitOfWork unitOfWork)
{
    public async Task<Result> Handle(ArchiveWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var workspace = await workspaces.GetTrackedAsync(command.Id, cancellationToken);
        if (workspace is null) return Result.Failure(new Error("Workspace.NotFound", "Workspace não encontrado."));
        workspace.Archive();
        return await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
