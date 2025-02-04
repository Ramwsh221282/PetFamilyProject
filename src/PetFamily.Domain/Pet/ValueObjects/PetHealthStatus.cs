namespace PetFamily.Domain.Pet.ValueObjects;

public record PetHealthStatus
{
    public bool IsVaccinated { get; }
    public bool IsCastrated { get; }

    public PetHealthStatus(bool isVaccinated, bool isCastrated)
    {
        IsVaccinated = isVaccinated;
        IsCastrated = isCastrated;
    }
}
