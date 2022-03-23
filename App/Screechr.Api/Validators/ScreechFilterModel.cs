using FluentValidation;
using Screechr.Api.Utils;

namespace Screechr.Api.Validators
{
    public class ScreechFilterModel
    {
        public string SortOrder { get; set; } 
        public ulong? UserId { get; set; }
        public int? PageSize { get; set; } 
        public int? Page { get; set; }
    }

    public class ScreechFilterModelValidator : AbstractValidator<ScreechFilterModel>
    {
        public ScreechFilterModelValidator()
        {
            RuleFor(x => x.PageSize).Must(ValidatePageSize);
            RuleFor(x => x.Page).Must(ValidatePage);
            RuleFor(x => x.SortOrder).Must(ValidateSortDirection);
        }

        private bool ValidatePageSize(int? pageSize)
        {
            if (!pageSize.HasValue)
                return true;
            return pageSize <= Constants.MAX_PAGESIZE;
        }

        private bool ValidatePage(int? page)
        {
            if (!page.HasValue)
                return true;
            return page >= 1;
        }

        private bool ValidateSortDirection(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
                return true;
            return sortOrder.Equals(SortDirection.ASCENDING) || sortOrder.Equals(SortDirection.DESCENDING);
        }
    }


}
