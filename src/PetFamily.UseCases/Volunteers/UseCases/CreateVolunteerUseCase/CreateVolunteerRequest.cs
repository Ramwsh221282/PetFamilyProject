using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.SocialMedia;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared.DTOs;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;

public sealed record CreateVolunteerRequest(
    PersonNameDTO NameDetails,
    ContactsDTO ContactsDetails,
    AccountDetailsDTO AccountDetails,
    DescriptionDTO DescriptionDetails,
    List<SocialMediaDTO>? SocialMediaDetails
);

public sealed record CreateVolunteerResponse(Guid Id);

public sealed class CreateVolunteerRequestHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<CreateVolunteerRequestHandler> _logger;

    public CreateVolunteerRequestHandler(
        IVolunteerRepository repository,
        ILogger<CreateVolunteerRequestHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<CreateVolunteerResponse>> Handle(
        CreateVolunteerRequest request,
        CancellationToken ct = default
    )
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

        if (!await _repository.AreContactsUnique(volunteer, ct))
        {
            Error error = VolunteerErrors.NotUniqueContacts(volunteer.Contacts);
            _logger.LogError("Volunteer was not created. Error: {ErrorMessage}", error.Description);
            return error;
        }

        Result<VolunteerId> insert = await _repository.Add(volunteer, ct);
        if (insert.IsSuccess)
        {
            _logger.LogInformation("Created volunteer. Id: {VolunteerId}", insert.Value.Id);
            return Result<CreateVolunteerResponse>.Success(new(insert.Value.Id));
        }

        _logger.LogError("Volunteer did not create. Error: {Message}", insert.Error.Description);
        return insert.Error;
    }
}
