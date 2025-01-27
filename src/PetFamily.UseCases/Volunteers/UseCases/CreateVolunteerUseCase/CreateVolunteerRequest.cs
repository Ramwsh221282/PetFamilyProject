using PetFamily.Domain.Shared.SocialMedia;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared.DTOs;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;

public record CreateVolunteerRequest(
    PersonNameDTO NameDetails,
    ContactsDTO ContactsDetails,
    AccountDetailsDTO AccountDetails,
    DescriptionDTO DescriptionDetails,
    List<SocialMediaDTO>? SocialMediaDetails
);

public sealed class CreateVolunteerRequestHandler
{
    private readonly IVolunteerRepository _repository;

    public CreateVolunteerRequestHandler(
        IVolunteerRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateVolunteerRequest request)
    {
        Contacts contacts = request.ContactsDetails.ToValueObject();
        PersonName name = request.NameDetails.ToValueObject();
        AccountDetails account = request.AccountDetails.ToValueObject();
        Description description = request.DescriptionDetails.ToValueObject();
        SocialMedia[] media = request.SocialMediaDetails.ToValueObject();
        ExperienceInYears experience = ExperienceInYears.NoExperience;
        Volunteer volunteer = new Volunteer(
            contacts,
            name,
            description,
            experience,
            account,
            media
        );
        //await _repository.AddVolunteer(volunteer);
        return volunteer.Id.Id;
    }
}
