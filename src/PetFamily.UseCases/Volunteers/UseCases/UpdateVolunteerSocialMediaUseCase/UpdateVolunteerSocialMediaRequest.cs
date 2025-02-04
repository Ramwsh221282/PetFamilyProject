using Microsoft.Extensions.Logging;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared.DTOs;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerSocialMediaUseCase;

public sealed record UpdateVolunteerSocialMediaRequest(
    VolunteerId Id,
    UpdateVolunteerSocialMediaDto Details
);

public sealed record UpdateVolunteerSocialMediaResponse(Guid Id);

public sealed class UpdateVolunteerSocialMediaRequestHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateVolunteerSocialMediaRequestHandler> _logger;

    public UpdateVolunteerSocialMediaRequestHandler(
        IVolunteerRepository repository,
        ILogger<UpdateVolunteerSocialMediaRequestHandler> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<UpdateVolunteerSocialMediaResponse>> Handle(
        UpdateVolunteerSocialMediaRequest request,
        CancellationToken ct = default
    )
    {
        Result<Volunteer> volunteer = await _repository.GetById(request.Id, ct);
        if (volunteer.IsFailure)
        {
            Error error = volunteer.Error;
            _logger.LogError(
                "Update volunteer social media canceled. Error: {ErrorMessage}",
                error.Description
            );
            return volunteer.Error;
        }

        volunteer.Value.CleanUpdateSocialMedia(
            request.Details.Media.Select(m => m.ToValueObject())
        );
        await _repository.Save(volunteer, ct);

        _logger.LogInformation("Volunteer social media updated. Id: {VolId}", request.Id.Id);
        return Result<UpdateVolunteerSocialMediaResponse>.Success(new(request.Id.Id));
    }
}
