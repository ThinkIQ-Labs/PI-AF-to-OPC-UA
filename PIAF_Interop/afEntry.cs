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
                return afModel.Items.Where(x => x.GetType() == typeof(AFElementTemplate)).Cast<AFElementTemplate>().ToList();
            }
        }

        public void ReadFile()
        {
            var aSerializer = new XmlSerializer(typeof(AF));
            var aFileStream = new FileStream(fileUrl, FileMode.Open);
            afModel = (AF)aSerializer.Deserialize(aFileStream);

        }

    }
}
