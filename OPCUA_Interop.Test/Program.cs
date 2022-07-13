using OPCUA_Interop;

uaEntry aEntry = new uaEntry();
aEntry.fileUrl = "./data/test2.xml";
aEntry.ReadFile();

foreach (var aType in aEntry.uaTypeDesings)
{
    Console.WriteLine(aType.SymbolicName);
}