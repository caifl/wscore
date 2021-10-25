using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;

namespace System.Configuration
{
    static class ContextInformationExtension
    {
        public static WebContext GetWebContext(this ContextInformation contextInformation)
        {
            var hostingContext = contextInformation.HostingContext;
            if(hostingContext is ExeContext)
            {
                return new WebContext((ExeContext)hostingContext);
            }

            return null;
        }
    }
}
