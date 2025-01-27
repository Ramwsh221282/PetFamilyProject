using FluentValidation;

namespace PetFamily.Scratches.FluentValidationScratches;

public sealed class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
{
    public CreatePersonValidator()
    {
        RuleFor(req => req.Name).MustSatisfy(req => PersonName.Create(req.FirstName, req.LastName, req.FirstName));
        RuleFor(req => req.Contacts).MustSatisfy(req => Contacts.Create(req.Email));
    }
}