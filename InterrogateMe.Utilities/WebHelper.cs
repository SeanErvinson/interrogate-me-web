using Microsoft.AspNetCore.Http;
using System;

namespace InterrogateMe.Utilities
{
    public static class WebHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext
        {
            get { return _httpContextAccessor.HttpContext; }
        }

        public static string GetRemoteIP
        {
            get { return HttpContext.Connection.RemoteIpAddress.ToString(); }
        }

        public static string GetUserAgent
        {
            get { return HttpContext.Request.Headers["User-Agent"].ToString(); }
        }

        public static string GetScheme
        {
            get { return HttpContext.Request.Scheme; }
        }

        public static void SetCookie(string key, string value, int? expirationTime)
        {
            var cookieOptions = new CookieOptions();
            var cookieValue = GetCookie(key);
            if (cookieValue != null)
                cookieValue += $"&{value}";
            else
                cookieValue = value;
            if (expirationTime.HasValue)
                cookieOptions.Expires = DateTime.Now.AddMinutes(expirationTime.Value);
            HttpContext.Response.Cookies.Append(key, cookieValue, cookieOptions);
        }

        public static void RemoveCookie(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        public static string GetCookie(string key)
        {
            return HttpContext.Request.Cookies[key];
        }
    }
}