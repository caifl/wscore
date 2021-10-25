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
            var options = services.GetRequiredService<WSCoreOptions>();

            var wsContainer = services.GetRequiredService<WebServiceContainer>();
            wsContainer.SetOptions(options);

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

            app.UseMiddleware<WebServiceMiddleware>(wsContainer);
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
