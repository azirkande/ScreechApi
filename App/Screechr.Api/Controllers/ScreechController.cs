using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Screechr.Api.Models;
using Screechr.Api.Utils;
using Screechr.Api.Validators;
using Screechr.Core.Dtos;
using Screechr.Core.Services;
using System.Threading.Tasks;

namespace Screechr.Api.Controllers
{
    [Route("api/screech")]
    [ApiController]
    [Authorize]
    public class ScreechController : ControllerBase
    {
        private readonly IScreechFilterCriteriaValidator _validator;
        private readonly IScreechService _screechService;

        public ScreechController(IScreechFilterCriteriaValidator validator, IScreechService screechService)
        {
            _validator = validator;
            _screechService = screechService;
        }

        [HttpPut("edit-screech/{userId}/{screechId}")]
        public async Task<IActionResult> ModifyScreech([FromBody] ScreechUpdateModel model, ulong screechId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _screechService.UpdateContents(screechId, model.Contents);
            return Accepted();
        }

        [HttpGet("{screechId}")]
        public async Task<IActionResult> GetScreech(ulong schreechId)
        {
            if (schreechId <= 0)
                return BadRequest();
            var result = await _screechService.Get(schreechId);
            if (result.Status == Core.Enums.OperationResult.NOT_FOUND)
                return NoContent();
            return Ok(result.Screech);
        }

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> Add([FromBody] ScreechModel model, ulong userId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var id = await _screechService.Add(new ScreechAddDto { Contents = model.Contents, CreatedBy = userId });
            return Created("api/screech/{schreechId}", new { Id = id });
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> List(ulong? userId, int pageSize = Constants.DEFAULT_PAGESIZE, int pageNumber = Constants.DEFAULT_PAGE, string sortOrder = SortDirection.DESCENDING)
        {
            bool isValid = _validator.Validate(new ScreechFilterModel { Page = pageNumber, PageSize = pageSize, SortOrder = sortOrder });
            if (!isValid)
                return BadRequest();

            var screeches = await _screechService.FilterByUser(new Core.Dtos.ScreechFilterCriteria { Page = pageNumber, PageSize = pageSize, SortOrder = sortOrder, UserId = userId });

            return Ok(screeches);
        }
    }
}
