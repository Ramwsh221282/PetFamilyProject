namespace PetFamily.Domain.Utils.ResultPattern;

public sealed record Error(string Description, ErrorStatusCode Code)
{
    public static Error None = new("", ErrorStatusCode.None);
}

public enum ErrorStatusCode
{
    None,
    BadRequest,
    InternalError,
    Unknown,
}
