using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screechr.Api.Utils
{
    public static class AuthConstants
    {
        public static string SchemeName = "BasicAuth";
    }
    public class Constants
    {
        public const string ProfileImageUrlRegexPattern = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
        public const int DEFAULT_PAGE = 1;
        public const int DEFAULT_PAGESIZE = 100;
        public const int MAX_PAGESIZE = 500;
    }

    public class FieldLengthLimits
    {
        public const int MAX_CONTENTS_LEN = 1024;
        public const int MAX_NAME_LEN= 100;
        public const int MAX_USER_NAME_LEN = 80;
        public const int MAX_SECRET_LEN = 32;
    }
    public static class SortDirection
    {
        public const string DESCENDING = "desc";
        public const string ASCENDING= "asc";

    }
}
