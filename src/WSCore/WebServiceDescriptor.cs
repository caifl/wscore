using System;
using System.Collections.Generic;
using System.Text;

namespace WSCore
{
    public class WebServiceDescriptor
    {
        public WebServiceDescriptor(Type type, string name, string description)
        {
            Type = type;
            Name = name;
            Description = description;
        }

        public Type Type { get; }

        public string Name { get; }
        
        public string Description { get; }
    }
}
