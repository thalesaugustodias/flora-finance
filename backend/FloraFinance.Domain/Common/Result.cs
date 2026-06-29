namespace FloraFinance.Domain.Common;

public sealed record Error(string Code, string Message);

public class Result
{
    protected Result(bool isSuccess, IReadOnlyList<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<Error> Errors { get; }
    public static Result Success() => new(true, []);
    public static Result Failure(params Error[] errors) => new(false, errors);
}

public sealed class Result<T> : Result
{
    private Result(T value) : base(true, []) => Value = value;
    private Result(IReadOnlyList<Error> errors) : base(false, errors) => Value = default;
    public T? Value { get; }
    public static Result<T> Success(T value) => new(value);
    public static new Result<T> Failure(params Error[] errors) => new(errors);
}
