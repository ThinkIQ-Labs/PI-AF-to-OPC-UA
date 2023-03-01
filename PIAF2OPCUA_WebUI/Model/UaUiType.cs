using CESMII.OpcUa.NodeSetModel;
using PIAF2OPCUA_WebUI.Pages;

namespace PIAF2OPCUA_WebUI.Model
{
    public class UaUiType
    {
        public bool isSelected { get; set; }

        public OPCUA2PIAF_Component ViewModel { get; set; }

        public UaUiType(OPCUA2PIAF_Component comp)
        {
            ViewModel = comp;
        }

        public string Name
        {
            get
            {
                return UaType.DisplayName.FirstOrDefault().Text;
            }
        }
        public ObjectTypeModel UaType { get; set; }
        public BaseTypeModel UaBaseType { get; set; }
        public string? BaseTypeString { get; set; }
        public string canBeAddedErrorHint { get; set; }
        public bool canBeAdded
        {
            get
            {
                // can't add because there is no model
                if (ViewModel.afModel == null)
                {
                    return false;
                }

                // can't add because it's alreay added
                if (isSelected)
                {
                    return false;
                }

                // can't add because base type is not loaded
                if (UaBaseType != null)
                {
                    if (ViewModel.UaUiTypes.First(x => x.UaType.DisplayName.FirstOrDefault().Text == UaBaseType.DisplayName.FirstOrDefault().Text).isSelected == false)
                    {
                        canBeAddedErrorHint = $"BaseType ({UaBaseType.DisplayName.FirstOrDefault().Text}) must be added first.";
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

                foreach (var aType in ViewModel.UaUiTypes.Where(x => x.isSelected))
                {
                    // can't remove because it's a basetype for something
                    if (aType.UaBaseType != null)
                    {
                        if (aType.UaBaseType.DisplayName.FirstOrDefault().Text == UaType.DisplayName.FirstOrDefault().Text)
                        {
                            canBeRemovedErrorHint = $"This is a BaseType for {aType.UaType.DisplayName.FirstOrDefault().Text}. It must be removed first.";
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
