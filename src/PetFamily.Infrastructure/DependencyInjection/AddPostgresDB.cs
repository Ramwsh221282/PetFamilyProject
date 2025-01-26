using Microsoft.Extensions.DependencyInjection;
using PetFamily.Infrastructure.Repositories.Volunteers;
using PetFamily.UseCases.Volunteers.Contracts;

namespace PetFamily.Infrastructure.DependencyInjection;

public static class AddPostgresDB
{
    public static IServiceCollection AddPostgresDbServices(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        return services;
    }
}
