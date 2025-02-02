namespace PetFamily.Domain.Utils.IdUtils.Implementations;

public class FromStringGuidGenerationStrategy : IGuidGenerationStrategy
{
    private readonly string _input;

    public FromStringGuidGenerationStrategy(string input) => _input = input;

    public Guid Generate()
    {
        if (string.IsNullOrWhiteSpace(_input))
            return Guid.Empty;
        bool conversion = Guid.TryParse(_input, out Guid id);
        return !conversion ? Guid.Empty : id;
    }
}
