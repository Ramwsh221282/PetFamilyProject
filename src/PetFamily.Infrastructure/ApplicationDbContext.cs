using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteer;
using PetFamily.Infrastructure.Configurations;

namespace PetFamily.Infrastructure;

public sealed class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    private const string PostgresDb = nameof(PostgresDb);

    public DbSet<Volunteer> Volunteers { get; set; } = null!;
    public DbSet<Specie> Species { get; set; } = null!;

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
            .ApplyConfiguration(new PetConfiguration())
            .ApplyConfiguration(new BreedConfiguration());

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
}
