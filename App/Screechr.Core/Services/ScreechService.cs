using Screechr.Core.Data.Entities;
using Screechr.Core.Data.Repositories;
using Screechr.Core.Dtos;
using Screechr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screechr.Core.Services
{
    public interface IScreechService
    {
        Task<ulong> Add(ScreechAddDto screech);
        Task UpdateContents(ulong id,  string contents);
        Task<IEnumerable<ScreechDto>> FilterByUser(ScreechFilterCriteria criteria);
        Task<ScreechResult> Get(ulong id);
    }
    public class ScreechService : IScreechService
    {
        private readonly IScreechRepository _screechRepository;
        public ScreechService(IScreechRepository dbContext)
        {
            this._screechRepository = dbContext;
        }

        public async Task<ScreechResult> Get(ulong id)
        {
            var screech =  await _screechRepository.Get(id);
            if (screech == null)
                return new ScreechResult { Status = OperationResult.NOT_FOUND };
            var screechDto = new ScreechDto { Id = screech.Id, Contents = screech.Contents, CreatedBy = screech.CreatedBy, DateCreated = screech.DateCreated, DateModified = screech.DateModified };
            return new ScreechResult { Status = OperationResult.SUCCESS, Screech = screechDto };
        }
        public async Task<ulong> Add(ScreechAddDto screech)
        {
            var newScreech = new Screech
            {
                Contents = screech.Contents,
                CreatedBy = screech.CreatedBy,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            return await _screechRepository.Add(newScreech);
        }
        public async Task UpdateContents(ulong id, string contents)
        {
            var existingScreech = await _screechRepository.Get(id);
            if (existingScreech != null)
            {
                existingScreech.Contents = contents;
                existingScreech.DateModified = DateTime.UtcNow;
                await _screechRepository.Update(existingScreech);
            }
        }
        public async Task<IEnumerable<ScreechDto>> FilterByUser(ScreechFilterCriteria criteria)
        {
            var screeches = (await _screechRepository.filter(criteria.UserId, criteria.Page, criteria.PageSize))
                                         ?.Select(s => new ScreechDto
                                         {
                                             Contents = s.Contents,
                                             DateCreated = s.DateCreated,
                                             CreatedBy = s.CreatedBy,
                                             DateModified = s.DateModified,
                                             Id = s.Id
                                         });
            if (screeches == null)
            {
                return Enumerable.Empty<ScreechDto>();
            }
            if (criteria.SortOrder == "asc")
            {
                return screeches.OrderBy(s => s.DateCreated);
            }

            return screeches.OrderByDescending(s => s.DateCreated);
        }

    }
}
