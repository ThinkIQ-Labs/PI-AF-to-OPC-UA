using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OPCUA_Interop.Managers
{
    public class uaPropertyDesignManager
    {
        ListOfChildren host { get; set; }

        public uaPropertyDesignManager(ListOfChildren host)
        {
            this.host = host;
            this.host.Items = new InstanceDesign[] { };
        }

        // <Property SymbolicName="ANIMAL:Name" DataType="ua:String" ValueRank="Scalar" ModellingRule="Mandatory">
        //      < Description > Name of the animal </ Description >
        //  </ Property >
        public PropertyDesign AddBasicPropertyDesign(string SymbolicName, string DataType, ValueRank valueRank, ModellingRule modellingRule, string Description,string NameSpace)
        {
            PropertyDesign newPropertyDesign = new PropertyDesign
            {
                SymbolicName = new XmlQualifiedName(SymbolicName, NameSpace),
                DataType = new XmlQualifiedName(DataType, "http://opcfoundation.org/UA/"),
                ValueRank = valueRank,
                ModellingRule = modellingRule,
                Description = new LocalizedText()
                {
                    Value = Description
                }
            };

            List<InstanceDesign> items = host.Items.ToList();
            items.Add(newPropertyDesign);
            host.Items = items.ToArray();

            return newPropertyDesign;
        }
    }
}
