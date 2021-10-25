using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Services;

namespace Microsoft.AspNetCore.Builder
{
    public interface IWebServicesBuilder
    {
        IReadOnlyDictionary<string, Type> WebServices
        {
            get;
        }

        IWebServicesBuilder Publish<TWebService>(string path = null);
    }

    class WebServicesBuilderImpl : IWebServicesBuilder
    {
        private readonly Dictionary<String, Type> services = new Dictionary<String, Type>();

        public virtual IReadOnlyDictionary<string, Type> WebServices
        {
            get => this.services;
        }

        public virtual IWebServicesBuilder Publish<TWebService>(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                var type = typeof(TWebService);
                var ws = type.GetCustomAttribute<WebServiceAttribute>();
                var name = ws?.Name;
                if (string.IsNullOrEmpty(name))
                {
                    name = type.Name;
                }

                path = $"/{name}.asmx";
            }

            this.services.Add(path, typeof(TWebService));

            return this;
        }
    }
}
