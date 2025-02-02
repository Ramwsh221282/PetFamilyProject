using FluentValidation.Results;
using PetFamily.Api.Responses;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Api.Extensions;

public static class ResponseExtensions
{
    public static IResult FromResult<TResult>(this Result<TResult> result)
    {
        if (result.IsSuccess)
            return TypedResults.Ok(Envelope.Ok(result.Value));
        return result.Error.StatusCode switch
        {
            ErrorStatusCode.BadRequest => TypedResults.BadRequest(Envelope.ToError(result)),
            ErrorStatusCode.NotFound => TypedResults.NotFound(Envelope.ToError(result)),
            ErrorStatusCode.InternalError => TypedResults.BadRequest(Envelope.ToError(result)),
            ErrorStatusCode.Unknown => TypedResults.BadRequest(Envelope.ToError(result)),
            _ => throw new ApplicationException($"Unknown error occured."),
        };
    }

    public static IResult FromResult(this ValidationResult result)
    {
        if (result.IsValid)
            return TypedResults.Ok();
        return TypedResults.BadRequest(Envelope.ToError(result));
    }
}
