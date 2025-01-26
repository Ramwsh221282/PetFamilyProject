using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.UseCases.Shared;

public static class ValidationExtensions
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustSatisfy<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject>> validationFunc
    )
    {
        return ruleBuilder.Custom(
            (value, context) =>
            {
                Result<TValueObject> result = validationFunc.Invoke(value);
                if (result.IsSuccess)
                    return;
                Error error = result.Error;
                ValidationFailure failure = new ValidationFailure()
                {
                    ErrorMessage = error.Description,
                    ErrorCode = error.Code.ToString(),
                };
                context.AddFailure(failure);
            }
        );
    }

    public static Result<T> ToFailureResult<T>(this ValidationResult result)
    {
        ValidationFailure failure = result.Errors[0];
        bool isParsed = Enum.TryParse(failure.ErrorCode, out ErrorStatusCode code);
        return isParsed
            ? new Error(failure.ErrorMessage, code)
            : new Error(failure.ErrorMessage, ErrorStatusCode.Unknown);
    }

    internal static IServiceCollection AddApplicationValidation(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(typeof(ValidationExtensions).Assembly);
    }
}
