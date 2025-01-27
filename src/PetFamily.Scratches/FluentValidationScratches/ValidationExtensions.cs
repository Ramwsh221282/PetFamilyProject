using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Scratches.Common.ResultPattern;

namespace PetFamily.Scratches.FluentValidationScratches;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TValidation, TExpected> MustSatisfy<TValidation, TExpected, TValueObject>(
        this IRuleBuilder<TValidation, TExpected> builder, Func<TExpected, Result<TValueObject>> factory) =>
        builder.Must(((_, expected, context) =>
        {
            var result = factory.Invoke(expected);
            if (result.IsSuccess)
                return true;
            ValidationFailure failure = new ValidationFailure()
            {
                ErrorMessage = result.Error.Description,
                ErrorCode = result.Error.Code.ToString()
            };
            context.AddFailure(failure);
            return false;
        })).WithMessage(((_, expected) =>
        {
            var result = factory.Invoke(expected);
            return result.IsSuccess ? "" : result.Error.Description;
        }));

    public static IServiceCollection AddValidation(this IServiceCollection collection) =>
        collection.AddValidatorsFromAssembly(typeof(ValidationExtensions).Assembly);
}