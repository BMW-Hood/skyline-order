using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public static class TracingExtensions
    {
        public static IApplicationBuilder UseJaegerTracing(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TracingMiddleWare>();
        }
    }

    public class TracingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ITracer _tracer;

        public TracingMiddleWare(RequestDelegate next, ILoggerFactory factory, ITracer tracer)
        {
            _next = next;
            _logger = factory.CreateLogger<TracingMiddleWare>();
            _tracer = tracer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"User IP:{context.Connection.RemoteIpAddress.ToString()}");
            
            var operation = $"{context.Request.Method}::{context.Request.Path}";
            using (IScope parentScope = _tracer.BuildSpan(operation).StartActive(finishSpanOnDispose: true))
            {
                parentScope.Span.Log(DateTimeOffset.Now, "loop_start");
                await _next.Invoke(context);
                parentScope.Span.SetTag("HttpStatus",context.Response.StatusCode.ToString());
                parentScope.Span.Log(DateTimeOffset.Now, "loop_finished");
                parentScope.Span.Finish(DateTimeOffset.Now); 
            }
        }
    }
}