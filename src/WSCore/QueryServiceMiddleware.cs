using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    class QueryServiceMiddleware
    {
        private readonly IEnumerable<Type> serviceTypes;
        private readonly RequestDelegate next;

        public QueryServiceMiddleware(IEnumerable<Type> serviceTypes, RequestDelegate next)
        {
            this.serviceTypes = serviceTypes;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if(context.Request.Method == "GET" && path == "/ws/services")
            {
                //JsonSerializer
                var sb = new StringBuilder();
                sb.Append("[");

                foreach (var type in this.serviceTypes)
                {
                    sb.Append("\"");
                    sb.Append(type.Name);
                    sb.Append("\"");
                }

                sb.Append("]");
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(sb.ToString(), Encoding.UTF8);

                return;
            }

            await this.next(context);
        }
    }
}
