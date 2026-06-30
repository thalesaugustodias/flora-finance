using FloraFinance.Domain.Common;

namespace FloraFinance.Domain.Workspaces;

public sealed class Workspace : AggregateRoot
{
    private Workspace() { }
    private Workspace(Guid userId, WorkspaceName name, WorkspaceSlug slug, Currency currency, string language, string timezone)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Slug = slug;
        Currency = currency;
        Language = language;
        Timezone = timezone;
        CreatedAt = DateTimeOffset.UtcNow;
        Raise(new WorkspaceCreated(Id, UserId));
    }

    public Guid UserId { get; private set; }
    public WorkspaceName Name { get; private set; } = null!;
    public WorkspaceSlug Slug { get; private set; } = null!;
    public Currency Currency { get; private set; } = null!;
    public string Language { get; private set; } = null!;
    public string Timezone { get; private set; } = null!;
    public bool IsArchived => DeletedAt.HasValue;

    public static Result<Workspace> CreateInitial(Guid userId)
    {
        if (userId == Guid.Empty) return Result<Workspace>.Failure(new Error("Workspace.UserRequired", "Usuário obrigatório."));
        var name = WorkspaceName.Create("Meu Financeiro");
        var currency = Currency.Create("BRL");
        if (name.IsFailure || currency.IsFailure) return Result<Workspace>.Failure([.. name.Errors, .. currency.Errors]);
        return Result<Workspace>.Success(new Workspace(userId, name.Value!, WorkspaceSlug.From(name.Value!.Value, userId), currency.Value!, "pt-BR", "America/Sao_Paulo"));
    }

    public Result Rename(string name)
    {
        var parsed = WorkspaceName.Create(name);
        if (parsed.IsFailure) return Result.Failure([.. parsed.Errors]);
        Name = parsed.Value!;
        UpdatedAt = DateTimeOffset.UtcNow;
        Raise(new WorkspaceUpdated(Id));
        return Result.Success();
    }

    public void Archive()
    {
        DeletedAt = DateTimeOffset.UtcNow;
        Raise(new WorkspaceArchived(Id));
    }
}

public sealed record WorkspaceName
{
    private WorkspaceName(string value) => Value = value;
    public string Value { get; }
    public static Result<WorkspaceName> Create(string? value) => string.IsNullOrWhiteSpace(value) || value.Length > 120
        ? Result<WorkspaceName>.Failure(new Error("Workspace.NameInvalid", "Nome obrigatório com no máximo 120 caracteres."))
        : Result<WorkspaceName>.Success(new WorkspaceName(value.Trim()));
}

public sealed record WorkspaceSlug(string Value)
{
    public static WorkspaceSlug From(string name, Guid userId) => new($"{Slugify(name)}-{userId:N}"[..Math.Min(120, $"{Slugify(name)}-{userId:N}".Length)]);
    private static string Slugify(string value) => string.Join('-', value.Normalize().ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries));
}

public sealed record WorkspaceCreated(Guid WorkspaceId, Guid UserId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record WorkspaceUpdated(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record WorkspaceArchived(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
