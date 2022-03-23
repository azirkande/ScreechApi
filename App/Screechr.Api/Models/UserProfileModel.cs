using FluentValidation;
using Screechr.Api.Utils;

namespace Screechr.Api.Models
{
    public class UserProfileModel
    {
        public string ProfileImageUrl { get; set; }
    }

    public class UserProfileModelValidator : AbstractValidator<UserProfileModel>
    {
        public UserProfileModelValidator()
        {
            RuleFor(model => model.ProfileImageUrl).NotEmpty().NotNull().Matches(Constants.ProfileImageUrlRegexPattern).Must(fv => !string.IsNullOrWhiteSpace(fv));
        }
      
    }
}
