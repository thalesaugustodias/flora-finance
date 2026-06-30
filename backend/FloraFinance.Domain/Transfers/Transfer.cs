using FloraFinance.Domain.Common;

namespace FloraFinance.Domain.Transfers;

public sealed class Transfer : AggregateRoot
{
    private Transfer() { }
    private Transfer(Guid workspaceId, Guid sourceAccountId, Guid destinationAccountId, Money amount, DateOnly transferDate, string? description)
    {
        Id = Guid.NewGuid();
        WorkspaceId = workspaceId;
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        Amount = amount.Amount;
        Currency = amount.Currency;
        TransferDate = transferDate;
        Description = description?.Trim();
        CreatedAt = DateTimeOffset.UtcNow;
        Raise(new TransferCreated(Id, WorkspaceId, SourceAccountId, DestinationAccountId));
        Raise(new DashboardRecalculationRequested(WorkspaceId));
        Raise(new CashFlowRecalculationRequested(WorkspaceId));
    }

    public Guid WorkspaceId { get; private set; }
    public Guid SourceAccountId { get; private set; }
    public Guid DestinationAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; } = null!;
    public DateOnly TransferDate { get; private set; }
    public string? Description { get; private set; }

    public static Result<Transfer> Create(Guid workspaceId, Guid sourceAccountId, Guid destinationAccountId, decimal amount, string? currencyCode, DateOnly transferDate, string? description)
    {
        List<Error> errors = [];
        if (workspaceId == Guid.Empty) errors.Add(new Error("Transfer.WorkspaceRequired", "Workspace obrigatório."));
        if (sourceAccountId == Guid.Empty) errors.Add(new Error("Transfer.SourceAccountRequired", "Conta de origem obrigatória."));
        if (destinationAccountId == Guid.Empty) errors.Add(new Error("Transfer.DestinationAccountRequired", "Conta de destino obrigatória."));
        if (sourceAccountId == destinationAccountId) errors.Add(new Error("Transfer.SameAccount", "Origem e destino devem ser diferentes."));
        if (description is { Length: > 160 }) errors.Add(new Error("Transfer.DescriptionInvalid", "Descrição deve ter no máximo 160 caracteres."));
        var currency = Currency.Create(currencyCode);
        if (currency.IsFailure) errors.AddRange(currency.Errors);
        if (errors.Count > 0) return Result<Transfer>.Failure([.. errors]);
        var money = Money.Create(amount, currency.Value!);
        if (money.IsFailure) return Result<Transfer>.Failure([.. money.Errors]);
        return Result<Transfer>.Success(new Transfer(workspaceId, sourceAccountId, destinationAccountId, money.Value!, transferDate, description));
    }

    public void Archive()
    {
        DeletedAt = DateTimeOffset.UtcNow;
        Raise(new TransferArchived(Id, WorkspaceId));
    }
}

public sealed record TransferCreated(Guid TransferId, Guid WorkspaceId, Guid SourceAccountId, Guid DestinationAccountId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record TransferArchived(Guid TransferId, Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record DashboardRecalculationRequested(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record CashFlowRecalculationRequested(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
