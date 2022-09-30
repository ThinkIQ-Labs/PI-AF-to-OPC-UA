# Creating OPC UA Nodesets From Aveva PI Asset Framework Element Templates

This project leverages XML interoperability standards to allow creation of OPC UA Information Models and Nodesets from Asset Framework (AF) Element Templates from AVEVA's PI System Explorer (PI AF). The reverse functionality, creation of PI AF Element Templates from OPC UA Information Models and Nodesets, is not yet supported, but planned.

A program was created with a user interface to import and browse AF templates.

![Screenshot](./images/af2ua_screenshot.png)
<p align = "center"><b>Fig.1 - Screenshot of program UI</b></p>

The first step is to create a new UA Model by entering a domain and name. The program automatically references the typical basic OPC UA namespaces. For instance, "ht<span>tps://</span>acme.com/UA" and "refridgerators" will result in the following OPC UA design model:

``` xml
<?xml version="1.0" encoding="utf-16"?>
<ModelDesign xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:uax="http://opcfoundation.org/UA/2008/02/Types.xsd" xmlns:ua="http://opcfoundation.org/UA/" xmlns:refridgerator="https://acme.com/refridgerator/" TargetNamespace="https://acme.com/refridgerator/" TargetXmlNamespace="https://acme.com/refridgerator/" xmlns="http://opcfoundation.org/UA/ModelDesign.xsd">
  <Namespaces>
    <Namespace Name="OpcUa" Prefix="Opc.Ua" InternalPrefix="Opc.Ua.Server" XmlNamespace="http://opcfoundation.org/UA/2008/02/Types.xsd" XmlPrefix="OpcUa" Version="1.03" PublicationDate="2013-12-02T00:00:00Z">http://opcfoundation.org/UA/</Namespace>
    <Namespace Name="refridgerator" Prefix="refridgerator" XmlNamespace="https://acme.com/refridgerator/Types.xsd" XmlPrefix="refridgerator">https://acme.com/refridgerator/</Namespace>
  </Namespaces>
</ModelDesign>
```

Once an XML file with AF element templates is loaded, element templates can be added as BasicObjectTypes to the OPC UA design model by using the "+" icons next to the element template names. Element templates can be removed from the model design the same way. At any time, one can preview and save the generated ModelDesign.xml text. The Nodeset.xml can also be compiled. This action is performed on the server utilizing the OPC UA model compiler library.

![Screenshot](./images/af2ua_model_preview_screenshot.png)
<p align = "center"><b>Fig.2 - Preview and Save ModelDesign and Nodeset XML Files</b></p>

## Modeling Techniques Supported in PI AF 

PI AF offers two basic types of objects that models are based upon: elements and attributes, both of which can be nested. Elements can be based on types, Element Templates, which are stored in a model library’s “Element Templates” section. An Element Template can be based of a single other Element Template. This allows inheritance and chaining of dependencies.

Even though element instances can be nested, element templates can not be nested, i.e. it is not possible to create composite types. However, within an element template, attribute templates can be nested. For the purpose of exporting AF element templates to OPC UA, one can either disregard nested attributes, or flatten them. Flattening nested attributes would preserve them for usage in the information model, but ultimately change the structure of the type, and possibly create issues with unique naming of sibling attributes.

## OPC UA Information Model Assumptions

Objects can be added into a newly created namespace. At this time, appending or editing of existing Information Models or Nodesets is not supported. The workflow builds an OPC UA model first (ModelDesign.xsd), and then compiles a Nodeset off it using the OPC UA model compiler.

Only a few basic features of the rich set of OPC UA object types and namespaces are used to effectively campture AF Element Templates:

- Element Templates are modeled as BaseObjectType.
- Element Templates that are based on another Element Template are modeled the same way. Thus, it is required to "work your way up", meaning that templates are only allowed to be added to an Information Model once the dependency exists.
- Attribute Templates are modeled either as BaseDataVariableType or as PropertyType.
- Time series attributes and attributes that are numeric are modeled as variables, so engineering units can be attached.
- Engineering Units are captured in a property called "EngineeringUnits" based on the EUInformation type, which leverages the UNICE library of engineering units. If the DefaultUoM doesn't match, an exception message is shown.
- Since AF does not allow nested element types, it is not possible to create complex types that encapsulate nameplate metadata such as the MachineIdentificationType.
- The modeling rule of all properties and variables is set to its default state. This is mandatory. 

## Element Template Meta-Data

- Name - is captured.
- Description - is captured.
- Base Template - is captured.
- Type - only Elements are considered.
- Categories - omitted. This is AF specific.
- Default Attribute - omitted. This is AF specific.
- Naming Pattern - omitted. This does not exist in the SMIP.

## Attribute Template Meta-Data

- Name - is captured.
- Description - is captured.
- Properties - omitted. This is AF specific.
- Categories - omitted. This is AF specific.
- Default UoM - is captured and validated against OPC UA engineering units.
- Default Value - is captured.
- Display Digits - omitted. This is AF specific.
- Data Reference - PI Point attributes will be marked for Historizing.

### Value Type Mapping

All data types supported by AF are available in OPC UA.


