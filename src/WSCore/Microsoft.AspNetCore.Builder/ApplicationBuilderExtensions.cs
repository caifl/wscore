using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WSCore;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseWSCore(this IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var services = app.ApplicationServices;
            var serviceItems = services.GetRequiredService<IServiceCollection>();

            var options = services.GetRequiredService<WSCoreOptions>();

            var section = WebServicesSection.Current;

            var types = options.SoapExtensionReflectorTypes;
            foreach(var type in types)
                section.SoapExtensionReflectorTypes.Add(new TypeElement(type));

            section.Protocols.Clear();
            var values = Enum.GetValues(typeof(WebServiceProtocols));
            foreach (WebServiceProtocols value in values)
            {
                if ((options.EnabledProtocols & value) == value)
                {
                    section.Protocols.Add(new ProtocolElement(value));
                }
            }

            var enumerator = serviceItems.GetEnumerator();

            var basePath = options.VirtualPath;
            if (!String.IsNullOrEmpty(options.VirtualPath))
            {
                if (!basePath.EndsWith("/"))
                    basePath = string.Concat(basePath, "/");
            }
            else
            {
                basePath = "/";
            }

            var list = new List<Type>();
            while (enumerator.MoveNext())
            {
                var type = enumerator.Current.ServiceType;

                var ws = type.GetCustomAttribute<WebServiceAttribute>();
                if (ws == null)
                    continue;

                var name = ws?.Name;
                if (string.IsNullOrEmpty(name))
                {
                    name = type.Name;
                }

                app.UseMiddleware<WebServiceMiddleware>(type, $"{basePath}{name}.asmx");
                list.Add(type);
            }

            app.UseMiddleware<QueryServiceMiddleware>(list.AsReadOnly());

            //app.Map(new Http.PathString(basePath + "services"), () =>
            //{
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            //app.MapWhen(basePath + "services", )
        }
    }
}
