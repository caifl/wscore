using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WSCore
{
    class HttpContextImpl : System.Web.HttpContext
    {
        private readonly HttpContext context;

        internal HttpContextImpl(HttpContext context)
        {
            this.context = context;

            this.Request = new HttpRequestImpl(context.Request);
            this.Response = new HttpResonseImpl(context.Response);

            this.RequestServices = this.context.RequestServices;
        }

        public override IDictionary<object, object> Items => this.context.Items;
    }
}
