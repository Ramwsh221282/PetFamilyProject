using PetFamily.Domain.Volunteer;

namespace PetFamily.UseCases.Volunteers.Contracts;

public interface IVolunteerRepository
{
    Task AddVolunteer(Volunteer volunteer);
}
