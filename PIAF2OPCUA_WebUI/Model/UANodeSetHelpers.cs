using System.Xml.Serialization;
using System.Xml;
using System.Reflection.PortableExecutable;

namespace Opc.Ua.Export
{
    public static class UANodeSetFromString
    {
        /// <summary>
        /// Loads a nodeset from a string.
        /// </summary>
        /// <param name="istr">The input string.</param>
        /// <returns>The set of nodes</returns>
        public static UANodeSet Read(String istr)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UANodeSet));
            using (TextReader reader = new StringReader(istr))
            { 
                return serializer.Deserialize(reader) as UANodeSet;
            }
        }
    }
}
