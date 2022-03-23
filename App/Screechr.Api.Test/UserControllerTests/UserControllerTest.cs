using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Screechr.Api.Test.Infrastructure;
using Screechr.Core.Data.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace Screechr.Api.Test.UserControllerTests
{
    public partial class UserControllerTest: IClassFixture<ScreechTestApiWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public UserControllerTest (ScreechTestApiWebApplicationFactory factory)
        {
            _httpClient = factory.WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services =>
                {
                    var testData = TestDataGenerator.SeedInitialData();
                    services.AddSingleton<IDbContext>(new DbContext(testData.Item1, testData.Item2));
                });
            }).CreateClient();
   
            _httpClient.BaseAddress = new Uri("http://localhost:5000/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YW1yaXRhejpkZW1vMTIz");
        }

    }
}
