using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;

namespace PetFamily.UseCases.Volunteers.Contracts;

public interface IVolunteerRepository
{
    Task<Result<VolunteerId>> Add(Volunteer volunteer, CancellationToken ct = default);
    Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken ct = default);
    Task<VolunteerId> Save(Volunteer volunteer, CancellationToken ct = default);
    Task<bool> AreContactsUnique(Volunteer volunteer, CancellationToken ct = default);
}
