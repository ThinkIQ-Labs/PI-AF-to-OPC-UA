using OPCUA_Interop.Managers;
using System.Diagnostics;
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

        // name, id, type


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

        public string GenerateCSV(string fileUrl)
        {
            using (TextWriter writer = new StreamWriter(fileUrl))
            {
                var typeCounter = 0;
                var propertyCounter = 0;
                foreach(var aType in uaTypeDesings)
                {
                    typeCounter++;
                    writer.WriteLine($"{aType.SymbolicName.Name},{10000 + typeCounter},ObjectType");
                    foreach(var aProperty in aType.Children.Items)
                    {
                        propertyCounter++;
                        writer.WriteLine($"{aProperty.SymbolicName.Name},{20000 + propertyCounter},Variable");
                    }
                }
            }
            return File.ReadAllText(fileUrl);

        }

        public void CompileNodeset(string compilerExecutableUrl, string xmlFileUrl, string csvFileUrl, string outputDirUrl)
        {
            var args = new string[]
                {
                    "compile",
                    "-d2",
                    xmlFileUrl,
                    "-cg",
                    csvFileUrl,
                    "-o2",
                    outputDirUrl
                };
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = $"{compilerExecutableUrl}";
            startInfo.Arguments = String.Join(" ", args);

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}