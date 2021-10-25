using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System.Security.Policy
{
    internal static class AssemblyExtension
    {
        public static Evidence Evidence(this Assembly assembly)
        {
            return null;//new Evidence()
            //throw new NotImplementedException();
        }
    }
}
