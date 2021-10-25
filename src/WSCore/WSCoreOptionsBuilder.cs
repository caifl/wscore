using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Configuration;
using System.Web.Services.Description;

namespace WSCore
{
    public class WSCoreOptionsBuilder
    {
        //private readonly List<Type> webServices = new List<Type>();
        //private readonly List<Type> soapExtensionReflectorTypes = new List<Type>();

        private readonly WSCoreOptions options = new WSCoreOptions();

        public virtual WSCoreOptions Options
        {
            get => this.options;
        }

        public virtual WSCoreOptionsBuilder SetEnabledProtocols(WebServiceProtocols protocols)
        {
            return this;
        }

        //public IReadOnlyCollection<Type> SoapExtensionReflectorTypes
        //{
        //    get => this.soapExtensionReflectorTypes.AsReadOnly();
        //}

        public virtual WSCoreOptionsBuilder AddSoapExtensionReflector<TSoapExtensionReflector>() 
            where TSoapExtensionReflector : SoapExtensionReflector
        {
            //this.options.AddSoapExtensionReflector<TSoapExtensionReflector>();

            return this;
        }

        public virtual WSCoreOptionsBuilder SetVirtualPath(string virtualPath)
        {
            options.VirtualPath = virtualPath;

            return this;
        }

        //public WSCoreOptionsBuilder AddWebService<TWebService>(string path = null)
        //    where TWebService : class
        //{
        //    this.options.AddWebService<TWebService>();

        //    //var type = typeof(TWebService);

        //    //if (!this.webServices.Contains(type))
        //    //{
        //    //    this.webServices.Add(type);
        //    //    //this.Services.AddTransient(type);
        //    //}
        //    //if (string.IsNullOrEmpty(path))
        //    //{
        //    //    var ws = type.GetCustomAttribute<WebServiceAttribute>();
        //    //    var name = ws?.Name;
        //    //    if (string.IsNullOrEmpty(name))
        //    //    {
        //    //        name = type.Name;
        //    //    }

        //    //    path = $"/{name}.asmx";
        //    //}

        //    //
        //    //this.Services.AddTransient<TWebService>();

        //    return this;
        //}
    }
}
