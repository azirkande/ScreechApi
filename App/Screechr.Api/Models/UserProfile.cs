using FluentValidation;
using Screechr.Api.Utils;
using System;
using System.Text.RegularExpressions;

namespace Screechr.Api.Models
{
    public class AddUserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Secret { get; set; }
        public string ProfileImageUrl { get; set; }
    }
    public class AddUserModelValidator: AbstractValidator<AddUserModel>
    {
        public AddUserModelValidator()
        {
            RuleFor(model => model.UserName).NotNull().NotEmpty().MaximumLength(FieldLengthLimits.MAX_USER_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.FirstName).NotNull().NotEmpty().MaximumLength(FieldLengthLimits.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.LastName).NotNull().NotEmpty().MaximumLength(FieldLengthLimits.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.Secret).NotNull().NotEmpty().Length(4, FieldLengthLimits.MAX_SECRET_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.ProfileImageUrl).Must(FieldValidator.ValidateUrl);
        }

    }
}
