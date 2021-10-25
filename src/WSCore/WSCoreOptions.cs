using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.Services.Configuration;
using System.Web.Services.Description;

namespace WSCore
{
    public class WSCoreOptions
    {
        //private readonly Dictionary<Type, string> webServices = new Dictionary<Type, string>();
        private readonly List<Type> serviceTypes = new List<Type>();
        private readonly List<Type> soapExtensionReflectorTypes = new List<Type>();

        public static WSCoreOptions Default
        {
            get;
        }

        public string VirtualPath
        {
            get;
            set;
        }

        public virtual WebServiceProtocols EnabledProtocols
        {
            get;
            set;
        }

        public virtual ICollection<Type> SoapExtensionReflectorTypes
        {
            get => this.soapExtensionReflectorTypes;
        }

        public virtual IReadOnlyCollection<Type> ServiceTypes
        {
            get => new List<Type>(this.serviceTypes);
        }

        //public virtual IReadOnlyDictionary<Type, String> WebServices
        //{
        //    get => new Dictionary<Type, String>(this.webServices);
        //}

        //internal void AddSoapExtensionReflector<TSoapExtensionReflector>()
        //    where TSoapExtensionReflector : SoapExtensionReflector
        //{

        //    if (!soapExtensionReflectorTypes.Contains(typeof(TSoapExtensionReflector)))
        //    {
        //        this.soapExtensionReflectorTypes.Add(typeof(TSoapExtensionReflector));
        //    }
        //}
        public void AddService<TService>()
            where TService : WebService
        {
            this.serviceTypes.Add(typeof(TService));
        }

        //internal void AddWebService<TWebService>(string path = null)
        //    where TWebService : class
        //{
        //    var type = typeof(TWebService);

        //    if (!this.webServices.ContainsKey(type))
        //    {
        //        this.webServices.Add(type, path);
        //    }

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
        //}
    }
}
