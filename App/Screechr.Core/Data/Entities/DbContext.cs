using System.Collections.Generic;

namespace Screechr.Core.Data.Entities
{
    public interface IDbContext
    {
         List<User> Users { get; set; }
        List<Screech> Screeches { get; set; }
    }
    public class DbContext : IDbContext
    {
        public DbContext()
        {
            Users = new List<User>();
            Screeches = new List<Screech>();
        }
        public DbContext(List<User> users, List<Screech> screeches)
        {
            Users = users;
            Screeches = screeches;
        }
        public List<User> Users { get; set; }
        public List<Screech> Screeches { get; set; }
    }
}
