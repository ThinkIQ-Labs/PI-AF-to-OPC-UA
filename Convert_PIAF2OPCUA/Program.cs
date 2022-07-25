using PIAF_Interop;
using ua.model.sdk.Model;
using System.Xml;

afEntry afEntry = new afEntry();
afEntry.fileUrl = "./data/element templates.xml";
//afEntry.fileUrl = @"C:\\Users\\GregorVilkner\\source\\repos\\SMIP2PIAF\\AF Sample Files\\Alone_Refridgerator with Water Dispenser - Copy.xml";
afEntry.ReadFile();

ModelDesign md = new ModelDesign("https://thinkiq.com/UA", "fridge");
var uaNamespace = md.NamespacesDictionary["OpcUa"].Value;
var fridgeNamespace = md.NamespacesDictionary["fridge"].Value;

foreach (var aAfTemplate in afEntry.afElementTemplates)
{

    var aType = md.ObjectTypeDesignsAdd(
        new XmlQualifiedName(aAfTemplate.Name.Replace(" ","_"), fridgeNamespace),
        new XmlQualifiedName("BaseObjectType", uaNamespace));
    aType.Description = new LocalizedText()
    {
        Key = "en-US",
        Value = aAfTemplate.Description
    };
    
    string? aAfTemplateBaseTypeString = afEntry.GetAFElementTemplateBaseTypeString(aAfTemplate);
    aType.BaseType = aAfTemplateBaseTypeString == null ? null : new System.Xml.XmlQualifiedName(aAfTemplateBaseTypeString, fridgeNamespace);


    foreach (var aAfAttribute in aAfTemplate.AFAttributeTemplate)
    {
        var aProperty = aType.PropertyDesignsAdd(
            new XmlQualifiedName(aAfAttribute.Name, fridgeNamespace),
            new XmlQualifiedName(aAfAttribute.Type, uaNamespace)
            );
        aProperty.Description = new LocalizedText()
        {
            Key = "en-US",
            Value = aAfAttribute.Description
        };
    }


}


var dirInfo = Directory.CreateDirectory("./out");

var xmlFileUrl = $"{dirInfo.FullName}\\test2.xml";
string aXmlFileContent = md.GenerateXML(xmlFileUrl);
Console.WriteLine(aXmlFileContent);

var csvFileUrl = $"{dirInfo.FullName}\\test2.csv";
string aCsvFileContent = md.GenerateCSV(csvFileUrl);
Console.WriteLine(aCsvFileContent);

var compilerExecutable = @"C:\Users\public\source\repos\UA-ModelCompiler\build\bin\Debug\net6.0\Opc.Ua.ModelCompiler.exe";
md.CompileNodeset(compilerExecutable, xmlFileUrl, csvFileUrl, dirInfo.FullName);