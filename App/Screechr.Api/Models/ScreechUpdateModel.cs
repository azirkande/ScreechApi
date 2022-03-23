using FluentValidation;
using Screechr.Api.Utils;

namespace Screechr.Api.Models
{
    public class ScreechUpdateModel
    {
        public string Contents { get; set; }
    }

    public class UpdateScreechModelValidator : AbstractValidator<ScreechUpdateModel>
    {
        public UpdateScreechModelValidator()
        {
            RuleFor(x => x.Contents).NotEmpty().NotNull().MaximumLength(FieldLengthLimits.MAX_CONTENTS_LEN);
        }

    }
}
