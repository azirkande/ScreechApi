using Moq;
using Screechr.Api.Authentication;
using Screechr.Data.Services;
using System.Threading.Tasks;
using Xunit;

namespace Screechr.Api.Test
{
    public class CustomAuthenticationManagerTests
    {
        private readonly Mock<IUserService> _userService;
        public CustomAuthenticationManagerTests()
        {
            _userService = new Mock<IUserService>();
            _userService.Setup(m => m.GetUserByUserName(It.IsAny<string>())).ReturnsAsync((string userName) =>
            {
                if (userName.Equals("fakeuser"))
                    return new Dtos.Entities.UserLoginResult { Status = Core.Enums.OperationResult.USER_NOT_FOUND };

                return new Dtos.Entities.UserLoginResult
                {
                    Status = Core.Enums.OperationResult.SUCCESS,
                    User = new Dtos.Entities.UserLoginDetails
                    {
                        FirstName = "Demo",
                        LastName = "User",
                        Secret = "demo111",
                        Id = 444
                    }
                };
                });
        }
        [Theory]
        [InlineData(null,false)]
        [InlineData("", false)]
        [InlineData("asdfasfsafafsaf", false)]
        [InlineData("YW1yaXRhejpkZW1vMQ==", false)]
        [InlineData("ZmFrZXVzZXI6ZGVtbzE=",false)]
        [InlineData("YW1yaXRhejpkZW1vMTEx", true)]
        public async Task should_validate_token(string token, bool expectedOutCome)
        {
            var authManager = new CustomAuthenticationManager(_userService.Object);
            var result = await authManager.ValidateToken(token);
            Assert.Equal(expectedOutCome, result.IsAuthenticated);
        }
    }
}
