using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WSCore
{
    class HttpRequestImpl : System.Web.HttpRequest
    {
        private readonly HttpRequest request;

        internal HttpRequestImpl(HttpRequest request)
            : base(null)
        {
            this.request = request;
            this.InputStream = new InputStream(request);
            this.ContentType = request.ContentType;

            if(request.ContentLength.HasValue)
                this.ContentLength = (int)request.ContentLength;
            this.HttpMethod = request.Method;

            var headers = request.Headers;
            var em = headers.GetEnumerator();
            while(em.MoveNext())
                this.Headers.Add(em.Current.Key, em.Current.Value);

            var query = request.Query;
            var q = query.GetEnumerator();
            while(q.MoveNext())
                this.QueryString.Add(q.Current.Key, q.Current.Value);

            var qs = request.QueryString.ToString();
            if(!string.IsNullOrEmpty(qs)
                && qs.IndexOf('=') < 0)
            {
                this.QueryString.Clear();

                var value = qs.TrimStart('?');
                this.QueryString.Add(null,value);
                this.QueryString.Add(value, null);
            }

            this.Path = request.Path.ToString();
            this.Url = new Uri(request.Scheme + "://" + request.Host + this.Path);
            this.PathInfo = String.Empty;
        }

        //public override NameValueCollection QueryString => base.QueryString;

        ////
        //// Summary:
        ////     Gets or sets the raw query string used to create the query collection in Request.Query.
        ////
        //// Returns:
        ////     The raw query string.
        //public abstract QueryString QueryString { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the RequestBody Stream.
        ////
        //// Returns:
        ////     The RequestBody Stream.
        //public abstract Stream Body { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Content-Type header.
        ////
        //// Returns:
        ////     The Content-Type header.
        //public abstract string ContentType { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Content-Length header.
        ////
        //// Returns:
        ////     The value of the Content-Length header, if any.
        //public abstract long? ContentLength { get; set; }
        ////
        //// Summary:
        ////     Gets the collection of Cookies for this request.
        ////
        //// Returns:
        ////     The collection of Cookies for this request.
        //public abstract IRequestCookieCollection Cookies { get; set; }
        ////
        //// Summary:
        ////     Gets the request headers.
        ////
        //// Returns:
        ////     The request headers.
        //public abstract IHeaderDictionary Headers { get; }
        ////
        //// Summary:
        ////     Gets or sets the RequestProtocol.
        ////
        //// Returns:
        ////     The RequestProtocol.
        //public abstract string Protocol { get; set; }
        ////
        //// Summary:
        ////     Gets the query value collection parsed from Request.QueryString.
        ////
        //// Returns:
        ////     The query value collection parsed from Request.QueryString.
        //public abstract IQueryCollection Query { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the request body as a form.
        //public abstract IFormCollection Form { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the request path from RequestPath.
        ////
        //// Returns:
        ////     The request path from RequestPath.
        //public abstract PathString Path { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the RequestPathBase.
        ////
        //// Returns:
        ////     The RequestPathBase.
        //public abstract PathString PathBase { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the Host header. May include the port.
        //public abstract HostString Host { get; set; }
        ////
        //// Summary:
        ////     Returns true if the RequestScheme is https.
        ////
        //// Returns:
        ////     true if this request is using https; otherwise, false.
        //public abstract bool IsHttps { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the HTTP request scheme.
        ////
        //// Returns:
        ////     The HTTP request scheme.
        //public abstract string Scheme { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the HTTP method.
        ////
        //// Returns:
        ////     The HTTP method.
        //public abstract string Method { get; set; }
        ////
        //// Summary:
        ////     Gets the Microsoft.AspNetCore.Http.HttpRequest.HttpContext for this request.
        //public abstract HttpContext HttpContext { get; }
        ////
        //// Summary:
        ////     Checks the Content-Type header for form types.
        ////
        //// Returns:
        ////     true if the Content-Type header represents a form content type; otherwise, false.
        //public abstract bool HasFormContentType { get; }

        ////
        //// Summary:
        ////     Reads the request body if it is a form.
        //public abstract Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default);
    }
}
