using Microsoft.Extensions.Logging;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Shared.DTOs.VolunteerDTOs;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;

public sealed record RemoveVolunteerRequest(VolunteerIdDTO Id);

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
        VolunteerId id = request.Id.ToValueObject();
        Result<VolunteerId> operation = await _repository.RemoveVolunteer(id, ct);
        if (operation.IsSuccess)
        {
            _logger.LogInformation("Removed volunteer. Id: {VolunteerId}", operation.Value);
            return Result<RemoveVolunteerResponse>.Success(new(true));
        }
        _logger.LogError("Volunteer did not remove. Error: {ErrorMessage}", operation.Error);
        return operation.Error;
    }
}
