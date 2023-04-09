using System;
namespace Ajmera.Assessment.API.Logging
{
    internal class EnableRequestBufferingMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request?.EnableBuffering();

            return next.Invoke(context);
        }
    }
}

