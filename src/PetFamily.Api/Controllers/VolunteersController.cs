using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;
using PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;
using PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerAccountDetailsUseCase;
using PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerProfileUseCase;
using PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerSocialMediaUseCase;

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
            return validation.FromResult();
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
            return validation.FromResult();
        Result<RemoveVolunteerResponse> deletion = await handler.Handle(request, ct);
        return deletion.FromResult();
    }

    [HttpPatch("{id}/profile")]
    public async Task<IResult> UpdateProfile(
        [FromServices] UpdateVolunteerRequestHandler handler,
        [FromServices] IValidator<UpdateVolunteerRequest> validator,
        [FromRoute] string id,
        [FromBody] UpdateVolunteerProfileDto updateDetails,
        CancellationToken ct = default
    )
    {
        VolunteerId requestedId = new VolunteerId(new FromStringGuidGenerationStrategy(id));
        UpdateVolunteerRequest request = new UpdateVolunteerRequest(requestedId, updateDetails);
        ValidationResult validation = await validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return validation.FromResult();
        Result<UpdateVolunteerResponse> update = await handler.Handle(request, ct);
        return update.FromResult();
    }

    [HttpPatch("{id}/profile/social-media")]
    public async Task<IResult> UpdateSocialMedia(
        [FromServices] UpdateVolunteerSocialMediaRequestHandler handler,
        [FromServices] UpdateVolunteerSocialMediaRequestValidator validator,
        [FromRoute] string id,
        [FromBody] UpdateVolunteerSocialMediaDto updateDetails,
        CancellationToken ct = default
    )
    {
        VolunteerId requestedId = new(new FromStringGuidGenerationStrategy(id));
        UpdateVolunteerSocialMediaRequest request = new(requestedId, updateDetails);
        ValidationResult validation = await validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return validation.FromResult();
        Result<UpdateVolunteerSocialMediaResponse> update = await handler.Handle(request, ct);
        return update.FromResult();
    }

    [HttpPatch("{id}/profile/account-details")]
    public async Task<IResult> UpdateAccountDetails(
        [FromServices] UpdateVolunteerAccountDetailsRequestHandler handler,
        [FromServices] UpdateVolunteerAccountDetailsRequestValidator validator,
        [FromRoute] string id,
        [FromBody] UpdateVolunteerAccountDetailsDto updateDetails,
        CancellationToken ct = default
    )
    {
        VolunteerId requestedId = new(new FromStringGuidGenerationStrategy(id));
        UpdateVolunteerAccountDetailsRequest request = new(requestedId, updateDetails);
        ValidationResult validation = await validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return validation.FromResult();
        Result<UpdateVolunteerAccountDetailsResponse> update = await handler.Handle(request, ct);
        return update.FromResult();
    }
}
