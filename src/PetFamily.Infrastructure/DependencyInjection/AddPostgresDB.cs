using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PetFamily.Infrastructure.Options;
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

    public static IServiceCollection AddVolunteerCleanDbOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<VolunteerCleanDbOptions>(
            configuration.GetSection(VolunteerCleanDbOptions.VolunteerCleanDb)
        );
        IServiceProvider provider = services.BuildServiceProvider();
        IOptions<VolunteerCleanDbOptions> options = provider.GetRequiredService<
            IOptions<VolunteerCleanDbOptions>
        >();
        services.AddSingleton(options.Value);
        return services;
    }
}
