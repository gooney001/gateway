using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Middle
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                await _next.Invoke(context);
                stopwatch.Stop();
                Console.WriteLine($"请求地址：{context.Request.Path}；请求相应时间：{stopwatch.ElapsedMilliseconds}ms");
                if (context.Response.StatusCode == (int)StatusCodes.Status404NotFound)
                {
                    var resultError = JsonConvert.SerializeObject(new { Ok = false, Data = "action not found" });
                    await context.Response.WriteAsync(resultError);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
