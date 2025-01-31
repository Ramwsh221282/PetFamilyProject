using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;

public static class RemoveVolunteerRequestDependencyInjection
{
    public static IServiceCollection AddRemoveVolunteerUseCase(this IServiceCollection services)
    {
        services.AddScoped<RemoveVolunteerRequestHandler>();
        return services;
    }
}