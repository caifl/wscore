using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Web
{
    public interface IHttpHandler
    {
        bool IsReusable
        {
            get;
        }

        void ProcessRequest(HttpContext context);
    }

    public interface IHttpAsyncHandler
    {
        IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object asyncState);

        void EndProcessRequest(IAsyncResult asyncResult);
    }

    public interface IHttpTaskHandler
    {
        Task ProcessRequestAsync(HttpContext context);
    }

    public interface IRequiresSessionState
    {

    }
}
