using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Utils.ResultPattern;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.ValueObjects;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.Infrastructure.Repositories.Volunteers;

public sealed class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _context;

    public VolunteerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<VolunteerId>> AddVolunteer(
        Volunteer volunteer,
        CancellationToken ct = default
    )
    {
        await _context.AddAsync(volunteer, ct);
        await _context.SaveChangesAsync(ct);
        return volunteer.Id;
    }

    public async Task<Result<VolunteerId>> RemoveVolunteer(
        VolunteerId id,
        CancellationToken ct = default
    )
    {
        int deleted = await _context.Volunteers.Where(v => v.Id == id).ExecuteDeleteAsync(ct);
        if (deleted == 0)
            return VolunteerErrors.NotFoundWithId(id);
        return id;
    }
}
