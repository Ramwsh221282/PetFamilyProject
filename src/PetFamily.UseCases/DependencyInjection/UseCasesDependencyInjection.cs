using Microsoft.Extensions.DependencyInjection;
using PetFamily.UseCases.Shared;
using PetFamily.UseCases.Volunteers.UseCases.CreateVolunteerUseCase;
using PetFamily.UseCases.Volunteers.UseCases.RemoveVolunteerUseCase;
using PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerAccountDetailsUseCase;
using PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerProfileUseCase;
using PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerSocialMediaUseCase;

namespace PetFamily.UseCases.DependencyInjection;

public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services = services.AddVolunteerUseCases();
        return services;
    }

    private static IServiceCollection AddVolunteerUseCases(this IServiceCollection services)
    {
        services.AddApplicationValidation();
        services.AddCreateVolunteerUseCase();
        services.AddRemoveVolunteerUseCase();
        services.AddUpdateVolunteerProfile();
        services.AddUpdateVolunteerAccountDetailsUseCase();
        services.AddUpdateVolunteerSocialMediaUseCase();
        return services;
    }
}
