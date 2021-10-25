using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Web.Hosting
{
    public sealed class HostingEnvironment : MarshalByRefObject
    {
        private static string applicationPhysicalPath;
        private static string applicationVirtualPath = "/";
        private static bool isHosted = true;

        static HostingEnvironment()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileName = assembly.GetName().CodeBase;
            applicationPhysicalPath = System.IO.Path.GetDirectoryName(fileName);
        }

        public static bool IsHosted
        {
            get => isHosted;
        }

        public static string ApplicationPhysicalPath
        {
            get => applicationPhysicalPath;
        }

        public static string ApplicationVirtualPath
        {
            get => applicationVirtualPath;
        }

        public static string MapPath(string path)
        {
            var value = Path.Combine(ApplicationPhysicalPath, path);
            return value;
        }
    }
}
