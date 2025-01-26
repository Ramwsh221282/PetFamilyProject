using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Responses;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;

namespace PetFamily.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolunteersController : Controller
{
    [HttpPost]
    public async Task<Results<BadRequest<Envelope>, Ok<Envelope>>> Create(
        [FromServices] CreateVolunteerRequestHandler handler,
        [FromBody] CreateVolunteerRequest request
    )
    {
        Result<Guid> result = await handler.Handle(request);
        return result.IsFailure
            ? TypedResults.BadRequest(Envelope.ToError(result.Error))
            : TypedResults.Ok(Envelope.Ok(result.Value));
    }
}
