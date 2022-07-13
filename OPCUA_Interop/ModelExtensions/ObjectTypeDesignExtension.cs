using OPCUA_Interop.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCUA_Interop.ModelExtensions
{
    public class ObjectTypeDesignExtension 
    {
        public ObjectTypeDesign objectTypeDesign { get; set; }
        public uaPropertyDesignManager uaPropertyDesignManager { get; set; }
        public ObjectTypeDesignExtension(ObjectTypeDesign objectTypeDesign)
        {
            this.objectTypeDesign = objectTypeDesign;
            uaPropertyDesignManager = new uaPropertyDesignManager(objectTypeDesign.Children);
        }
    }
}
