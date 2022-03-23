using System;

namespace Screechr.Core.Data.Entities
{
    public record User
    {
        public ulong Id { get;  set; }
        public string UserName { get;  set; }
        public string FirstName { get;  set;  }

        public string LastName { get;  set; }
        public string Secret { get;  set; }

        public string ProfileImageUrl { get;  set; }
        public DateTime CreatedOn { get;  set; }
        public DateTime ModifiedOn { get;  set; }
    }
}
