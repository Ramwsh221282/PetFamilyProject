using FluentValidation;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.UseCases.Shared;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerAccountDetailsUseCase;

public sealed class UpdateVolunteerAccountDetailsRequestValidator
    : AbstractValidator<UpdateVolunteerAccountDetailsRequest>
{
    public UpdateVolunteerAccountDetailsRequestValidator()
    {
        RuleFor(req => req.Details.Details)
            .MustSatisfy(det => AccountDetails.Create(det.Name, det.Description));
    }
}
