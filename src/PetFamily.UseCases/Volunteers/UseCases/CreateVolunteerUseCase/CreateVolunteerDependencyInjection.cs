using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;

internal static class CreateVolunteerDependencyInjection
{
    internal static IServiceCollection AddCreateVolunteerUseCase(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerRequestHandler>();
        return services;
    }
}
