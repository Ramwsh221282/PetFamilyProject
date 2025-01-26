using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Api.Responses;

public record  Envelope
{
    public object? Result { get; }
    public string? Error { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result = null, string? error = null)
    {
        Result = result;
        Error = error;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null) => new Envelope(result);
    public static Envelope ToError(Error error) => new Envelope(error: error.Description);
}