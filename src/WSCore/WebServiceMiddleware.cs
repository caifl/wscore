using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    class WebServiceMiddleware
    {
        private readonly Type serviceType;
        private readonly string path;
        private readonly WebServiceHandlerFactory handlerFactory;
        private readonly RequestDelegate next;

        public WebServiceMiddleware(Type serviceType, string path, 
            RequestDelegate next)
        {
            this.serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            this.path = path ?? throw new ArgumentNullException(nameof(path));
            this.handlerFactory = new WebServiceHandlerFactory();
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(this.path,
                StringComparison.OrdinalIgnoreCase))
            {
                var request = context.Request;
                var size = (int)request.ContentLength.GetValueOrDefault(1024);

                using (var memoryStream = new MemoryStream(size))
                {
                    await request.Body.CopyToAsync(memoryStream);//.ConfigureAwait(false);

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    context.Request.Body = memoryStream;

                    var httpContext = new HttpContextImpl(context);

                    var handler = this.handlerFactory.GetHandler(httpContext, this.serviceType);
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
