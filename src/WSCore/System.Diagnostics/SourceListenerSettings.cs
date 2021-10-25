using System.Collections.Generic;
using System.Xml;

namespace System.Diagnostics
{
    class SourceListenerSettings
    {
        public SourceListenerSettings()
        {
        }

        public ListenerElement[] Listeners
        {
            get;
            set;
        }

        public void Load(XmlElement element)
        {
            var items = new List<NameTypeElement>();

            var children = element.ChildNodes;
            foreach (var child in children)
            {
                if (child is XmlElement)
                {
                    var lElement = child as XmlElement;
                    switch (lElement.LocalName)
                    {
                        case "add":
                            items.Add(new NameTypeElement()
                            {
                                IsRemove = false,
                                Name = lElement.GetAttribute("name"),
                            });
                            break;

                        case "remove":
                            items.Add(new NameTypeElement()
                            {
                                IsRemove = true,
                                Name = lElement.GetAttribute("name"),
                            });
                            break;
                    }
                }
            }
        }
    }
}
