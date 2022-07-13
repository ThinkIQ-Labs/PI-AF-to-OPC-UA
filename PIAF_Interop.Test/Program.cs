using PIAF_Interop;

afEntry aEntry = new afEntry();
aEntry.fileUrl = "./data/Element Templates.xml";
aEntry.ReadFile();

foreach(var aType in aEntry.afElementTemplates)
{
    Console.WriteLine(aType.Name);
}