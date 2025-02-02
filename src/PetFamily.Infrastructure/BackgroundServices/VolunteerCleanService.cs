using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFamily.Infrastructure.Options;

namespace PetFamily.Infrastructure.BackgroundServices;

public sealed class VolunteerCleanService : BackgroundService
{
    private readonly ILogger<VolunteerCleanService> _logger;
    private readonly IServiceScopeFactory _factory;
    private readonly VolunteerCleanDbOptions _options;

    public VolunteerCleanService(
        ILogger<VolunteerCleanService> logger,
        IServiceScopeFactory factory,
        VolunteerCleanDbOptions options
    )
    {
        _logger = logger;
        _factory = factory;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Service: Clean DB Volunteers service started");
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _factory.CreateScope();
            ApplicationDbContext context =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            DateOnly current = DateOnly.FromDateTime(DateTime.Now);

            int removed = await context
                .Volunteers.Where(vol =>
                    vol.IsDeleted
                    && vol.DeletedOn != null
                    && vol.DeletedOn.Value.AddDays(_options.MaxLifeLimitDays) >= current
                )
                .ExecuteDeleteAsync(stoppingToken);

            _logger.LogInformation($"Background Service: Removed {removed} volunteer entries");
            await Task.Delay(TimeSpan.FromHours(_options.RepeatEveryHours), stoppingToken);
        }
        await Task.CompletedTask;
    }
}
