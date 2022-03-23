using FluentValidation;
using Screechr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screechr.Api.Models
{
    public class ScreechModel
    {
        public string Contents { get; set; }
    }

    public class ScreechModelValidator : AbstractValidator<ScreechModel>
    {
      public ScreechModelValidator()
        {
            RuleFor(x => x.Contents).NotEmpty().NotNull().MaximumLength(FieldLengthLimits.MAX_CONTENTS_LEN);
        }

    }
}
