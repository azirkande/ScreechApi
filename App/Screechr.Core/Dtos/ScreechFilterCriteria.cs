using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screechr.Core.Dtos
{
    public record ScreechFilterCriteria
    {
        public string SortOrder { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public ulong? UserId { get; init; }
    }


}
