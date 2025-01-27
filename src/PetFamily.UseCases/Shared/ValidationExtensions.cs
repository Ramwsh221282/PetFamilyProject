using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.UseCases.Shared;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TValidation, TExpected> MustSatisfy<TValidation, TExpected, TValueObject>(
        this IRuleBuilder<TValidation, TExpected> builder, Func<TExpected, Result<TValueObject>> factory) =>
        builder.Must(((_, expected) =>
        {
            var result = factory.Invoke(expected);
            Type type = typeof(TValueObject);
            string name = type.Name;
            if (result.IsSuccess)
            {
                Console.WriteLine($"Success validation of: {name}");
                return true;
            }
            // ValidationFailure failure = new ValidationFailure()
            // {
            //     ErrorMessage = result.Error.Description,
            //     ErrorCode = result.Error.StatusCode.ToString()
            // };
            // context.AddFailure(failure);
            Console.WriteLine($"Failed validation of: {name}");
            return false;
        })).WithMessage(((_, expected) =>
        {
            var result = factory.Invoke(expected);
            return result.IsSuccess ? "" : result.Error.Description;
        }));

    public static IServiceCollection AddValidation(this IServiceCollection collection) =>
        collection.AddValidatorsFromAssembly(typeof(ValidationExtensions).Assembly);

    internal static IServiceCollection AddApplicationValidation(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(typeof(ValidationExtensions).Assembly);
    }
}
