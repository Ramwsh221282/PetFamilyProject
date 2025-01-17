namespace PetFamily.Domain.Pet.ValueObjects;

public abstract record PetHealthStatus
{
    public bool IsVaccinated { get; }
    public bool IsCastrated { get; }

    protected PetHealthStatus(bool isVaccinated, bool isCastrated)
    {
        IsVaccinated = isVaccinated;
        IsCastrated = isCastrated;
    }

    public static PetHealthStatus Create(bool isVaccinated, bool isCastrated) =>
        (isCastrated, isCastrated) switch
        {
            (true, true) => new VaccinatedAndCastrated(),
            (false, true) => new CastratedOnly(),
            (true, false) => new VaccinatedOnly(),
            _ => new NeitherVaccinatedNorCastrated(),
        };
}

public sealed record VaccinatedOnly : PetHealthStatus
{
    internal VaccinatedOnly()
        : base(true, false) { }
}

public sealed record CastratedOnly : PetHealthStatus
{
    internal CastratedOnly()
        : base(false, true) { }
}

public sealed record VaccinatedAndCastrated : PetHealthStatus
{
    internal VaccinatedAndCastrated()
        : base(true, true) { }
}

public sealed record NeitherVaccinatedNorCastrated : PetHealthStatus
{
    internal NeitherVaccinatedNorCastrated()
        : base(false, false) { }
}
