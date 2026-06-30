namespace FloraFinance.Application.Incomes;

public sealed record IncomeResponse(Guid Id, Guid WorkspaceId, Guid AccountId, Guid CategoryId, string Description, decimal Amount, string Currency, DateOnly ReceivedDate, string? Observation, DateTimeOffset CreatedAt);
