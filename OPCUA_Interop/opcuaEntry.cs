using OPCUA_Interop.Managers;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OPCUA_Interop
{
    public class uaEntry
    {
        public ModelDesign model { get; set; }
        public string fileUrl { get; set; }

        public List<ObjectTypeDesign> uaTypeDesings
        {
            get
            {
                return model.Items.Where(x => x.GetType() == typeof(ObjectTypeDesign)).Cast<ObjectTypeDesign>().ToList();
            }
        }

        public void ReadFile()
        {
            var aSerializer = new XmlSerializer(typeof(ModelDesign));
            var aFileStream = new FileStream(fileUrl, FileMode.Open);
            model = (ModelDesign)aSerializer.Deserialize(aFileStream);

        }

        XmlSerializer XmlSerializer { get; set; }
        public XmlSerializerNamespaces XmlSerializerNamespaces { get; set; }
        public uaNameSpaceManager uaNameSpaceManager { get; set; }
        public uaObjectTypeDesignManager uaObjectTypeDesignManager { get; set; }

        public void CreateNewModel(string domain, string name)
        {
            XmlSerializer = new XmlSerializer(typeof(ModelDesign));
            XmlSerializerNamespaces = new XmlSerializerNamespaces();
            XmlSerializerNamespaces.Add("uax", "http://opcfoundation.org/UA/2008/02/Types.xsd");
            XmlSerializerNamespaces.Add("ua", "http://opcfoundation.org/UA/");
            XmlSerializerNamespaces.Add(name, $"{domain}/{name}/");

            model = new ModelDesign();
            model.TargetNamespace = $"{domain}/{name}/";
            model.TargetXmlNamespace = $"{domain}/{name}/";
            
            uaNameSpaceManager = new uaNameSpaceManager(model);
            uaNameSpaceManager.AddVanillaUaNameSpace();
            uaNameSpaceManager.AddBasicNameSpace(domain, name);

            uaObjectTypeDesignManager = new uaObjectTypeDesignManager(model);
        }

        public string GenerateXML(string fileUrl)
        {

            using (TextWriter writer = new StreamWriter(fileUrl))
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true }))
            {

                XmlSerializer.Serialize(xmlWriter, model, XmlSerializerNamespaces);

            }

            return XDocument.Parse(File.ReadAllText(fileUrl)).ToString();

        }
    }
}