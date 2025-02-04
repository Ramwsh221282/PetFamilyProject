using Microsoft.Extensions.DependencyInjection;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerAccountDetailsUseCase;

internal static class UpdateVolunteerAccountDetailsDependencyInjection
{
    public static IServiceCollection AddUpdateVolunteerAccountDetailsUseCase(
        this IServiceCollection services
    )
    {
        services.AddScoped<UpdateVolunteerAccountDetailsRequestHandler>();
        return services;
    }
}
