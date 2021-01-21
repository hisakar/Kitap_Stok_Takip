using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace KitapeviStokTakipWebApi.MiddleWares
{
 
    public class BasicAuthMiddleware
    {
    //    private readonly RequestDelegate _next;       NOT USED BASIC AUTH // JUST TEST

    //    public BasicAuthMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    public async Task Invoke(HttpContext httpContext)
    //    {
    //        string authHeader = httpContext.Request.Headers["Authorization"];
    //        if (authHeader != null && authHeader.StartsWith("Basic"))
    //        {
    //            string encodeUsernameAndPassword = authHeader.Substring(6).Trim();
    //            Encoding encoding = Encoding.GetEncoding("UTF-8");
    //            string usernameAndPassword = encoding.GetString(Convert.FromBase64String(encodeUsernameAndPassword));
    //            int index = usernameAndPassword.IndexOf(":");
    //            var username = usernameAndPassword.Substring(0, index);
    //            var password = usernameAndPassword.Substring(index +1 );
    //            //if (username.Equals("ishakgeneli") && password.Equals("123456"))
    //            //{
    //            //   await _next.Invoke(httpContext);
    //            //}
    //            //else
    //            //{
    //            //    httpContext.Response.StatusCode = 401;
    //            //    return;
    //            //}
    //        }
    //        else
    //        {
    //            httpContext.Response.StatusCode = 401;
    //            return;
    //        }
    //    }
    //}

    //public static class BasicAuthMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<BasicAuthMiddleware>();
    //    }
    }
}
