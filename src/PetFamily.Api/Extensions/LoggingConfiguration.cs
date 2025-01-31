using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Debugging;

namespace PetFamily.Api.Extensions;

public static class LoggingConfiguration
{
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        var seqUrl = builder.Configuration.GetSection("Seq").GetSection("ServerUrl").Value;
        if (string.IsNullOrWhiteSpace(seqUrl))
            throw new LoggingFailedException("Seq connection url was not found");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt")
            .WriteTo.Seq(seqUrl)
            .CreateLogger();
        builder.Services.AddLogging();
        builder.Services.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.All;
            options.MediaTypeOptions.AddText("application/javascript");
            options.CombineLogs = true;
        });
        builder.Services.AddSerilog();
    }
}
