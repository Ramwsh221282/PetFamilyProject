namespace PetFamily.Domain.Utils.ResultPattern;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; } = Error.None;

    protected Result()
    {
        IsSuccess = true;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Failure(Error error) => new(error);

    public static implicit operator Result(Error error) => Failure(error);

    public static Result Success() => new();
}

public sealed class Result<TValue> : Result
{
    private readonly TValue _value = default!;

    private Result(TValue value) => _value = value;

    private Result(Error error)
        : base(error) { }

    public static new Result<TValue> Failure(Error error) => new(error);

    public static Result<TValue> Success(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error) => Failure(error);

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public static implicit operator TValue(Result<TValue>? value) => value._value;

    public TValue Value => IsSuccess ? _value : throw new ResultException();
}
