using FluentValidation;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerProfileUseCase;

public sealed class UpdateVolunteerRequestValidator : AbstractValidator<UpdateVolunteerRequest>
{
    public UpdateVolunteerRequestValidator()
    {
        RuleFor(req => req.UpdateDetails.Name)
            .MustSatisfy(n => PersonName.Create(n.Name, n.Surname, n.Patronymic));

        RuleFor(req => req.UpdateDetails.Contacts)
            .MustSatisfy(c => Contacts.Create(c.Phone, c.Email));

        RuleFor(req => req.UpdateDetails.Description).MustSatisfy(d => Description.Create(d.Text));

        RuleFor(req => req.UpdateDetails.ExperienceInYears)
            .MustSatisfy(exp => ExperienceInYears.Create(exp.Experience));
    }
}
