using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityServer
{
    public static class Routes
    {
        public static IApplicationBuilder UseRoute(this IApplicationBuilder builder)
        {

            builder.Map("/user", app =>
            {
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Hello User!");
                });
            });


            builder.Use( (context,next)=> {
                Console.WriteLine("start");
                return next();

            } );


            builder.Use(handler=> {
                Console.WriteLine("start");
                return handler;

            });


            return builder;
        }
    }
}
