using FloraFinance.Domain.Common;

namespace FloraFinance.Domain.Expenses;

public sealed class Expense : AggregateRoot
{
    private Expense() { }
    private Expense(Guid workspaceId, Guid accountId, Guid categoryId, string description, Money amount, DateOnly dueDate, DateOnly? paidDate, string? observation, bool isRecurring, int? installmentNumber, int? installmentTotal)
    {
        Id = Guid.NewGuid();
        WorkspaceId = workspaceId;
        AccountId = accountId;
        CategoryId = categoryId;
        Description = description.Trim();
        Amount = amount.Amount;
        Currency = amount.Currency;
        DueDate = dueDate;
        PaidDate = paidDate;
        Observation = observation;
        IsRecurring = isRecurring;
        InstallmentNumber = installmentNumber;
        InstallmentTotal = installmentTotal;
        CreatedAt = DateTimeOffset.UtcNow;
        Raise(new ExpenseCreated(Id, WorkspaceId, AccountId));
        Raise(new DashboardRecalculationRequested(WorkspaceId));
        Raise(new CashFlowRecalculationRequested(WorkspaceId));
    }

    public Guid WorkspaceId { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Description { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; } = null!;
    public DateOnly DueDate { get; private set; }
    public DateOnly? PaidDate { get; private set; }
    public string? Observation { get; private set; }
    public bool IsRecurring { get; private set; }
    public int? InstallmentNumber { get; private set; }
    public int? InstallmentTotal { get; private set; }
    public bool IsPaid => PaidDate.HasValue;

    public static Result<Expense> Create(Guid workspaceId, Guid accountId, Guid categoryId, string? description, decimal amount, string? currencyCode, DateOnly dueDate, DateOnly? paidDate, string? observation, bool isRecurring = false, int? installmentNumber = null, int? installmentTotal = null)
    {
        List<Error> errors = [];
        if (workspaceId == Guid.Empty) errors.Add(new Error("Expense.WorkspaceRequired", "Workspace obrigatório."));
        if (accountId == Guid.Empty) errors.Add(new Error("Expense.AccountRequired", "Conta obrigatória."));
        if (categoryId == Guid.Empty) errors.Add(new Error("Expense.CategoryRequired", "Categoria obrigatória."));
        if (string.IsNullOrWhiteSpace(description) || description.Length > 160) errors.Add(new Error("Expense.DescriptionInvalid", "Descrição obrigatória com no máximo 160 caracteres."));
        if ((installmentNumber.HasValue || installmentTotal.HasValue) && (!installmentNumber.HasValue || !installmentTotal.HasValue || installmentNumber < 1 || installmentTotal < 1 || installmentNumber > installmentTotal))
            errors.Add(new Error("Expense.InstallmentInvalid", "Parcelamento inválido."));
        var currency = Currency.Create(currencyCode);
        if (currency.IsFailure) errors.AddRange(currency.Errors);
        if (errors.Count > 0) return Result<Expense>.Failure([.. errors]);
        var money = Money.Create(amount, currency.Value!);
        if (money.IsFailure) return Result<Expense>.Failure([.. money.Errors]);
        return Result<Expense>.Success(new Expense(workspaceId, accountId, categoryId, description!, money.Value!, dueDate, paidDate, observation, isRecurring, installmentNumber, installmentTotal));
    }

    public void Archive()
    {
        DeletedAt = DateTimeOffset.UtcNow;
        Raise(new ExpenseArchived(Id, WorkspaceId));
    }
}

public sealed record ExpenseCreated(Guid ExpenseId, Guid WorkspaceId, Guid AccountId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record ExpenseArchived(Guid ExpenseId, Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record DashboardRecalculationRequested(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record CashFlowRecalculationRequested(Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
