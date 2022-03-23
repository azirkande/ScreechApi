using Screechr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screechr.Core.Dtos
{

    public record ScreechDto
    {
        public ulong Id { get; init; }
        public string Contents { get; init; }
        public ulong CreatedBy { get; init; }
        public DateTime DateCreated { get; init; }
        public DateTime DateModified { get; init; }

    }

    public record ScreechAddDto
    {
        public string Contents { get; init; }
        public ulong CreatedBy { get; init; }
    }


}
