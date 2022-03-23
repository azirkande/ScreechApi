using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screechr.Api.Controllers
{
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        public string GetToken(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return input;
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(textBytes);
        }
    }
}
