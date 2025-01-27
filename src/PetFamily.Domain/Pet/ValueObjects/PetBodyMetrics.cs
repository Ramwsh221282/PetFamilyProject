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
        new ResultPipe()
            .Check(weight <= 0, PetBodyMetricsErrors.WeightIsLessThanZero)
            .Check(heigth <= 0, PetBodyMetricsErrors.HeightIsLessThanZero)
            .Check(weight > MaxWeight, PetBodyMetricsErrors.WeirdWeightError)
            .Check(heigth > MaxHeight, PetBodyMetricsErrors.WeirdHeightError)
            .FromPipe(new PetBodyMetrics(weight, heigth));
}

public static class PetBodyMetricsErrors
{
    public static Error WeightIsLessThanZero => new("Pet weight should be greater than 0", ErrorStatusCode.BadRequest);
    public static Error HeightIsLessThanZero => new("Pet height should be greater than 0", ErrorStatusCode.BadRequest);
    public static Error WeirdWeightError => new("Pet weight is weird", ErrorStatusCode.BadRequest);
    public static Error WeirdHeightError => new("Pet height is weird", ErrorStatusCode.BadRequest);
}
