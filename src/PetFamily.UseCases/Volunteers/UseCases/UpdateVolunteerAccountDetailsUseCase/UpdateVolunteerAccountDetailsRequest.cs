using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared.DTOs;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerAccountDetailsUseCase;

public sealed record UpdateVolunteerAccountDetailsRequest(
    VolunteerId Id,
    UpdateVolunteerAccountDetailsDto Details
);

public sealed record UpdateVolunteerAccountDetailsResponse(Guid Id);

public sealed class UpdateVolunteerAccountDetailsRequestHandler
{
    private readonly ILogger<UpdateVolunteerAccountDetailsRequestHandler> _logger;
    private readonly IVolunteerRepository _repository;

    public UpdateVolunteerAccountDetailsRequestHandler(
        ILogger<UpdateVolunteerAccountDetailsRequestHandler> logger,
        IVolunteerRepository repository
    )
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<UpdateVolunteerAccountDetailsResponse>> Handle(
        UpdateVolunteerAccountDetailsRequest request,
        CancellationToken ct = default
    )
    {
        Result<Volunteer> volunteer = await _repository.GetById(request.Id);
        if (volunteer.IsFailure)
        {
            Error error = volunteer.Error;
            _logger.LogError(
                "Volunteer account details was not updated. Error: {Message}",
                error.Description
            );
            return volunteer.Error;
        }

        AccountDetails newDetails = request.Details.Details.ToValueObject();
        volunteer.Value.CleanUpdateAccountDetails(newDetails);
        await _repository.Save(volunteer);

        _logger.LogInformation("Volunteer account details was updated. Id: {VolId}", request.Id);
        return Result<UpdateVolunteerAccountDetailsResponse>.Success(new(request.Id.Id));
    }
}
