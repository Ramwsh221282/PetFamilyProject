using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Api.Responses;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;
using PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;

namespace PetFamily.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolunteersController : Controller
{
    [HttpPost]
    public async Task<IResult> Create(
        [FromServices] CreateVolunteerRequestHandler handler,
        [FromServices] IValidator<CreateVolunteerRequest> validator,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken ct = default
    )
    {
        ValidationResult validation = await validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return TypedResults.BadRequest(Envelope.ToError(validation));
        Result<CreateVolunteerResponse> creation = await handler.Handle(request, ct);
        return creation.FromResult();
    }

    [HttpDelete]
    public async Task<IResult> Delete(
        [FromServices] RemoveVolunteerRequestHandler handler,
        [FromServices] IValidator<RemoveVolunteerRequest> validator,
        [FromBody] RemoveVolunteerRequest request,
        CancellationToken ct = default
    )
    {
        ValidationResult validation = await validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return TypedResults.BadRequest(Envelope.ToError(validation));
        Result<RemoveVolunteerResponse> deletion = await handler.Handle(request, ct);
        return deletion.FromResult();
    }
}
