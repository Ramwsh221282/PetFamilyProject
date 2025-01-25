using PetFamily.Domain.Volunteer;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.Infrastructure.Repositories.Volunteers;

public sealed class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _context;

    public VolunteerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddVolunteer(Volunteer volunteer)
    {
        await _context.AddAsync(volunteer);
    }
}
