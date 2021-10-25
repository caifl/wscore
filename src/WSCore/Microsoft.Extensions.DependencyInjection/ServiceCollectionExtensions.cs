
using System;
using System.Reflection;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using WSCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        private static WSCoreBuilder builder;

        public static IWSCoreBuilder AddWSCore(this IServiceCollection services, Action<WSCoreOptions> optionsAction)
        {
            if (optionsAction is null)
            {
                throw new ArgumentNullException(nameof(optionsAction));
            }

            if (builder == null)
            {
                builder = new WSCoreBuilder(services);

                services.AddSingleton(services);
                services.AddTransient<DocumentationServerProtocol>();
                services.AddTransient<DiscoveryServerProtocol>();
            }

            var options = new WSCoreOptions();
            optionsAction.Invoke(options);
            services.AddSingleton(options);

            return builder;
        }
    }
}
