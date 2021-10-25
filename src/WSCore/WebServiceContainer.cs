using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Linq;

namespace WSCore
{
    public class WebServiceContainer
    {
        private readonly ConcurrentDictionary<string, WebServiceDescriptor> serviceTypes = new ConcurrentDictionary<string, WebServiceDescriptor>();
        private WSCoreOptions options;
        private const string AsmxExtension = ".asmx";

        public virtual Type ResolveType(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            //Check if request path starts with virtual path.
            PathString remaining;
            if (!(request.Path.StartsWithSegments(options.VirtualPath, StringComparison.OrdinalIgnoreCase, out remaining) && remaining.HasValue))
                return null;

            var path = remaining.Value;
            int index = path.LastIndexOf('/');
            if (index != 0 || !path.EndsWith(AsmxExtension, StringComparison.OrdinalIgnoreCase))
                return null;

            var name = path.Substring(1, path.Length - AsmxExtension.Length - 1);
            if(name.Length == 0)
                return null;

            name = name.ToLowerInvariant();

            WebServiceDescriptor ws;
            if (this.serviceTypes.TryGetValue(name, out ws))
                return ws.Type;

            return null;
        }

        internal void SetOptions(WSCoreOptions options)
        {
            this.options = options;

            var serviceTypes = options.ServiceTypes;
            foreach (var type in serviceTypes)
                this.Add(type);
        }

        public virtual IReadOnlyList<WebServiceDescriptor> Items
        {
            get => new List<WebServiceDescriptor>(this.serviceTypes.Values);
        }

        //public virtual void AddAssembly(Assembly assembly)
        //{

        //}

        public virtual void Add<TService>()
            where TService : WebService
        {
            this.Add(typeof(TService));
        }

        public virtual void Add(Type type)
        {
            if (type.IsAbstract || type.IsInterface) // || !(typeof(WebService).IsAssignableFrom(type)))
                throw new NotSupportedException("Abstract class or interface is not supported.");

            var ws = type.GetCustomAttribute<WebServiceAttribute>();
            var serviceName = ws.Name ?? type.Name;

            serviceTypes.TryAdd(serviceName.ToLowerInvariant(), new WebServiceDescriptor(type, serviceName, ws?.Description));
        }
    }
}
