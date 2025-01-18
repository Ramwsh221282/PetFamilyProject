using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Infrastructure.Configurations;

namespace PetFamily.Infrastructure;

public sealed class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    private const string PostgresDb = nameof(PostgresDb);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(PostgresDb));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
            .ApplyConfiguration(new VolunteerConfiguration())
            .ApplyConfiguration(new SpecieConfiguration())
            .ApplyConfiguration(new SocialMediaConfiguration())
            .ApplyConfiguration(new PetConfiguration())
            .ApplyConfiguration(new BreedConfiguration());

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
}
