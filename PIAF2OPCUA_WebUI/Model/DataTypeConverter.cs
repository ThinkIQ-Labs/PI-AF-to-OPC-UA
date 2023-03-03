namespace PIAF2OPCUA_WebUI.Model
{
    static public class DataTypeConverter
    {
        static public string PIAF2OPCUA(string piAfDataType)
        {
            switch (piAfDataType)
            {
                case "boolean":
                    return "Boolean";
                case "integer":
                    return "Integer";
                case "float":
                    return "Float";
                case "string":
                    return "String";
                case "datetime":
                    return "DateTime";
                case "interval":
                    // hmmm, how do you do intervals in OPC UA?
                    return "String";
                case "object":
                    // hmmm, how do you do json in OPC UA?
                    return "String";
                case "enumeration":
                    // hmmm, how do you do enumeration in OPC UA?
                    return "Enumeration";
                case "geopoint":
                    // hmmm, how do you do geo locations in OPC UA?
                    return "String";
                case "reference":
                    // hmmm, how do you do references in OPC UA?
                    return "Reference";
                default:
                    return "String";
            }
        }
        
        static public string OPCUA2PIAF(string opcUaDataType)
        {
            switch (opcUaDataType)
            {
                case "Boolean":
                    return "Boolean";
                case "Integer":
                    return "Int64";
                case "Float":
                    return "Double";
                case "String":
                    return "String";
                case "Datetime":
                    return "DateTime";
                default:
                    return "String";
            }
        }
    }
}
