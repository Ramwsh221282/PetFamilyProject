using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Pet.ValueObjects;

public record PetBodyMetrics
{
    private const double MaxWeight = 500;
    private const double MaxHeight = 500;

    public double Weight { get; }
    public double Height { get; }

    private PetBodyMetrics(double weight, double height)
    {
        Weight = weight;
        Height = height;
    }

    public static Result<PetBodyMetrics> Create(double weight, double heigth) =>
        (weight, heigth) switch
        {
            (<= 0, _) => new Error(
                PetBodyMetricsErrors.WeightIsLessThanZero(),
                ErrorStatusCode.BadRequest
            ),
            (_, <= 0) => new Error(
                PetBodyMetricsErrors.HeightIsLessThanZero(),
                ErrorStatusCode.BadRequest
            ),
            (> MaxWeight, _) => new Error(
                PetBodyMetricsErrors.WeirdWeightError(),
                ErrorStatusCode.BadRequest
            ),
            (_, > MaxHeight) => new Error(
                PetBodyMetricsErrors.WeirdHeightError(),
                ErrorStatusCode.BadRequest
            ),
            _ => new PetBodyMetrics(weight, heigth),
        };
}

public static class PetBodyMetricsErrors
{
    public static string WeightIsLessThanZero() => "Pet weight should be greater than 0";

    public static string HeightIsLessThanZero() => "Pet height should be greater than 0";

    public static string WeirdWeightError() => "Pet weight is weird";

    public static string WeirdHeightError() => "Pet height is weird";
}
