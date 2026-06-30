using FloraFinance.Domain.Common;

namespace FloraFinance.Domain.Incomes;

public sealed class Income : AggregateRoot
{
    private Income() { }
    private Income(Guid workspaceId, Guid accountId, Guid categoryId, string description, Money amount, DateOnly receivedDate, string? observation)
    {
        Id = Guid.NewGuid(); WorkspaceId = workspaceId; AccountId = accountId; CategoryId = categoryId; Description = description.Trim(); Amount = amount.Amount; Currency = amount.Currency; ReceivedDate = receivedDate; Observation = observation; CreatedAt = DateTimeOffset.UtcNow;
        Raise(new IncomeCreated(Id, WorkspaceId, AccountId));
        Raise(new DashboardRecalculationRequested(WorkspaceId));
        Raise(new CashFlowRecalculationRequested(WorkspaceId));
    }

    public Guid WorkspaceId { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Description { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; } = null!;
    public DateOnly ReceivedDate { get; private set; }
    public string? Observation { get; private set; }

    public static Result<Income> Create(Guid workspaceId, Guid accountId, Guid categoryId, string? description, decimal amount, string? currencyCode, DateOnly receivedDate, string? observation)
    {
        List<Error> errors = [];
        if (workspaceId == Guid.Empty) errors.Add(new Error("Income.WorkspaceRequired", "Workspace obrigatório."));
        if (accountId == Guid.Empty) errors.Add(new Error("Income.AccountRequired", "Conta obrigatória."));
        if (categoryId == Guid.Empty) errors.Add(new Error("Income.CategoryRequired", "Categoria obrigatória."));
        if (string.IsNullOrWhiteSpace(description) || description.Length > 160) errors.Add(new Error("Income.DescriptionInvalid", "Descrição obrigatória com no máximo 160 caracteres."));
        var currency = Currency.Create(currencyCode);
        if (currency.IsFailure) errors.AddRange(currency.Errors);
        if (errors.Count > 0) return Result<Income>.Failure([.. errors]);
        var money = Money.Create(amount, currency.Value!);
        if (money.IsFailure) return Result<Income>.Failure([.. money.Errors]);
        return Result<Income>.Success(new Income(workspaceId, accountId, categoryId, description!, money.Value!, receivedDate, observation));
    }
}

public sealed record IncomeCreated(Guid IncomeId, Guid WorkspaceId, Guid AccountId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record DashboardRecalculationRequested(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record CashFlowRecalculationRequested(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
