using System.Collections.Generic;
using System.Xml;

namespace System.Diagnostics
{
    class SwitchSettings
    {
        public NameTypeElement[] Switches
        {
            get;
            set;
        }

        public void Load(XmlElement element)
        {
            var list = new List<NameTypeElement>();

            var children = element.ChildNodes;
            foreach (var child in children)
            {
                if (child is XmlElement)
                {
                    var source = child as XmlElement;
                    var isRemove = source.LocalName.Equals("remove");
                    if (!source.LocalName.Equals("add") && !isRemove)
                        continue;

                    var ntElement = new NameTypeElement();
                    ntElement.Name = source.GetAttribute("name");
                    ntElement.Type = source.GetAttribute("type");
                    ntElement.IsRemove = isRemove;

                    list.Add(ntElement);
                }
            }

            this.Switches = list.ToArray();
        }
    }
}
