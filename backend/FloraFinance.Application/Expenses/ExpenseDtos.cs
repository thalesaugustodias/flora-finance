namespace FloraFinance.Application.Expenses;

public sealed record ExpenseResponse(Guid Id, Guid WorkspaceId, Guid AccountId, Guid CategoryId, string Description, decimal Amount, string Currency, DateOnly DueDate, DateOnly? PaidDate, string? Observation, bool IsRecurring, int? InstallmentNumber, int? InstallmentTotal, DateTimeOffset CreatedAt);
