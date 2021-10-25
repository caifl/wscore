using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;

namespace System.Diagnostics
{
    class SystemDiagnosticsSection : IConfigurationSectionHandler
    {
        public SystemDiagnosticsSection()
        {
            this.Sources = new SourceSettings();
            this.SharedListeners = new ListenerSettings();
            this.Switches = new SwitchSettings();
        }

        public void Apply(TraceSource traceSource)
        {
            var traceListeners = new List<ListenerElement>();
            if (this.SharedListeners.SharedListeners != null && this.SharedListeners.SharedListeners.Length > 0)
                traceListeners.AddRange(this.SharedListeners.SharedListeners);

            if(this.Sources.Sources != null && this.Sources.Sources.Length > 0)
            {
                var sourceMap = this.Sources.Sources.ToDictionary(x => x.Name);
                var name = traceSource.Name;
                SourceElement sourceElement = null;
                
                if(sourceMap.TryGetValue(name, out sourceElement))
                {
                    var switchType = sourceElement.SwitchType;
                    if(!string.IsNullOrEmpty(switchType))
                    {
                        var type = Type.GetType(switchType);
                        if(type != null)
                        {
                            var switchInstance = (SourceSwitch)Activator.CreateInstance(type);
                            traceSource.Switch = switchInstance;
                        }
                    }
                }
            }

            if(this.Switches.Switches != null)
            {
                var map = this.Switches.Switches.ToDictionary(x => x.Name);

                
            }
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            var children = section.ChildNodes;
            foreach (XmlElement element in children)
            {
                switch (element.LocalName)
                {
                    case "sources":
                        this.Sources.Load(element);
                        break;

                    case "sharedListeners":
                        this.SharedListeners.Load(element);
                        break;

                    case "switches":
                        this.Switches.Load(element);
                        break;

                    case "trace":
                        this.Trace = new TraceSettings();
                        this.Trace.Load(element);
                        break;
                }
            }

            return this;
        }

        public SourceSettings Sources
        {
            get;
        }

        public SwitchSettings Switches
        {
            get;
        }

        public ListenerSettings SharedListeners
        {
            get;
        }

        public TraceSettings Trace
        {
            get;
            protected set;
        }
    }
}
