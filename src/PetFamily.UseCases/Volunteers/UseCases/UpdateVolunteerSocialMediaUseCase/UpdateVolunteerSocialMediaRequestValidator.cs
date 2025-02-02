using FluentValidation;
using PetFamily.Domain.Shared.SocialMedia.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.UseCases.Volunteers.UseCases.UpdateVolunteerSocialMediaUseCase;

public sealed class UpdateVolunteerSocialMediaRequestValidator
    : AbstractValidator<UpdateVolunteerSocialMediaRequest>
{
    public UpdateVolunteerSocialMediaRequestValidator()
    {
        RuleFor(req => req.Details.Media)
            .Must(med =>
            {
                var validAll = med.TrueForAll(item =>
                {
                    Result<SocialMediaName> name = SocialMediaName.Create(item.Name);
                    Result<SocialMediaUrl> url = SocialMediaUrl.Create(item.Url);
                    return name.IsSuccess && url.IsSuccess;
                });
                return validAll;
            })
            .WithMessage("Invalid social media params");
    }
}
