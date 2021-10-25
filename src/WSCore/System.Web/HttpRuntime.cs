using System.Web.Caching;

namespace System.Web
{
    public class HttpRuntime
    {
        private static readonly Cache cache = new Cache();

        public static Cache Cache
        {
            get => cache;
        }

        public static string MachineConfigurationDirectory
        {
            get;
        }
    }
}
