using Screechr.Api.Models;
using Screechr.Api.Utils;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Screechr.Api.Test.UserControllerTests
{
    public partial class UserControllerTest
    {
        [Fact]
        public async Task when_unavailable_userid_is_provided()
        {
            var url = "api/user/profile/1";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public async Task when_user_is_not_authenticated()
        {
            var url = "api/user/profile/1";
            _httpClient.DefaultRequestHeaders.Authorization = null;
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task when_invalid_userid_is_provided()
        {
            var url = "api/user/profile/-122";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_username_firstname_is_missing()
        {
            var url = "api/user/add";
            var newUser = new AddUserModel
            {
                LastName = "User",
                Secret = "secret"
            };
            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_duplicate_username_is_provided()
        {
            var url = "api/user/add";

            var newUser = new AddUserModel
            {
                FirstName = "Demo",
                LastName = "User",
                UserName = "prashantd",
                ProfileImageUrl = "http://s3bucket.com/picture",
                Secret = "secret"
            };

            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_invalid_profile_format_is_provided()
        {
            var url = "api/user/update-profile-picture/456";

            var newUser = new UserProfileModel
            {
                ProfileImageUrl = "gibberrish",
            };

            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_field_values_character_limit_exceeds()
        {
            var url = "api/user/add";

            var newUser = new AddUserModel
            {
                FirstName = "Demo",
                LastName = "User",
                UserName = "Ee5b9oe5q7vtBC40VnahoJPYDIpSfUSn0ccV2jVah3rDcp1n4ADJwp1DskTJ3EXCdz9CGIgwQ2WKopbQRwSoPyUUgTgtVOloq1pIOVUGG9Anl6PkdsUgevKMagjx2yOWCMwPNbYndzfcmr8KjIJIJOCrRjv2LvfKbFwxRgMN8NEUDsEyG4K3exNQwATWAxWuyDpNq8W6KYSG2K7CSgNPGrEIxlobXqSwVxgVBhnKx1k9i98RLUKYb7c016PfY0JPYQfLW9Ay5yBSWvyXZUetehmUTRKQQ5evBRVZqZ10hH57ccN81kG7OBsnSniXqsVoJy1xnosWgbAw4rvI39qTq1msbq6FXUyqtwmRAXZIiH5k5qJ8WgxLiVGXXR2HOaHhhXf0PlLJpo4XzkwCLU4Qxf39ESDEi85eHyEWrpzcUFyDMWNndgfJyIlEu2hYUV02d0dUw5J9z9q6kozctbAPCXg5FRZHIEgeAkVWnXoZAkXLj2tMaILBhZOal4AVxfKZGPlo2P5waEmdmgrRZKmRe3dkuM2vGOnB4fvDTk7sK36W4aZdMVrjgjcCbV6HGKEwigEMfLVdg7dxgeeu1U8eSmcCAYbarpkoeTJlTaG4ZajM96va13W9LIAqRZpKMdiUZjhkBeUthEh1R1YucQUt2y5weMsz6qQOMcJW4hjEotpVVnEAhwibHNEOspPwDpfeiHrTFvr7zo71fmprM6EYeBrcoLPO2r5iYJ6WtgkSkVpccYmWbIvHYYMKSg9tk3CokTExMZjzomhsoNxHJ9tAWEaEXGxQxrKPb9bCCY8g4HARKCTA8iBhbd5ia1IMUXhUAQDvKMag0jpwydTzejhUOegxg3JEso9S8RJMFfrEqH4wCNptaTAxqe8KiYYeNzjhk2PVblwjXCannrZJpTOvf7UAAwfDeapz1vfiLDliu6OqGgx5teAnfFpfHeQ9wkSMEe11jRPFClOBfhB9zZb4JM8VAe15WoozYdsl9DfX",
                ProfileImageUrl = "http://s3bucket.com/picture",
                Secret = "secret"
            };

            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_duplicate_username_is_given_while_updating()
        {
            var url = "api/user/update/456";

            var updatedUser = new UpdateUserModel
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "amritaz",
                ProfileImageUrl = "http://s3bucket.com/picture",
            };

            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(updatedUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
