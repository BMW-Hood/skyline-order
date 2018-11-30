using AspectCore.DynamicProxy;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interceptors
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TracingInterceptorAttribute : AbstractInterceptorAttribute
    {
        public ITracer Tracer { get; set; }
        public string Name { get; set; }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {

            using (IScope childScope = Tracer.BuildSpan(Name).StartActive(finishSpanOnDispose: true))
            {
                childScope.Span.Log(DateTimeOffset.Now, "Test Start");

                await next(context);

                childScope.Span.Log(DateTimeOffset.Now, "Test Finish");
            }





        }
    }
}
