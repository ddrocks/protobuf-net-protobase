# protobuf-net-protobase

An extension to add functionality to define base type includes on derived classes.

## Runtime Installation

You can use the following command in the Package Manager Console:
```ps
Install-Package protobuf-net-protobase
```

## Basic usage

### 1 First Decorate your classes
'IMessage' in this example is an external interface provided by an API library.

```csharp
[ProtoContract]
[ProtoBase(10)] 
class UserMessage : IMessage 
{
	[ProtoMember(1)]
	public int Id { get; set;}

	[ProtoMember(2)]
	public string Name { get; set; }
}

[ProtoContract]
[ProtoBase(20, InterfaceType = typeof(IMessage)] 
class AdminMessage : IMessage, IAdminMessage 
{
	[ProtoMember(1)]
	public int Id { get; set;}

	[ProtoMember(2)]
	public string Name { get; set; }

	[ProtoMember(3)]
	public int Flags { get; set; }
}

[ProtoContract]
[ProtoBase(30, typeof(GenericMessage<string>)]
[ProtoBase(31, typeof(GenericMessage<Guid>)]
class GenericMessage<T> : IMessage 
{
	[ProtoMember(1)]
	public int Id { get; set;}

	[ProtoMember(2)]
	public string Name { get; set; }

	[ProtoMemeber(3)]
	public T Value { get; set; }
}
```
The field number provided in 'ProtoBase' must be unique in the dependency tree.

### Prepare your RuntimeTypeModel

```csharp
// No assembly filter
RuntimeTypeModel.Default.ApplyBase();

// Regex assembly name filter 
RuntimeTypeModel.Default.ApplyBase(new Regex("^(?!System\\.)"));

// Callback assembly filter
RuntimeTypeModel.Default.ApplyBase((assembly) => assembly == GetType().Assembly);
```
All extension calls take another optional parameter for implicit field assignment of base types.
(Default: ImplicitFields.AllPublic)

## Important

This must be called before any serialization happens, to link up the hierarchy dependencies.

