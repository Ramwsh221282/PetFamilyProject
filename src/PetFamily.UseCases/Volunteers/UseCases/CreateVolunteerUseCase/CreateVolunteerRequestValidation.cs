using FluentValidation;
using PetFamily.Domain.Shared.SocialMedia.ValueObjects;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.UseCases.Shared;

namespace PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;

public sealed class CreateVolunteerRequestValidation : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidation()
    {
        RuleFor(r => r.NameDetails)
            .MustSatisfy(d => PersonName.Create(d.Name, d.Surname, d.Patronymic));

        RuleFor(r => r.ContactsDetails).MustSatisfy(d => Contacts.Create(d.Phone, d.Email));

        RuleFor(r => r.AccountDetails)
            .MustSatisfy(d => AccountDetails.Create(d.Description, d.Name));

        RuleFor(r => r.DescriptionDetails).MustSatisfy(d => Description.Create(d.Text));

        RuleFor(r => r.SocialMediaDetails)
            .Must(media =>
            {
                if (media is null)
                    return true;
                return !media.Any(m =>
                    SocialMediaName.Create(m.Name).IsFailure
                    && SocialMediaUrl.Create(m.Url).IsFailure
                );
            });
    }
}
