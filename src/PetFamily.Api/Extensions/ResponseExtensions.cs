using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Responses;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Api.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToErrorResponse(this Error error)
    {
        int code = error.ParseErrorCode();
        Envelope envelope = Envelope.ToError(error);
        return new ObjectResult(envelope) { StatusCode = code };
    }

    private static int ParseErrorCode(this Error concreteError) =>
        concreteError.Code switch
        {
            ErrorStatusCode.None => StatusCodes.Status500InternalServerError,
            ErrorStatusCode.BadRequest => StatusCodes.Status400BadRequest,
            ErrorStatusCode.InternalError => StatusCodes.Status500InternalServerError,
            ErrorStatusCode.Unknown => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
}