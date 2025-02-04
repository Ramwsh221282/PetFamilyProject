using FluentValidation.Results;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Api.Responses;

public record Envelope
{
    public object? Result { get; }
    public List<string> Errors { get; } = [];
    public DateTime TimeGenerated { get; }

    private Envelope(object? result = null)
    {
        Result = result;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null) => new Envelope(result);

    public static Envelope ToError(ValidationResult validation)
    {
        if (validation.IsValid)
            throw new ApplicationException();

        Envelope envelope = new Envelope();
        validation.Errors.ForEach((err) => envelope.Errors.Add(err.ErrorMessage));
        return envelope;
    }

    public static Envelope ToError<T>(Result<T> failed)
    {
        if (failed.IsSuccess)
            throw new ApplicationException();

        Envelope envelope = new Envelope();
        envelope.Errors.Add(failed.Error.Description);
        return envelope;
    }
}
