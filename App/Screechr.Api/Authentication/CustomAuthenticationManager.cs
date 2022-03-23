using Screechr.Api.Utils;
using Screechr.Core.Enums;
using Screechr.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screechr.Api.Authentication
{
    public record AuthResult
    {
        public bool IsAuthenticated { get; init; }
        public string Name { get; init; }
        public ulong Id { get; init; }
    }
    public interface ICustomAuthenticationManager
    {
        Task<AuthResult> ValidateToken(string tokent);
    }
    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        private readonly IUserService _userService;

        public CustomAuthenticationManager(IUserService userService)
        {
            _userService = userService;
        }

        public  async Task<AuthResult> ValidateToken(string token)
        {
            if(string.IsNullOrEmpty(token))
                return new AuthResult { IsAuthenticated = false };

            var secret = TokenHelper.DecodeToken(token);
            var tokenParts = secret.Split(':');
            if (tokenParts?.Length == 2)
            {
                return await AuthenticateUser(userName: tokenParts[0], secret: tokenParts[1]);
            }

            return new AuthResult { IsAuthenticated = false };
        }

       
        private async Task<AuthResult> AuthenticateUser(string userName, string secret)
        {
            var result = await _userService.GetUserByUserName(userName);
            if (result.Status != OperationResult.SUCCESS)
                return new AuthResult { IsAuthenticated = false };

            if (result.User.Secret.ToLower().Equals(secret))
                return new AuthResult { IsAuthenticated = true, Id = result.User.Id, Name = string.Join(result.User.FirstName, "  ", result.User.LastName) };

            return new AuthResult { IsAuthenticated = false };
        }
    }
}
