
using System;
using System.Collections.Generic;
using System.Web.Services.Description;

namespace Microsoft.Extensions.DependencyInjection
{
    public class WSCoreBuilder : IWSCoreBuilder
    {
        public WSCoreBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public virtual IServiceCollection Services
        {
            get;
        }
    }
}
