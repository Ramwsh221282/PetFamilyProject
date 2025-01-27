namespace PetFamily.Domain.Utils.ResultPattern;

public sealed record Error(string Description, ErrorStatusCode StatusCode)
{
    public static Error None => new Error("", ErrorStatusCode.Unknown);
};

public enum ErrorStatusCode
{
    BadRequest,
    InternalError,
    Unknown,
}
