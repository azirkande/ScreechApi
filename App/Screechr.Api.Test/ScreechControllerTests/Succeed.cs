using Microsoft.AspNetCore.Mvc;
using Screechr.Api.Models;
using Screechr.Api.Test.Infrastructure;
using Screechr.Api.Utils;
using Screechr.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace Screechr.Api.Test.ScreechControllerTests
{
    public partial class ScreechControllerTest : IClassFixture<ScreechTestApiWebApplicationFactory>
    {
        [Fact]
        public async Task when_valid_details_are_given_while_adding_screech()
        {
            var url = "api/screech/add/456";
            var screech = new ScreechModel
            {
                Contents = "Sampleeeeeeeeeee"
            };
            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(screech));
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task when_valid_contents_are_given_while_updating_screech()
        {
            var url = "api/screech/edit-screech/456/777";

            var screech = new ScreechUpdateModel
            {
                Contents = "Sampleeeeeeeeeee"
            };

            var result = await _httpClient.PutAsync(url, HttpUtils.GetContent(screech));
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
        }

        [Fact]
        public async Task when_default_filter_criteria_is_given()
        {
            var url = "api/screech/filter";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var data = JsonConvert.DeserializeObject<IEnumerable<ScreechDto>>(await result.Content.ReadAsStringAsync());
            Assert.Equal(3, data.Count());
            var screchesReturned = data.ToArray();
            Assert.Equal("777", screchesReturned[2].Id.ToString());
            Assert.Equal("888", screchesReturned[1].Id.ToString());
            Assert.Equal("999", screchesReturned[0].Id.ToString());
        }

        [Fact]
        public async Task when_asc_sort_id_filter_is_given()
        {
            var url = "api/screech/filter?sortOrder=asc";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var data = JsonConvert.DeserializeObject<IEnumerable<ScreechDto>>(await result.Content.ReadAsStringAsync());
            Assert.Equal(3, data.Count());
            var screchesReturned = data.ToArray();
            Assert.Equal("777", screchesReturned[0].Id.ToString());
            Assert.Equal("888", screchesReturned[1].Id.ToString());
            Assert.Equal("999", screchesReturned[2].Id.ToString());
        }

        [Fact]
        public async Task when_no_user_id_filter_is_given()
        {
            var url = "api/screech/filter?pageSize=10&page=1";
            var result = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var data = JsonConvert.DeserializeObject<IEnumerable<ScreechDto>>(await result.Content.ReadAsStringAsync());
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public async Task when_filter_by_user_id()
        {
            var url = "api/screech/filter?userId=456";
            var response = await _httpClient.GetAsync(url);
            var data = JsonConvert.DeserializeObject<IEnumerable<ScreechDto>> (await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, data.Count());
        }

    }
}
