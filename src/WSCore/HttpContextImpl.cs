using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WSCore
{
    class HttpContextImpl : System.Web.HttpContext
    {
        private readonly HttpContext context;
        private readonly WebServiceContainer container;

        internal HttpContextImpl(HttpContext context, WebServiceContainer container)
        {
            this.context = context;
            this.container = container;
            this.Request = new HttpRequestImpl(context.Request);
            this.Response = new HttpResonseImpl(context.Response);
            this.User = new UserImpl(context.User);

            this.RequestServices = new ServiceProviderImpl(this.context.RequestServices, container);
        }

        class UserImpl : System.Security.Principal.IPrincipal
        {
            private readonly ClaimsPrincipal user;

            public UserImpl(ClaimsPrincipal user)
            {
                this.user = user;
            }

            public IIdentity Identity => user.Identity;

            public bool IsInRole(string role)
            {
                return this.user.IsInRole(role);
            }
        }

        class ServiceProviderImpl : IServiceProvider
        {
            private readonly IServiceProvider requestServices;
            private readonly WebServiceContainer container;

            public ServiceProviderImpl(IServiceProvider requestServices,
                WebServiceContainer container)
            {
                this.requestServices = requestServices;
                this.container = container;
            }

            public virtual object GetService(Type serviceType)
            {
                var service = this.requestServices.GetService(serviceType);
                if (service == null)
                    return Activator.CreateInstance(serviceType);

                return service;
            }
        }

        public override IDictionary<object, object> Items => this.context.Items;
    }
}
