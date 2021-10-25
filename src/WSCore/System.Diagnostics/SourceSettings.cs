using System.Collections.Generic;
using System.Xml;

namespace System.Diagnostics
{
    class SourceSettings
    {
        public SourceSettings()
        {
            this.Sources = new SourceElement[0];
        }

        public SourceElement[] Sources
        {
            get;
            set;
        }

        public void Load(XmlElement element)
        {
            var list = new List<SourceElement>();

            var children = element.ChildNodes;
            foreach (XmlElement source in children)
            {
                if (!source.LocalName.Equals("source"))
                    continue;

                var sourceElement = new SourceElement();
                sourceElement.Name = source.GetAttribute("name");
                sourceElement.SwitchName = source.GetAttribute("switchName");
                sourceElement.SwitchType = source.GetAttribute("switchType");
                sourceElement.SwitchValue = source.GetAttribute("switchValue");

                list.Add(sourceElement);
            }

            this.Sources = list.ToArray();
        }
    }
}
