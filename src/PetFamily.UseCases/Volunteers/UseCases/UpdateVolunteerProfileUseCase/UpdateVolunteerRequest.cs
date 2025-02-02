using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared.DTOs;
using PetFamily.UseCases.Shared.DTOs.VolunteerDTOs;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerProfileUseCase;

public sealed record UpdateVolunteerRequest(
    VolunteerId Id,
    UpdateVolunteerProfileDto UpdateDetails
);

public sealed record UpdateVolunteerResponse(Guid Id);

public sealed class UpdateVolunteerRequestHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerRequestHandler> _logger;

    public UpdateVolunteerRequestHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerRequestHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<UpdateVolunteerResponse>> Handle(
        UpdateVolunteerRequest request,
        CancellationToken ct
    )
    {
        Result<Volunteer> volunteer = await _repository.GetById(request.Id, ct);
        if (volunteer.IsFailure)
        {
            Error error = VolunteerErrors.NotUniqueContacts(volunteer.Value.Contacts);
            _logger.LogError("Volunteer was not updated. Error: {ErrorMessage}", error.Description);
            return error;
        }

        Description newDescription = request.UpdateDetails.Description.ToValueObject();
        ExperienceInYears newExperience = request.UpdateDetails.ExperienceInYears.ToValueObject();
        Contacts newContacts = request.UpdateDetails.Contacts.ToValueObject();
        PersonName newName = request.UpdateDetails.Name.ToValueObject();
        volunteer.Value.UpdateProfile(newDescription, newExperience, newName, newContacts);

        if (!await _repository.AreContactsUnique(volunteer, ct))
        {
            Error error = VolunteerErrors.NotUniqueContacts(volunteer.Value.Contacts);
            _logger.LogError("Volunteer was not updated. Error: {ErrorMessage}", error.Description);
            return VolunteerErrors.NotUniqueContacts(volunteer.Value.Contacts);
        }

        await _repository.Save(volunteer, ct);
        return Result<UpdateVolunteerResponse>.Success(new(volunteer.Value.Id.Id));
    }
}
