using Screechr.Core.Data.Entities;
using Screechr.Core.Data.Repositories;
using Screechr.Core.Enums;
using Screechr.Dtos.Entities;
using System;
using System.Threading.Tasks;

namespace Screechr.Data.Services
{
    public interface IUserService
    {
        Task<UserResult> GetUser(ulong id);

        Task<UpdateProfileResult> Add(UserDto user);

        Task<UpdateProfileResult> Update(UserDto user);

        Task<UpdateProfileResult> UpdateProfilePicture(ulong id, string profilePictureUrl);

        Task<UserLoginResult> GetUserByUserName(string userName);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginResult> GetUserByUserName(string userName)
        {
            var user = await _userRepository.GetUserByName(userName);
            if (user == null)
                return new UserLoginResult { Status = OperationResult.USER_NOT_FOUND };
            return new UserLoginResult
            {
                Status = OperationResult.SUCCESS,
                User = new UserLoginDetails
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Secret = user.Secret
                }
            };
        }
        public async Task<UpdateProfileResult> Add(UserDto user)
        {
            var userToSave = MapDtoToEntity(user);

            var isUserExists =  (await _userRepository.GetUserByName(userToSave.UserName)) != null;
            if (isUserExists)
            {
                return new UpdateProfileResult { Status = Core.Enums.OperationResult.USER_ALREADY_EXISTS };
            }
            userToSave.ModifiedOn = DateTime.UtcNow;
            userToSave.CreatedOn = DateTime.UtcNow;
            userToSave.Secret = user.Secret;
            var id = await _userRepository.Add(userToSave);

            return new UpdateProfileResult { Status = Core.Enums.OperationResult.SUCCESS, UserId = id };
        }

        public async Task<UpdateProfileResult> Update(UserDto user)
        {

            var isExists = await _userRepository.IsDifferentUserWithSameUserNameExists(user.UserName, user.Id);

            if (isExists)
            {
                return new UpdateProfileResult { Status = Core.Enums.OperationResult.USER_ALREADY_EXISTS };
            }
            var userToSave = MapDtoToEntity(user);


            userToSave.ModifiedOn = DateTime.UtcNow;
            await _userRepository.Update(userToSave);

            return new UpdateProfileResult { Status = Core.Enums.OperationResult.SUCCESS };
        }

        public async Task<UserResult> GetUser(ulong id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return new UserResult { Status = Core.Enums.OperationResult.USER_NOT_FOUND };
            return new UserResult
            {
                Status = Core.Enums.OperationResult.SUCCESS,
                User = MapEntityToDto(user)
            };
        }

        public async Task<UpdateProfileResult> UpdateProfilePicture(ulong id, string profilePictureUrl)
        {
            var user = await _userRepository.GetUserById(id);
            if (user != null)
            {
                user.ProfileImageUrl = profilePictureUrl;
                await _userRepository.Update(user);
                return new UpdateProfileResult { Status = Core.Enums.OperationResult.SUCCESS };
            }
            return new UpdateProfileResult { Status = Core.Enums.OperationResult.USER_NOT_FOUND };
        }

        private UserDto MapEntityToDto(User entity)
        {
            return new UserDto
            {
                CreatedOn = entity.CreatedOn,
                FirstName = entity.FirstName,
                Id = entity.Id,
                LastName = entity.LastName,
                ModifiedOn = entity.ModifiedOn,
                ProfileImageUrl = entity.ProfileImageUrl,
                UserName = entity.UserName
            };
        }

        private User MapDtoToEntity(UserDto dto)
        {
            return new User
            {
                FirstName = dto.FirstName,
                Id = dto.Id,
                LastName = dto.LastName,
                ProfileImageUrl = dto.ProfileImageUrl,
                UserName = dto.UserName
            };
        }
    }
}
