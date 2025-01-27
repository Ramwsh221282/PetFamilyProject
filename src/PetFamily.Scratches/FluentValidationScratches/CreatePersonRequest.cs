using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace PetFamily.Scratches.FluentValidationScratches;

public record PersonNameDto(string FirstName, string LastName, string? Patronymic);
public record ContactsDto(string Email);
public record CreatePersonRequest(PersonNameDto Name, ContactsDto Contacts);

public sealed class CreatePersonRequestHandler
{
    private readonly IValidator<CreatePersonRequest> _validator;
    
    public CreatePersonRequestHandler(IValidator<CreatePersonRequest> validator) => _validator = validator;
    
    public void Handle(CreatePersonRequest request)
    {
        ValidationResult result = _validator.Validate(request);
        if (!result.IsValid)
        {
            int bpoint = 0;
            return;
        }
        Console.WriteLine("Created person");
    }
}

public static class CreatePersonRequestExtensions
{
    public static IServiceCollection AddCreatePersonRequestHandler(this IServiceCollection services)
    {
        return services.AddTransient<CreatePersonRequestHandler>();
    }
}