using PIAF_Interop;
using PIAF2OPCUA_WebUI.Pages;

namespace PIAF2OPCUA_WebUI.Model
{
    public class AfUiType
    {
        public bool isSelected { get; set; }

        public PIAF2OPCUA_Component ViewModel { get; set; }

        public AfUiType(PIAF2OPCUA_Component comp)
        {
            ViewModel = comp;
        }

        public string Name
        {
            get
            {
                return AfType.Name;
            }
        }
        public AFElementTemplate AfType { get; set; }
        public AFElementTemplate AfBaseType { get; set; }
        public string? BaseTypeString { get; set; }
        public string canBeAddedErrorHint { get; set; }
        public bool canBeAdded
        {
            get
            {
                // can't add because there is no model
                if (ViewModel.nodeSetModel == null)
                {
                    return false;
                }

                // can't add because it's alreay added
                if (isSelected)
                {
                    return false;
                }

                // can't add because base type is not loaded
                if (AfBaseType != null)
                {
                    if (ViewModel.AfUiTypes.First(x => x.AfType.Name == AfBaseType.Name).isSelected == false)
                    {
                        canBeAddedErrorHint = $"BaseType ({AfBaseType.Name}) must be added first.";
                        return false;
                    }
                }

                return true;
            }
        }
        public string canBeRemovedErrorHint { get; set; }
        public bool canBeRemoved
        {
            get
            {
                // can't remove because it's not selected yet
                if (!isSelected)
                {
                    return false;
                }

                foreach (var aType in ViewModel.AfUiTypes.Where(x => x.isSelected))
                {
                    // can't remove because it's a basetype for something
                    if (aType.AfBaseType != null)
                    {
                        if (aType.AfBaseType.Name == AfType.Name)
                        {
                            canBeRemovedErrorHint = $"This is a BaseType for {aType.AfType.Name}. It must be removed first.";
                            return false;
                        }
                    }
                }

                // if we're still alive it's ok to remove
                return true;
            }
        }


    }
}
