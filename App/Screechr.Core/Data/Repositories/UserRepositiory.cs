using Screechr.Core.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screechr.Core.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(ulong id);
        Task<User> GetUserByName(string userName);
        Task<ulong> Add(User user);
        Task Update(User user);
        Task<bool> IsDifferentUserWithSameUserNameExists(string userName, ulong id);
    }
    public class UserRepositiory : IUserRepository
    {
        private readonly IDbContext _dbContext;
        public UserRepositiory(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<bool> IsDifferentUserWithSameUserNameExists(string userName, ulong id)
        {
            return Task.FromResult(_dbContext.Users.Any(u => u.Id != id && u.UserName.ToLower().Equals(userName)));
        }
        public Task<ulong> Add(User user)
        {
            var lastInsertedUserId = _dbContext.Users?.LastOrDefault()?.Id ?? 0;
            user.Id = lastInsertedUserId + 1;
            _dbContext.Users.Add(user);
            return Task.FromResult(user.Id);
        }

        public Task Update(User user)
        {
            if (_dbContext.Users.Any(user => user.Id == user.Id))
            {
                _dbContext.Users.RemoveAll(user => user.Id == user.Id);
                _dbContext.Users.Add(user);
            }

            return Task.CompletedTask;
        }

        public Task<User> GetUserByName(string userName)
        {
            return Task.FromResult(_dbContext.Users.FirstOrDefault(user => user.UserName.ToLower().Equals(userName.ToLower())));
        }
        public Task<User> GetUserById(ulong id)
        {
            return Task.FromResult(_dbContext.Users.FirstOrDefault(user => user.Id == id));
        }
    }
}
