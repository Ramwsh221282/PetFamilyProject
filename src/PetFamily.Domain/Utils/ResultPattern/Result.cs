namespace PetFamily.Domain.Utils.ResultPattern;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Failure(Error concreteError) => new(concreteError);

    public static implicit operator Result(Error concreteError) => Failure(concreteError);

    public static Result Success() => new();
}

public sealed class Result<TValue> : Result
{
    private readonly TValue _value = default!;

    private Result(TValue value) => _value = value;

    private Result(Error error)
        : base(error) { }

    public new static Result<TValue> Failure(Error concreteError) => new(concreteError);

    public static Result<TValue> Success(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error concreteError) => Failure(concreteError);

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public static implicit operator TValue(Result<TValue> value) => value._value;

    public TValue Value => IsSuccess ? _value : throw new ResultException();
}
