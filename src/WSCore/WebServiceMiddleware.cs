using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    class WebServiceMiddleware
    {
        private readonly WebServiceHandlerFactory handlerFactory;
        private readonly WebServiceContainer container;
        private readonly RequestDelegate next;

        public WebServiceMiddleware(WebServiceContainer container, RequestDelegate next)
        {
            this.handlerFactory = new WebServiceHandlerFactory();
            this.container = container;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var serviceType = this.container.ResolveType(context.Request);
            if (serviceType != null)
            {
                var request = context.Request;
                var size = (int)request.ContentLength.GetValueOrDefault(1024);

                using (var memoryStream = new MemoryStream(size))
                {
                    await request.Body.CopyToAsync(memoryStream);

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    context.Request.Body = memoryStream;

                    var httpContext = new HttpContextImpl(context, this.container);

                    var handler = this.handlerFactory.GetHandler(httpContext, serviceType);
                    if (handler is System.Web.IHttpTaskHandler)
                    {
                        await ((System.Web.IHttpTaskHandler)handler).ProcessRequestAsync(httpContext);
                    }
                    else if (handler is System.Web.IHttpAsyncHandler)
                    {
                        var asyncHandler = ((System.Web.IHttpAsyncHandler)handler);
                        var iar = asyncHandler.BeginProcessRequest(httpContext, new AsyncCallback((asyncResult) =>
                        {
                            asyncHandler.EndProcessRequest(asyncResult);
                        }), handler);

                        while (!iar.IsCompleted)
                        {
                            iar.AsyncWaitHandle.WaitOne();
                        }
                    }
                    else
                    {
                        handler.ProcessRequest(httpContext);
                    }
                }

                return;
            }

            await this.next(context);
        }
    }
}
