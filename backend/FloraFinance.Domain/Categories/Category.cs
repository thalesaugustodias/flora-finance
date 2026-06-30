using FloraFinance.Domain.Common;

namespace FloraFinance.Domain.Categories;

public enum CategoryKind { Income, Expense }

public sealed class Category : AggregateRoot
{
    private Category() { }
    private Category(Guid workspaceId, string name, CategoryKind kind, bool isSystem)
    {
        Id = Guid.NewGuid(); WorkspaceId = workspaceId; Name = name.Trim(); Kind = kind; IsSystem = isSystem; CreatedAt = DateTimeOffset.UtcNow;
        Raise(new CategoryCreated(Id, WorkspaceId, Kind));
    }

    public Guid WorkspaceId { get; private set; }
    public string Name { get; private set; } = null!;
    public CategoryKind Kind { get; private set; }
    public bool IsSystem { get; private set; }
    public bool IsArchived => DeletedAt.HasValue;

    public static Result<Category> Create(Guid workspaceId, string? name, CategoryKind kind, bool isSystem = false)
    {
        if (workspaceId == Guid.Empty) return Result<Category>.Failure(new Error("Category.WorkspaceRequired", "Workspace obrigatório."));
        if (string.IsNullOrWhiteSpace(name) || name.Length > 120) return Result<Category>.Failure(new Error("Category.NameInvalid", "Nome obrigatório com no máximo 120 caracteres."));
        return Result<Category>.Success(new Category(workspaceId, name, kind, isSystem));
    }

    public void Archive()
    {
        DeletedAt = DateTimeOffset.UtcNow;
        Raise(new CategoryArchived(Id, WorkspaceId));
    }
}

public sealed record CategoryCreated(Guid CategoryId, Guid WorkspaceId, CategoryKind Kind) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record CategoryArchived(Guid CategoryId, Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
