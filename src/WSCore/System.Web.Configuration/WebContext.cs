using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace System.Web.Configuration
{
    public class WebContext
    {
        private readonly ExeContext context;

        internal WebContext(ExeContext context)
        {
            this.context = context;

            this.Path = String.Empty; //System.IO.Path.GetDirectoryName(this.context.ExePath);
        }

        public string Path { get; }
    }
}
