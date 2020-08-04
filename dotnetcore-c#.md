## Serialization (C#)
1. https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/
2. Serialization is the process of converting an object into a stream of bytes to store the object or transmit it to memory, a database, or a file. 
Its main purpose is to save the state of an object in order to be able to recreate it when needed. The reverse process is called deserialization.

## Attribute Class
1. https://docs.microsoft.com/en-us/dotnet/api/system.attribute?view=netcore-3.1
2. An attribute is a declarative tag that is used convery information to runtime about behaviors various elements liks classes, methods, structures, enumerators, assemblies etc.
Attribute are used for adding metadata, such as compiler instruction and other information such as comments, description, methods and classes to a program. The .Net provides two types of attribute: the pre-defined attributes and custom built attributes.
3. Specifying an attribute: attribute[positional_parameters, name_parameters = value, ...]
3. .Net provides three pre-defined Attributes

| Attributes ||Description|
|:-------------------|
|AttributeUsage      |
|Conditional         |
|Obsolete            |
