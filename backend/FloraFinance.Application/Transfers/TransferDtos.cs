namespace FloraFinance.Application.Transfers;

public sealed record TransferResponse(Guid Id, Guid WorkspaceId, Guid SourceAccountId, Guid DestinationAccountId, decimal Amount, string Currency, DateOnly TransferDate, string? Description, DateTimeOffset CreatedAt);
