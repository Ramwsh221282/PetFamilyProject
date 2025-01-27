using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public sealed record PetAttachments
{
    private readonly List<Photo> _photos = [];
    public IReadOnlyCollection<Photo> Photos => _photos;
}

public sealed record Photo
{
    private static string[] _extensions = [".jpg", ".png", ".bmp", ".PNG"];
    public string Path { get; }
    private Photo(string path) => Path = path;

    public static Result<Photo> Create(string? filePath) =>
        new ResultPipe()
            .Check(string.IsNullOrWhiteSpace(filePath), PhotoErrors.PathWasEmpty)
            .Check(!string.IsNullOrWhiteSpace(filePath) && !IsFileExtensionSupported(filePath), PhotoErrors.UnsupportedPhotoExtensions)
            .FromPipe(new Photo(filePath!));
    private static bool IsFileExtensionSupported(string filePath) => _extensions.Any(filePath.Contains);
}

public static class PhotoErrors
{
    public static Error PathWasEmpty => new Error("Photo path was empty", ErrorStatusCode.BadRequest);
    public static Error UnsupportedPhotoExtensions => new Error("Unsupported photo extension", ErrorStatusCode.BadRequest);
}
