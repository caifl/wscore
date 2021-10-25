using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Threading;
//using System.Web.SessionState;

namespace System.Web
{
    public class HttpContext
    {
        private readonly static AsyncLocal<HttpContext> current
            = new AsyncLocal<HttpContext>();

        public static HttpContext Current
        {
            get => current.Value;
            set => current.Value = value;
        }

        internal HttpContext()
        {
            this.Items = new Dictionary<object, object>();
        }

        //internal Type WebServiceType
        //{
        //    get;
        //    set;
        //}

        internal IServiceProvider RequestServices
        {
            get;
            set;
        }

        //public virtual HttpApplicationState Application
        //{
        //    get;
        //}

        //public virtual HttpServerUtility Server
        //{
        //    get;
        //}

        //public virtual HttpSessionState Session
        //{
        //    get;
        //}

        public virtual IPrincipal User
        {
            get;
            protected set;
        }

        public virtual HttpRequest Request
        {
            get;
            protected set;
        }

        public virtual HttpResponse Response
        {
            get;
            protected set;
        }

        public virtual IDictionary<object, Object> Items
        {
            get;
        }

        public bool IsCustomErrorEnabled { get; internal set; }
        
        public bool IsDebuggingEnabled { get; internal set; }

        public object GetSection(string name)
        {
            return null;
        }
    }

    public static class HttpWorkerRequest
    {
        public static string GetStatusDescription(int statusCode)
        {
            return statusCode.ToString();
        }
    }
}
