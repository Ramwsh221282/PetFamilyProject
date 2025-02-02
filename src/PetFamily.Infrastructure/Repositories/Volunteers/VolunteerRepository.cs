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

    public async Task<Result<VolunteerId>> Add(Volunteer volunteer, CancellationToken ct = default)
    {
        await _context.AddAsync(volunteer, ct);
        await _context.SaveChangesAsync(ct);
        return volunteer.Id;
    }

    public async Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken ct = default)
    {
        Volunteer? requested = await _context
            .Volunteers.Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == id, ct);
        return requested == null ? VolunteerErrors.NotFoundWithId(id) : requested;
    }

    public async Task<VolunteerId> Save(Volunteer volunteer, CancellationToken ct = default)
    {
        _context.Volunteers.Attach(volunteer);
        await _context.SaveChangesAsync(ct);
        return volunteer.Id;
    }

    public async Task<bool> AreContactsUnique(
        Volunteer volunteer,
        CancellationToken ct = default
    ) =>
        !await _context.Volunteers.AnyAsync(
            v =>
                v.Contacts.Email == volunteer.Contacts.Email
                || v.Contacts.Phone == volunteer.Contacts.Phone,
            ct
        );
}
