using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Screechr.Api.Utils
{
    public static  class FieldValidator
    {
        public static bool ValidateUrl(string profileUrl)
        {
            if (string.IsNullOrEmpty(profileUrl))
                return true;
            return Regex.IsMatch(profileUrl, Constants.ProfileImageUrlRegexPattern);
        }
    }
}
