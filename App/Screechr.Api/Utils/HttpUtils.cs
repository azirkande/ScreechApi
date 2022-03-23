using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Screechr.Api.Utils
{
    public static class HttpUtils
    {
        public static HttpContent GetContent(object payload)
        {
            var data = JsonConvert.SerializeObject(payload);
            return new StringContent(data, Encoding.UTF8,"application/json");
        }
    }
}
