using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Screechr.Api.Authentication;
using Screechr.Api.Models;
using Screechr.Data.Services;
using Screechr.Dtos.Entities;
using System.Threading.Tasks;

namespace Screechr.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
   [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthenticationManager _authenticationManger;
        public UserController(IUserService userService, ICustomAuthenticationManager authenticationManger)
        {
            _userService = userService;
            _authenticationManger = authenticationManger;
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfile(ulong userId)
        {
            if (userId > 0)
            {
                var result = await _userService.GetUser(userId);
                if (result.Status == Core.Enums.OperationResult.SUCCESS)
                    return Ok(result.User);

                if (result.Status == Core.Enums.OperationResult.USER_NOT_FOUND)
                    return NoContent();
            }

            return BadRequest("Invalid userId");
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model, ulong userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.Update(new UserDto
            {
                Id = userId,
                LastName = model.LastName,
                FirstName = model.FirstName,
                ProfileImageUrl = model.ProfileImageUrl,
                UserName = model.UserName
            });
            if (result.Status == Core.Enums.OperationResult.USER_ALREADY_EXISTS)
                return BadRequest();
            return Accepted();
        }

        [HttpPut("update-profile-picture/{userId}")]
        public async Task<IActionResult> UpdateProfileUrl(ulong userId, [FromBody] UserProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateProfilePicture(userId, model.ProfileImageUrl);
            return Accepted();
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser([FromBody] AddUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.Add(new UserDto
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                ProfileImageUrl = model.ProfileImageUrl,
                Secret = model.Secret,
                UserName = model.UserName
            });

            if (result.Status == Core.Enums.OperationResult.USER_ALREADY_EXISTS)
                return BadRequest("User name is taken");
            return Created("api/profile/{userId}", new { id = result.UserId });

        }
    }

}
