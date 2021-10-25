using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Web
{
    public class HttpResponse
    {
        private Stream outputStream;

        public HttpResponse()
        {
            this.Cache = new HttpCachePolicy();
            this.Headers = new NameValueCollection();
            this.outputStream = new MemoryStream();
        }

        public virtual bool BufferOutput
        {
            get;
            set;
        }

        public virtual string ContentType
        {
            get;
            set;
        }

        public virtual Stream OutputStream
        {
            get => this.outputStream;
            protected set => this.outputStream = value;
        }

        public virtual HttpCachePolicy Cache
        {
            get;
        }

        public virtual int StatusCode
        {
            get;
            set;
        }

        public virtual NameValueCollection Headers
        {
            get;
        }

        public virtual bool TrySkipIisCustomErrors { get; set; }

        public virtual string StatusDescription { get; set; }

        internal virtual void Clear()
        {
        }

        public virtual void ClearHeaders()
        {
            this.Headers.Clear();
        }

        public virtual void AppendHeader(string name, string value)
        {
            this.Headers[name] = value;
        }

        public virtual void AddHeader(string name, string value)
        {
            this.Headers.Add(name, value);
        }

        public virtual void Redirect(string location)
        {

        }

        public virtual void Print()
        {
            //var list = this.outputStream.ToArray();
            //var text = Encoding.UTF8.GetString(list);
            //Console.WriteLine(text);
        }
    }

    public class HttpCachePolicy
    {
        public HttpCacheVaryByHeaders VaryByHeaders
        {
            get;
        }

        public HttpCacheVaryByParams VaryByParams
        {
            get;
        }

        public void SetCacheability(HttpCacheability cacheability)
        {

        }

        internal void SetExpires(DateTime dateTime)
        {
        }

        internal void SetSlidingExpiration(bool v)
        {
        }

        internal void SetNoServerCaching()
        {
        }

        internal void SetMaxAge(TimeSpan zero)
        {
        }
    }

    public class HttpCacheVaryByHeaders
    {
        public bool this[string name]
        {
            get => true;
            set
            {

            }
        }
    }

    public class HttpCacheVaryByParams
    {
        public bool this[string name]
        {
            get => true;
            set
            {

            }
        }
    }

    public enum HttpCacheability
    {
        NoCache = 1,

        Server = 3
    }
}
