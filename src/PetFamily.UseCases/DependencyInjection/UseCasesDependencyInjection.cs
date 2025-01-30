using Microsoft.Extensions.DependencyInjection;
using PetFamily.UseCases.Shared;
using PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;
using PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;

namespace PetFamily.UseCases.DependencyInjection;

public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddApplicationValidation();
        services.AddCreateVolunteerUseCase();
        services.AddRemoveVolunteerUseCase();
        return services;
    }
}
