using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    class HttpResonseImpl : System.Web.HttpResponse
    {
        private readonly HttpResponse response;
        private readonly OutputStream outputStream;

        public HttpResonseImpl(HttpResponse response)
        {
            this.response = response;
            this.outputStream = new OutputStream(response);
        }

        //public override bool BufferOutput { get => base.BufferOutput; 
        //    set => base.BufferOutput = value; }
        public override NameValueCollection Headers
        {
            get
            {
                var map = new NameValueCollection();

                var headers = this.response.Headers;
                foreach(var item in headers)
                {
                    map.Add(item.Key, item.Value);
                }

                return map;
            }
        }

        public override Stream OutputStream
        {
            get => this.outputStream;
            protected set => this.response.Body = value;
        }

        public override string ContentType { get => response.ContentType; set => response.ContentType = value; }

        public override int StatusCode { get => response.StatusCode; set => response.StatusCode = value; }

        public override void AddHeader(string name, string value)
        {
            this.response.Headers[name] = value;
        }

        public override void AppendHeader(string name, string value)
        {
            this.response.Headers.Add(name, value);
        }

        public override void ClearHeaders()
        {
            this.response.Headers.Clear(); //.ClearHeaders();
        }

        public override void Redirect(string location)
        {
            this.response.Redirect(location);
        }

        public override void Print()
        {
            this.outputStream.Seek(0, SeekOrigin.Begin);
            var rd = new StreamReader(this.outputStream);
            var all = rd.ReadToEnd();
            Console.WriteLine(all);
        }
        //public override string StatusDescription { get => response.StatusDescription; set => response.StatusDescription = value; }

        ////
        //// Summary:
        ////     Gets the Microsoft.AspNetCore.Http.HttpResponse.HttpContext for this response.
        //public abstract HttpContext HttpContext { get; }
        ////
        //// Summary:
        ////     Gets or sets the HTTP response code.
        //public abstract int StatusCode { get; set; }
        ////
        //// Summary:
        ////     Gets the response headers.
        //public abstract IHeaderDictionary Headers { get; }
        ////
        //// Summary:
        ////     Gets or sets the response body System.IO.Stream.
        //public abstract Stream Body { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the value for the Content-Length response header.
        //public abstract long? ContentLength { get; set; }
        ////
        //// Summary:
        ////     Gets or sets the value for the Content-Type response header.
        //public abstract string ContentType { get; set; }
        ////
        //// Summary:
        ////     Gets an object that can be used to manage cookies for this response.
        //public abstract IResponseCookies Cookies { get; }
        ////
        //// Summary:
        ////     Gets a value indicating whether response headers have been sent to the client.
        //public abstract bool HasStarted { get; }

        ////
        //// Summary:
        ////     Adds a delegate to be invoked after the response has finished being sent to the
        ////     client.
        ////
        //// Parameters:
        ////   callback:
        ////     The delegate to invoke.
        ////
        ////   state:
        ////     A state object to capture and pass back to the delegate.
        //public abstract void OnCompleted(Func<object, Task> callback, object state);
        ////
        //// Summary:
        ////     Adds a delegate to be invoked after the response has finished being sent to the
        ////     client.
        ////
        //// Parameters:
        ////   callback:
        ////     The delegate to invoke.
        //public virtual void OnCompleted(Func<Task> callback);
        ////
        //// Summary:
        ////     Adds a delegate to be invoked just before response headers will be sent to the
        ////     client.
        ////
        //// Parameters:
        ////   callback:
        ////     The delegate to execute.
        ////
        ////   state:
        ////     A state object to capture and pass back to the delegate.
        //public abstract void OnStarting(Func<object, Task> callback, object state);
        ////
        //// Summary:
        ////     Adds a delegate to be invoked just before response headers will be sent to the
        ////     client.
        ////
        //// Parameters:
        ////   callback:
        ////     The delegate to execute.
        //public virtual void OnStarting(Func<Task> callback);
        ////
        //// Summary:
        ////     Returns a temporary redirect response (HTTP 302) to the client.
        ////
        //// Parameters:
        ////   location:
        ////     The URL to redirect the client to. This must be properly encoded for use in http
        ////     headers where only ASCII characters are allowed.
        //public virtual void Redirect(string location);
        ////
        //// Summary:
        ////     Returns a redirect response (HTTP 301 or HTTP 302) to the client.
        ////
        //// Parameters:
        ////   location:
        ////     The URL to redirect the client to. This must be properly encoded for use in http
        ////     headers where only ASCII characters are allowed.
        ////
        ////   permanent:
        ////     True if the redirect is permanent (301), otherwise false (302).
        //public abstract void Redirect(string location, bool permanent);
        ////
        //// Summary:
        ////     Registers an object for disposal by the host once the request has finished processing.
        ////
        //// Parameters:
        ////   disposable:
        ////     The object to be disposed.
        //public virtual void RegisterForDispose(IDisposable disposable);
    }
}
