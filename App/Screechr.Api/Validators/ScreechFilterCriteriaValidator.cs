using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screechr.Api.Validators
{
    public interface IScreechFilterCriteriaValidator
    {
        bool Validate(ScreechFilterModel model);
    }
    public class ScreechFilterCriteriaValidator : IScreechFilterCriteriaValidator
    {
        public bool Validate(ScreechFilterModel model)
        {
            ScreechFilterModelValidator validator = new ScreechFilterModelValidator();
            var result = validator.Validate(model);
            return result.IsValid;
        }
    }
}
