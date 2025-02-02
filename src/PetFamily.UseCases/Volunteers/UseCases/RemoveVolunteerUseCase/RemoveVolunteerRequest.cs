using Microsoft.Extensions.Logging;
using PetFamily.Domain.Utils.IdUtils.Implementations;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared.DTOs.VolunteerDTOs;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;

public sealed record RemoveVolunteerRequest(Guid Id);

public sealed record RemoveVolunteerResponse(bool IsRemoved);

public sealed class RemoveVolunteerRequestHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<RemoveVolunteerRequest> _logger;

    public RemoveVolunteerRequestHandler(
        IVolunteerRepository repository,
        ILogger<RemoveVolunteerRequest> logger
    )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RemoveVolunteerResponse>> Handle(
        RemoveVolunteerRequest request,
        CancellationToken ct = default
    )
    {
        VolunteerId id = new VolunteerId(new AdjustedGuidGenerationStrategy(request.Id));
        Result<Volunteer> volunteer = await _repository.GetById(id, ct);
        if (volunteer.IsFailure)
        {
            Error error = volunteer.Error;
            _logger.LogError("Volunteer did not remove. Error: {ErrorMessage}", error.Description);
            return volunteer.Error;
        }
        volunteer.Value.Delete();
        await _repository.Save(volunteer.Value, ct);
        _logger.LogInformation("Volunteer marked for remove. Id: {VolunteerId}", id);
        return Result<RemoveVolunteerResponse>.Success(new(true));
    }
}
