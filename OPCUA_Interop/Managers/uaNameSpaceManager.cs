using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCUA_Interop.Managers
{
    public class uaNameSpaceManager
    {
        ModelDesign model { get; set; }
        public uaNameSpaceManager(ModelDesign model)
        {
            this.model = model;
            this.model.Namespaces = new Namespace[] { };
        }

        // <Namespace Name="animal" Prefix="animal"
        //      XmlNamespace="https://opcua.rocks/UA/animal/Types.xsd" XmlPrefix="animal">https://opcua.rocks/UA/animal/</Namespace>
        public void AddBasicNameSpace(string domain, string name)
        {
            List<Namespace> namespaces = model.Namespaces.ToList();
            namespaces.Add(new Namespace
            {
                Name = name,
                Prefix = name,
                XmlNamespace = $"{domain}/{name}/Types.xsd",
                XmlPrefix = name,
                Value = $"{domain}/{name}/"
            });
            model.Namespaces = namespaces.ToArray();
        }

        // <Namespace Name="OpcUa" Version="1.03" PublicationDate="2013-12-02T00:00:00Z"
        //      Prefix="Opc.Ua" InternalPrefix="Opc.Ua.Server"
        //      XmlNamespace="http://opcfoundation.org/UA/2008/02/Types.xsd" XmlPrefix="OpcUa">http://opcfoundation.org/UA/</Namespace>
        public void AddVanillaUaNameSpace()
        {
            List<Namespace> namespaces = model.Namespaces.ToList();
            namespaces.Add(new Namespace
            {
                Name = "OpcUa",
                Version = "1.03",
                PublicationDate = "2013-12-02T00:00:00Z",
                Prefix = "Opc.Ua",
                InternalPrefix = "Opc.Ua.Server",
                XmlNamespace = "http://opcfoundation.org/UA/2008/02/Types.xsd",
                XmlPrefix = "OpcUa",
                Value = "http://opcfoundation.org/UA/"
            });
            model.Namespaces = namespaces.ToArray();
        }
    }
}
