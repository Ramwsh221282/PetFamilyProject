using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.UseCases.Volunteers.Contracts;

public interface IVolunteerRepository
{
    Task<Result<VolunteerId>> AddVolunteer(Volunteer volunteer, CancellationToken ct = default);
    Task<Result<VolunteerId>> RemoveVolunteer(VolunteerId id, CancellationToken ct = default);
}
