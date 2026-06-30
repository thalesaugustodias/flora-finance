namespace FloraFinance.Domain.Common;

public sealed record Currency
{
    private static readonly HashSet<string> Allowed = ["BRL", "USD", "EUR"];
    private Currency(string code) => Code = code;
    public string Code { get; }
    public static Result<Currency> Create(string? code) =>
        string.IsNullOrWhiteSpace(code) || !Allowed.Contains(code)
            ? Result<Currency>.Failure(new Error("Currency.Invalid", "Moeda obrigatória ou inválida."))
            : Result<Currency>.Success(new Currency(code));
}

public sealed record Money(decimal Amount, Currency Currency)
{
    public static Result<Money> Create(decimal amount, Currency currency, bool allowZero = false, bool allowNegative = false)
    {
        if (!allowZero && amount == 0) return Result<Money>.Failure(new Error("Money.Zero", "Valor deve ser diferente de zero."));
        if (!allowNegative && amount < 0) return Result<Money>.Failure(new Error("Money.Negative", "Valor não pode ser negativo."));
        return Result<Money>.Success(new Money(amount, currency));
    }
}
