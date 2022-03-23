using Screechr.Api.Models;
using Screechr.Api.Utils;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Screechr.Api.Test.UserControllerTests
{
    public partial class UserControllerTest
    {
        [Fact]
        public async Task when_valid_userid_is_provided()
        {
            var url = "api/user/profile/123";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task when_valid_user_details_are_added()
        {
            var url = "api/user/add";

            var newUser = new AddUserModel
            {
                FirstName = "Demo",
                LastName = "User",
                UserName = "amritaz",
                ProfileImageUrl = "http://s3bucket.com/picture",
                Secret = "secret"
            };
         
            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_valid_user_details_are_updated()
        {
            var url = "api/user/update/123";

            var updatedUser = new UpdateUserModel
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "amritaz",
                ProfileImageUrl = "http://s3bucket.com/picture",
            };
           
            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(updatedUser));
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task when_valid_profile_format_is_provided()
        {
            var url = "api/user/update-profile-picture/456";

            var newUser = new UserProfileModel
            {
                ProfileImageUrl = "http://s3bucket.com/pictured",
            };

            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        }

    }
}

