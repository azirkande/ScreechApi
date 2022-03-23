using Screechr.Api.Models;
using Screechr.Api.Test.Infrastructure;
using Screechr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Screechr.Api.Test.ScreechControllerTests
{
    public partial class ScreechControllerTest : IClassFixture<ScreechTestApiWebApplicationFactory>
    {
        private string GetMaxLenContents()
        {
            return "voluptatem dolorum nam nulla dolorem ut porro dolor. Et sint ipsa cum voluptatem et iure praesentium sit tenetur neque ut dolorem debitis qui totam error.Lorem ipsum dolor sit amet. Ut omnis porro quo labore alias in libero odit ad officia autem est modi nihil sed nemo autem. Quo consectetur necessitatibus et deserunt dolor sit optio fuga. Consequatur rerum sit voluptatem dolorum nam nulla dolorem ut porro dolor. Et sint ipsa cum voluptatem et iure praesentium sit tenetur neque ut dolorem debitis qui totam error.Lorem ipsum dolor sit amet. Ut omnis porro quo labore alias in libero odit ad officia autem est modi nihil sed nemo autem. Quo consectetur necessitatibus et deserunt dolor sit optio fuga. Consequatur rerum sit voluptatem dolorum nam nulla dolorem ut porro dolor. Et sint ipsa cum voluptatem et iure praesentium sit tenetur neque ut dolorem debitis qui totam error.Lorem ipsum dolor sit amet. Ut omnis porro quo labore alias in libero odit ad officia autem est modi nihil sed nemo autem. Quo consectetur necessitatibus et deserunt dolor sit optio fuga. Consequatur rerum sit voluptatem dolorum nam nulla dolorem ut porro dolor. Et sint ipsa cum voluptatem et iure praesentium sit tenetur neque ut dolorem debitis qui totam error.";
        }

        [Fact]
        public async Task when_user_is_not_authenticated()
        {

            var url = "api/screech/edit-screech/456/777";

            var screech = new ScreechUpdateModel
            {
                Contents = GetMaxLenContents()
            };

            _httpClient.DefaultRequestHeaders.Authorization = null;
            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(screech));
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task when_contents_exceeds_max_length_limit_while_adding_screech()
        {
            var url = "api/screech/add/456";
            var screech = new ScreechModel
            {
                Contents = GetMaxLenContents()
            };
            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(screech));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_contents_exceeds_max_length_limit_while_updating_screech()
        {
            var url = "api/screech/edit-screech/456/777";

            var screech = new ScreechUpdateModel
            {
                Contents = GetMaxLenContents()
            };

            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(screech));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_contents_are_empty_while_updating()
        {
            var url = "api/screech/edit-screech/456/777";

            var screech = new ScreechUpdateModel();

            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(screech));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_contents_are_empty_while_adding()
        {
            var url = "api/screech/add/456";
            var screech = new ScreechModel();
            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(screech));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_page_size_exceeds_max_limit()
        {
            var url = "api/screech/filter?pageSize=900";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_wrong_page_number_given_while_filtering()
        {
            var url = "api/screech/filter?pageNumber=-1";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_wrong_screech_id_is_given_while_fetching()
        {
            var url = "api/screech/4444";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
