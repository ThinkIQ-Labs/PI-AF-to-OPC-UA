using PIAF_Interop;
using ua.model.sdk.Managers;
using ua.model.sdk.Model;
using System.Xml;
using ModelCompiler;

afEntry afEntry = new afEntry();
//afEntry.fileUrl = "./data/element templates.xml";
afEntry.fileUrl = @"C:\\Users\\GregorVilkner\\source\\repos\\SMIP2PIAF\\AF Sample Files\\Alone_Refridgerator with Water Dispenser - Copy.xml";
afEntry.ReadFile();

uaModelDesign md = new uaModelDesign("https://thinkiq.com/UA", "fridge");
var uaNamespace = md.uaNameSpaces["OpcUa"].NameSpace.Value;
var fridgeNamespace = md.uaNameSpaces["fridge"].NameSpace.Value;

foreach (var aAfTemplate in afEntry.afElementTemplates)
{

    var aType = md.uaObjectTypeDesignManager.AddBasicObjectTypeDesign(
        new XmlQualifiedName(aAfTemplate.Name.Replace(" ","_"), fridgeNamespace),
        new XmlQualifiedName("BaseObjectType", uaNamespace));
    aType.ObjectTypeDesign.Description = new LocalizedText()
    {
        Key = "en-US",
        Value = aAfTemplate.Description
    };
    
    string? aAfTemplateBaseTypeString = afEntry.GetAFElementTemplateBaseTypeString(aAfTemplate);
    aType.ObjectTypeDesign.BaseType = aAfTemplateBaseTypeString == null ? null : new System.Xml.XmlQualifiedName(aAfTemplateBaseTypeString, fridgeNamespace);


    foreach (var aAfAttribute in aAfTemplate.AFAttributeTemplate)
    {
        var aProperty = aType.uaPropertyDesignManager.AddBasicPropertyDesign(
            new XmlQualifiedName(aAfAttribute.Name, fridgeNamespace),
            new XmlQualifiedName(aAfAttribute.Type, uaNamespace)
            );
        aProperty.PropertyDesign.Description = new LocalizedText()
        {
            Key = "en-US",
            Value = aAfAttribute.Description
        };
    }


}


var dirInfo = Directory.CreateDirectory("./out");

var xmlFileUrl = $"{dirInfo.FullName}\\test2.xml";
string aXmlFileContent = md.uaModelDesignManager.GenerateXML(xmlFileUrl);
Console.WriteLine(aXmlFileContent);

var csvFileUrl = $"{dirInfo.FullName}\\test2.csv";
string aCsvFileContent = md.uaModelDesignManager.GenerateCSV(csvFileUrl);
Console.WriteLine(aCsvFileContent);

var compilerExecutable = @"C:\Users\public\source\repos\UA-ModelCompiler\build\bin\Debug\net6.0\Opc.Ua.ModelCompiler.exe";
md.uaModelDesignManager.CompileNodeset(compilerExecutable, xmlFileUrl, csvFileUrl, dirInfo.FullName);