using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerSocialMediaUseCase;

internal static class UpdateVolunteerUseCaseDependencyInjection
{
    public static IServiceCollection AddUpdateVolunteerSocialMediaUseCase(
        this IServiceCollection services
    )
    {
        services.AddScoped<UpdateVolunteerSocialMediaRequestHandler>();
        return services;
    }
}
