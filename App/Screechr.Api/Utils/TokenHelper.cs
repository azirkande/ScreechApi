using System;
using System.Text;

namespace Screechr.Api.Utils
{
    public static class TokenHelper
    {
        public static string DecodeToken(string tokenValue)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(tokenValue);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch { }
            return "";
        }
    }
}
