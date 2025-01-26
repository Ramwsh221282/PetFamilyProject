using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public enum PetHelpStatuses
{
    RequiresHelp = 0,
    LookingForHouse = 1,
    ReceivedHelp = 2,
}

public abstract record PetHelpStatus
{
    public string StatusText;
    public PetHelpStatuses StatusCode;

    protected PetHelpStatus(string statusText, PetHelpStatuses statusCode)
    {
        StatusText = statusText;
        StatusCode = statusCode;
    }

    public static Result<PetHelpStatus> Create(int status) =>
        status switch
        {
            (int)PetHelpStatuses.RequiresHelp => new RequiresHelp(PetHelpStatuses.RequiresHelp),
            (int)PetHelpStatuses.ReceivedHelp => new ReceivedHelp(PetHelpStatuses.ReceivedHelp),
            (int)PetHelpStatuses.LookingForHouse => new LookingForHouse(
                PetHelpStatuses.LookingForHouse
            ),
            _ => new Error(PetHelpStatusErrors.UnknownPetHelpStatus(), ErrorStatusCode.BadRequest),
        };
}

public sealed record RequiresHelp : PetHelpStatus
{
    internal RequiresHelp(PetHelpStatuses statusCode)
        : base("Currently pet requires a help", statusCode) { }
}

public sealed record LookingForHouse : PetHelpStatus
{
    internal LookingForHouse(PetHelpStatuses statusCode)
        : base("Currently pet is looking for a house", statusCode) { }
}

public sealed record ReceivedHelp : PetHelpStatus
{
    internal ReceivedHelp(PetHelpStatuses statusCode)
        : base("Pet received a help", statusCode) { }
}

public static class PetHelpStatusErrors
{
    public static string UnknownPetHelpStatus() => "Pet help status is unknown";
}
