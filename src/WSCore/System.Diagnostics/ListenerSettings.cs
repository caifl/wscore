using System.Collections.Generic;
using System.Xml;

namespace System.Diagnostics
{
    class ListenerSettings
    {
        public ListenerElement[] SharedListeners
        {
            get;
            set;
        }

        public void Load(XmlElement element)
        {
            var list = new List<ListenerElement>();

            var children = element.ChildNodes;
            foreach (XmlElement source in children)
            {
                if (!source.LocalName.Equals("source"))
                    continue;

                var sourceElement = new ListenerElement();
                sourceElement.Name = source.GetAttribute("name");
                sourceElement.Type = source.GetAttribute("type");
                //sourceElement.SwitchType = source.GetAttribute("switchType");
                //sourceElement.SwitchValue = source.GetAttribute("switchValue");

                list.Add(sourceElement);
            }

            this.SharedListeners = list.ToArray();
        }
    }
}
