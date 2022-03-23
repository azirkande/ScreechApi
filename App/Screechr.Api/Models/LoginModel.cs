using FluentValidation;
using Screechr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screechr.Api.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Secret { get; set; }
    }

    public class LoginModelModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelModelValidator()
        {
            RuleFor(model => model.UserName).NotNull().NotEmpty().MaximumLength(FieldLengthLimits.MAX_USER_NAME_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
            RuleFor(model => model.Secret).NotNull().NotEmpty().Length(8, FieldLengthLimits.MAX_SECRET_LEN).Must(fv => !string.IsNullOrWhiteSpace(fv));
        }

    }
}
