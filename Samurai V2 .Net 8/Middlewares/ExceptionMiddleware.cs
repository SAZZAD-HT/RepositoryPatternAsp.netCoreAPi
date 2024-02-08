using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Samurai_V2_.Net_8.CommonFile;
using System.Net.Http;
namespace Samurai_V2_.Net_8.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _env = env;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
            
                await _next(context);

            }
            catch (Exception ex)
            {
                CustomeMiddleWareService response;
                HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

                string message;
                var exceptiopnType = ex.GetType();

                if (exceptiopnType == typeof(UnauthorizedAccessException))
                {
                    httpStatusCode = HttpStatusCode.Forbidden;
                    message = "You are not authorize.";
                }
                else
                {
                    message = "Something went wrong.";
                }

                if (_env.IsDevelopment())
                {
                    response = new CustomeMiddleWareService((long)httpStatusCode, ex.Message, ex.StackTrace);
                }
                else
                {
                    response = new CustomeMiddleWareService((long)httpStatusCode, message);
                }

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToJson());

            }
        }
        
    }
}
