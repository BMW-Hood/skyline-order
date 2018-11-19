using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public static class TracingExtensions
    {


    }

    public class TracingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _factory;
        private readonly ITracer _tracer;
        public TracingMiddleWare(RequestDelegate next, ILoggerFactory factory, ITracer tracer)
        {
            _next = next;
            _factory = factory;
            _tracer = tracer;

        }
        public async Task Invoke(HttpContext context)
        {

            var builder = _tracer.BuildSpan($"{context.Request.Method}::{context.Request.QueryString}");
            builder.WithTag("machine.name", "machine1").WithTag("cpu.cores", 8);
            var startTime = DateTimeOffset.Now;
            var span = builder.WithStartTimestamp(startTime).Start();


            var logData = new List<KeyValuePair<string, object>>();
            logData.Add(KeyValuePair.Create<string, object>("handling number of events", 6));
            span.Log(DateTimeOffset.Now, logData);


            var @vent = "loop_finished";
            span.Log(DateTimeOffset.Now, @vent);
            span.Finish(DateTimeOffset.Now);

            await _next.Invoke(context);
        }

    }


}
