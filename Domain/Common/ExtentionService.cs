namespace Domain.Common
{
    public static class ExtentionService
    {
        //public static Task<bool> IsClaimsIdentity()
        //{
        //    // Debug claims
        //    var identity = User.Identity as ClaimsIdentity;
        //    //var claims = identity?.Claims.Select(c => new { c.Type, c.Value }).ToList();
        //    //_logger.LogInformation("User Claims: {@Claims}", claims);
        //    return;
        //}

        public static IEnumerable<(string Type, string Value)> GetClaimsData(this System.Security.Claims.ClaimsPrincipal user)
        {
            return user.Claims.Select(c => (c.Type, c.Value));
        }

        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str;
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string ToFriendlyDate(this DateTime dateTime)
        {
            return dateTime.ToString("MMMM dd, yyyy");
        }
        public static string ToFriendlyTime(this DateTime dateTime)
        {
            return dateTime.ToString("hh:mm tt");
        }
        public static string ToFriendlyDateTime(this DateTime dateTime)
        {
            return $"{dateTime.ToFriendlyDate()} at {dateTime.ToFriendlyTime()}";
        }
    }
}
