using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerProfileUseCase;

internal static class UpdateVolunteerDependencyInjection
{
    public static IServiceCollection AddUpdateVolunteerProfile(this IServiceCollection services)
    {
        services.AddScoped<UpdateVolunteerRequestHandler>();
        return services;
    }
}
