namespace FloraFinance.Application.Workspaces;

public sealed record WorkspaceResponse(Guid Id, Guid UserId, string Name, string Slug, string Currency, string Language, string Timezone, DateTimeOffset CreatedAt, DateTimeOffset? UpdatedAt, DateTimeOffset? DeletedAt);
public sealed record UpdateWorkspaceRequest(string Name);
