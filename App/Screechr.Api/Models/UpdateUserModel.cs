using FluentValidation;
using Screechr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Screechr.Api.Models
{
    public class UpdateUserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }

    }

    public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(model => model.UserName).NotNull().NotEmpty().MaximumLength(FieldLengthLimits.MAX_USER_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.FirstName).NotNull().NotEmpty().MaximumLength(FieldLengthLimits.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.LastName).NotNull().NotEmpty().MaximumLength(FieldLengthLimits.MAX_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.ProfileImageUrl).Must(FieldValidator.ValidateUrl);
        }

      
    }
}
