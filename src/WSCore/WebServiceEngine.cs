using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    public class WebServiceEngine
    {
        private readonly WebServiceHandlerFactory handlerFactory;

        public WebServiceEngine()
        {
            this.handlerFactory = new WebServiceHandlerFactory();
        }

        public virtual async Task ProcessRequest(HttpContext context, Type targetType)
        {
            var request = context.Request;
            var size = (int)request.ContentLength.GetValueOrDefault(1024);

            var memoryStream = new MemoryStream(size);
            await request.Body.CopyToAsync(memoryStream);//.ConfigureAwait(false);

            memoryStream.Seek(0, SeekOrigin.Begin);

            context.Request.Body = memoryStream;

            var httpContext = new HttpContextImpl(context);

            var handler = this.handlerFactory.GetHandler(httpContext, targetType);
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

                while(!iar.IsCompleted)
                {
                    iar.AsyncWaitHandle.WaitOne();
                }
            }
            else
            {
                handler.ProcessRequest(httpContext);
            }

            //if (!(handler is NopHandler))
            //{
            //    var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
            //    Console.WriteLine("Request Message => ");
            //    Console.WriteLine(xml);

            //    handler.ProcessRequest(httpContext);
            //    return true;
            //}

            //return false;
        }
    }
}
