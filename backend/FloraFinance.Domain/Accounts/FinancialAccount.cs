using FloraFinance.Domain.Common;

namespace FloraFinance.Domain.Accounts;

public enum AccountType { Checking, Savings, Cash, Investment, Digital, Credit, Wallet, Other }

public sealed class FinancialAccount : AggregateRoot
{
    private FinancialAccount() { }
    private FinancialAccount(Guid workspaceId, AccountName name, string? bank, AccountType type, Money openingBalance, string color, string icon)
    {
        Id = Guid.NewGuid();
        WorkspaceId = workspaceId;
        Name = name;
        Bank = bank;
        Type = type;
        Currency = openingBalance.Currency;
        InitialBalance = openingBalance.Amount;
        CurrentBalance = openingBalance.Amount;
        Color = color;
        Icon = icon;
        CreatedAt = DateTimeOffset.UtcNow;
        Raise(new AccountCreated(Id, WorkspaceId));
    }

    public Guid WorkspaceId { get; private set; }
    public AccountName Name { get; private set; } = null!;
    public string? Bank { get; private set; }
    public AccountType Type { get; private set; }
    public Currency Currency { get; private set; } = null!;
    public decimal InitialBalance { get; private set; }
    public decimal CurrentBalance { get; private set; }
    public string Color { get; private set; } = "#2f855a";
    public string Icon { get; private set; } = "wallet";
    public bool IsArchived { get; private set; }

    public static Result<FinancialAccount> Create(Guid workspaceId, string? name, string? bank, AccountType type, decimal initialBalance, string? currencyCode, string? color, string? icon)
    {
        var accountName = AccountName.Create(name);
        var currency = Currency.Create(currencyCode);
        if (workspaceId == Guid.Empty) return Result<FinancialAccount>.Failure(new Error("Account.WorkspaceRequired", "Workspace obrigatório."));
        if (accountName.IsFailure || currency.IsFailure) return Result<FinancialAccount>.Failure([.. accountName.Errors, .. currency.Errors]);
        var money = Money.Create(initialBalance, currency.Value!, allowZero: true, allowNegative: true);
        if (money.IsFailure) return Result<FinancialAccount>.Failure([.. money.Errors]);
        return Result<FinancialAccount>.Success(new FinancialAccount(workspaceId, accountName.Value!, bank, type, money.Value!, color ?? "#2f855a", icon ?? "wallet"));
    }

    public void Archive()
    {
        IsArchived = true;
        DeletedAt = DateTimeOffset.UtcNow;
        Raise(new AccountArchived(Id, WorkspaceId));
    }
}

public sealed record AccountName
{
    private AccountName(string value) => Value = value;
    public string Value { get; }
    public static Result<AccountName> Create(string? value) => string.IsNullOrWhiteSpace(value) || value.Length > 120
        ? Result<AccountName>.Failure(new Error("Account.NameInvalid", "Nome obrigatório com no máximo 120 caracteres."))
        : Result<AccountName>.Success(new AccountName(value.Trim()));
}

public sealed record AccountCreated(Guid AccountId, Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
public sealed record AccountArchived(Guid AccountId, Guid WorkspaceId) : IDomainEvent { public Guid EventId { get; } = Guid.NewGuid(); public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow; }
