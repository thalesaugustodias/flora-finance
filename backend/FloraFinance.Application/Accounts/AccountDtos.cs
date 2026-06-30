namespace FloraFinance.Application.Accounts;

public sealed record AccountResponse(Guid Id, Guid WorkspaceId, string Name, string? Bank, string Type, string Currency, decimal InitialBalance, decimal CurrentBalance, string Color, string Icon, bool IsArchived, DateTimeOffset CreatedAt, DateTimeOffset? DeletedAt);
