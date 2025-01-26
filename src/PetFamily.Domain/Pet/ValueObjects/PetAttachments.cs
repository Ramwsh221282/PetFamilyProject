using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public sealed record PetAttachments
{
    public List<Photo> Photos { get; } = [];
}

public sealed record Photo
{
    private static string[] _extensions = [".jpg", ".png", ".bmp", ".PNG"];

    public string Path { get; }

    private Photo(string path) => Path = path;

    public static Result<Photo> Create(string filePath) =>
        filePath switch
        {
            null => new Error(PhotoErrors.PathWasNull(), ErrorStatusCode.BadRequest),
            not null when string.IsNullOrWhiteSpace(filePath) => new Error(
                PhotoErrors.PathWasEmpty(),
                ErrorStatusCode.BadRequest
            ),
            not null when !_extensions.Any(filePath.Contains) => new Error(
                PhotoErrors.UnsupportedPhotoExtensions(),
                ErrorStatusCode.BadRequest
            ),
            _ => new Photo(filePath),
        };
}

public static class PhotoErrors
{
    public static string PathWasNull() => "Photo path was null";

    public static string PathWasEmpty() => "Photo path was empty";

    public static string UnsupportedPhotoExtensions() => "Unsupported photo extension";
}
