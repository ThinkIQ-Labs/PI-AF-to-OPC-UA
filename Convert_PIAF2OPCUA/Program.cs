using PIAF_Interop;
using OPCUA_Interop;
using System.Xml;

afEntry afEntry = new afEntry();
afEntry.fileUrl = "./data/element templates.xml";
afEntry.ReadFile();

uaEntry uaEntry = new uaEntry();

var domain = "https://thinkiq.com/UA";
var name = "fridge";
var myNameSpace = $"{domain}/{name}/";

uaEntry.CreateNewModel(domain, name);

foreach (var aAfTemplate in afEntry.afElementTemplates)
{

    var aType = uaEntry.uaObjectTypeDesignManager.AddBasicTypeDesign(aAfTemplate.Name.Replace(" ","_"), aAfTemplate.Description, myNameSpace);
    
    string? aAfTemplateBaseTypeString = afEntry.GetAFElementTemplateBaseTypeString(aAfTemplate);
    aType.objectTypeDesign.BaseType = aAfTemplateBaseTypeString == null ? null : new System.Xml.XmlQualifiedName(aAfTemplateBaseTypeString, myNameSpace);


    foreach (var aAfAttribute in aAfTemplate.AFAttributeTemplate)
    {
        aType.uaPropertyDesignManager.AddBasicPropertyDesign(aAfAttribute.Name, aAfAttribute.Type, ValueRank.Scalar, ModellingRule.Mandatory, aAfAttribute.Description, myNameSpace);
    }


}


var dirInfo = Directory.CreateDirectory("./out");

var xmlFileUrl = $"{dirInfo.FullName}\\test2.xml";
string aXmlFileContent = uaEntry.GenerateXML(xmlFileUrl);
Console.WriteLine(aXmlFileContent);

var csvFileUrl = $"{dirInfo.FullName}\\test2.csv";
string aCsvFileContent = uaEntry.GenerateCSV(csvFileUrl);
Console.WriteLine(aCsvFileContent);

var compilerExecutable = @"C:\Users\GregorVilkner\source\repos\UA-ModelCompiler\build\bin\Debug\net6.0\Opc.Ua.ModelCompiler.exe";
uaEntry.CompileNodeset(compilerExecutable, xmlFileUrl, csvFileUrl, dirInfo.FullName);