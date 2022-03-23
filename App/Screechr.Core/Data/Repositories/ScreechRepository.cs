using Screechr.Core.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screechr.Core.Data.Repositories
{
    public interface IScreechRepository
    {
        Task<Screech> Get(ulong id);
        Task<ulong> Add(Screech screech);
        Task Update(Screech screech);
        Task<IEnumerable<Screech>> filter(ulong? userId, int page, int pageSize);
    }
    public class ScreechRepository: IScreechRepository
    {
        private readonly IDbContext _dbContext;
        public ScreechRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<ulong> Add(Screech screech)
        {
            var lastInserted= _dbContext.Screeches?.LastOrDefault()?.Id ?? 0;
            screech.Id = lastInserted + 1;
            _dbContext.Screeches.Add(screech);
            return Task.FromResult(screech.Id);
        }

        public Task Update(Screech screech)
        {
            if (_dbContext.Screeches.Any(user => screech.Id == screech.Id))
            {
                _dbContext.Screeches.RemoveAll(s => s.Id == screech.Id);
                _dbContext.Screeches.Add(screech);
            }
          
            return Task.CompletedTask;
        }

        public Task<Screech> Get(ulong id)
        {
            return Task.FromResult(_dbContext.Screeches.FirstOrDefault(s => s.Id == id));
        }

        public Task<IEnumerable<Screech>> filter(ulong? userId, int page, int pageSize)
        {
            if(userId.HasValue)
                return Task.FromResult(_dbContext.Screeches.Where(s =>s.CreatedBy == userId.Value)
                     .Skip((page - 1) * pageSize)
                .Take(pageSize));

            return Task.FromResult(
                _dbContext.Screeches
                .Skip((page -1) * pageSize)
                .Take(pageSize));
        }

    }
}
