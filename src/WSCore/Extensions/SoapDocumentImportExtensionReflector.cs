using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace WSCore.Extensions
{
    public class SoapDocumentImportExtensionReflector : SoapExtensionReflector
    {
        public override void ReflectDescription()
        {
            var serviceType = this.ReflectionContext.ServiceType;
            var attributes = serviceType.GetCustomAttributes(typeof(SoapDocumentImportAttribute), true)
                .OfType<SoapDocumentImportAttribute>();

            if (attributes.Any())
            {
                var sd = this.ReflectionContext.ServiceDescription;
                var messages = sd.Messages;
                foreach (Message message in messages)
                {
                    var name = message.Name;
                    if (name.EndsWith("SoapIn"))
                    {
                        message.Name = name.Substring(0, name.Length - 6);
                    }
                    else if (name.EndsWith("SoapOut"))
                    {
                        message.Name = name.Substring(0, name.Length - 7) + "Response";
                    }
                }

                //var bindings = sd.Bindings;
                //foreach(var binding in bindings)
                //{
                //    binding.o
                //}


                var portTypes = sd.PortTypes;
                foreach(PortType pt in portTypes)
                {
                    var name = pt.Name;
                    if (name.EndsWith("Soap"))
                    {
                        pt.Name = name.Substring(0, name.Length - 4);
                    }

                    var operations = pt.Operations;
                    foreach(Operation operation in operations)
                    {
                        var operationMessages = operation.Messages;

                        var input = operationMessages.Input;
                        name = input.Message.Name;
                        if (name.EndsWith("SoapIn"))
                        {
                            name = name.Substring(0, name.Length - 6);
                            var ns = input.Message.Namespace;

                            input.Message = new XmlQualifiedName(name, ns);
                        }

                        var output = operationMessages.Output;
                        name = output.Message.Name;
                        if (name.EndsWith("SoapOut"))
                        {
                            name = name.Substring(0, name.Length - 7) + "Response";
                            var ns = input.Message.Namespace;

                            output.Message = new XmlQualifiedName(name, ns);
                        }
                    }
                }

                var list = new List<Import>();

                foreach (var attr in attributes)
                {
                    var item = new Import()
                    {
                        Namespace = attr.Namespace,
                        Location = attr.Location
                    };

                    if (!this.IsDuplicated(sd.Imports, attr))
                        list.Add(item);
                }

                list.ForEach(x =>
                {
                    sd.Imports.Add(new Import() { Namespace = x.Namespace, Location = x.Location });
                });

                //foreach(var x in attributes)
                //{
                //    XmlSchemaImport schemaImport = new XmlSchemaImport();
                //    schemaImport.Namespace = x.Namespace;
                //    schemaImport.SchemaLocation = x.Location;

                //    XmlSchema schema = new XmlSchema();
                //    schema.Items.Add(schemaImport);

                //    schema.Write(Console.Out);

                //    sd.Types.Schemas.Add(schema);
                //}
            }
        }

        bool IsDuplicated(ImportCollection items, SoapDocumentImportAttribute newItem)
        {
            foreach (Import item in items)
            {
                if (item.Namespace == newItem.Namespace && item.Location == newItem.Location)
                    return true;
            }

            return false;
        }

        public override void ReflectMethod()
        {
            //var method = this.ReflectionContext.Method;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class SoapDocumentImportAttribute : Attribute
    {
        public virtual string Location
        {
            get;
            set;
        }

        public virtual string Namespace
        {
            get;
            set;
        }
    }
}
