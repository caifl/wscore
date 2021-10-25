using System.Collections.Specialized;
using System.IO;

namespace System.Web
{
    public class HttpRequest
    {
        private readonly IServiceProvider services;

        internal HttpRequest(IServiceProvider services)
        {
            this.QueryString = new NameValueCollection();
            this.Form = new NameValueCollection();
            this.Headers = new NameValueCollection();
            this.InputStream = new MemoryStream();
            this.services = services;
        }

        internal virtual object GetRequiredService(Type type)
        {
            return this.services.GetService(type);
        }

        //public static HttpRequest Create(string url)
        //{
        //    var uri = new Uri(url);
        //    var r = new HttpRequest();
        //    r.Url = uri;
        //    r.HttpMethod = "GET";

        //    //var index = url.LastIndexOf('?');
        //    //if(index >= 0)
        //    //{
        //    //    r.PathInfo = url.Substring(index + 1);
        //    //}

        //    r.PathInfo = string.Empty;
        //    r.Path = uri.PathAndQuery;

        //    var q = uri.Query.TrimStart('?');
        //    if(!string.IsNullOrEmpty(q))
        //    {
        //        var array = q.Split('&');
        //        foreach(var item in array)
        //        {
        //            var i = item.IndexOf('=');
        //            if (i < 0)
        //                continue;

        //            var items = item.Split('=');

        //            string key = null;
        //            string value = null;
        //            if (items.Length > 0)
        //                key = items[0];

        //            if (items.Length > 1)
        //                value = items[1];

        //            r.QueryString[key] = value;
        //        }

        //        if (r.QueryString.Count == 0)
        //            r.QueryString[null] = q;
        //    }

        //    return r;
        //}

        public virtual Uri Url
        {
            get;
            protected set;
        }

        public virtual string Path
        {
            get;
            protected set;
        }

        public virtual string PhysicalPath
        {
            get;
        }

        public virtual string ContentType
        {
            get;
            set;
        }

        public virtual string HttpMethod
        {
            get;
            set;
        }

        public virtual string PathInfo
        {
            get;
            protected set;
        }

        public virtual bool IsLocal
        {
            get;
        }

        public virtual NameValueCollection Headers
        {
            get;
        }

        public virtual NameValueCollection QueryString
        {
            get;
        }

        public virtual NameValueCollection Form
        {
            get;
        }

        public string UserHostName { get; }

        public string UrlReferrer { get; internal set; }
        
        public string UserHostAddress { get; internal set; }
        
        public int ContentLength { get; set; }

        public Stream InputStream { get; protected set; }
    }
}
