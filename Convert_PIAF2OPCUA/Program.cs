using PIAF_Interop;
using OPCUA_Interop;

afEntry afEntry = new afEntry();
afEntry.fileUrl = "./data/element templates.xml";
afEntry.ReadFile();

uaEntry uaEntry = new uaEntry();

var domain = "https://opcua.rocks/UA";
var name = "animal";
var myNameSpace = $"{domain}/{name}/";

uaEntry.CreateNewModel(domain, name);

var aType = uaEntry.uaObjectTypeDesignManager.AddBasicTypeDesign("AnimalType", "Base type for all animals", myNameSpace);

aType.uaPropertyDesignManager.AddBasicPropertyDesign("Name", "String", ValueRank.Scalar, ModellingRule.Mandatory, "Name of the animal", myNameSpace);

string aFile = uaEntry.GenerateXML("./test2.xml");


Console.WriteLine(aFile);