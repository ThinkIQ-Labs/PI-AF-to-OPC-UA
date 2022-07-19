using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PIAF_Interop
{
    public class afEntry
    {
        public AF afModel { get; set; }
        public string fileUrl { get; set; }

        public List<AFElementTemplate> afElementTemplates {
            get
            {
                if(afModel.ExportedType== "AFDatabase")
                {
                    return (afModel.Items.First() as AFDatabase).Items.Where(x => x.GetType() == typeof(AFElementTemplate)).Cast<AFElementTemplate>().ToList();
                } else
                {
                    return afModel.Items.Where(x => x.GetType() == typeof(AFElementTemplate)).Cast<AFElementTemplate>().ToList();
                }
            }
        }

        public string? GetAFElementTemplateBaseTypeString(AFElementTemplate aAfTemplate)
        {
            if (aAfTemplate.BaseTemplate == null) return null;

            if (aAfTemplate.BaseTemplate.GetType() != typeof(XmlNode[])) return null;

            return (aAfTemplate.BaseTemplate as XmlNode[])[0].InnerText;
        }
        public void ReadFile()
        {
            var aSerializer = new XmlSerializer(typeof(AF));
            var aFileStream = new FileStream(fileUrl, FileMode.Open);
            afModel = (AF)aSerializer.Deserialize(aFileStream);
        }

        public void ReadString(string xml)
        {
            var aSerializer = new XmlSerializer(typeof(AF));
            using (var reader = new StringReader(xml))
            {
                afModel = (AF)aSerializer.Deserialize(reader);
            }
        }

    }
}
