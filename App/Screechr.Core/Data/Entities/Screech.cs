using System;

namespace Screechr.Core.Data.Entities
{
    public record Screech
    {
        public ulong Id { get;  set; }
        public string Contents { get;  set; }
        public ulong CreatedBy { get;  set; }
        public DateTime DateCreated { get;  set; }
        public DateTime DateModified { get;  set; }

    }
}
