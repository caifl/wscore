using System;
using System.Collections.Generic;
using System.Text;

namespace System.Configuration
{
    internal static class PrivilegedConfigurationManager
    {
        public static object GetSection(string sectionName)
        {
            if (sectionName is null)
                throw new ArgumentNullException(nameof(sectionName));

            return ConfigurationManager.GetSection("system.web/webServices");
        }
    }
}
