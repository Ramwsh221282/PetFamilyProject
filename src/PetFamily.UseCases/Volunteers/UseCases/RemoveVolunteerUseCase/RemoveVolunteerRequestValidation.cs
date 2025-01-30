using FluentValidation;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;

public class RemoveVolunteerRequestValidation : AbstractValidator<RemoveVolunteerRequest>
{
    public RemoveVolunteerRequestValidation()
    {
        RuleFor(req => req.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Volunteer Id cannot be empty")
            .WithErrorCode(ErrorStatusCode.BadRequest.ToString());
    }
}