namespace Potisan.Windows.DXCore.ComTypes;

[ComImport]
[Guid("f0db4c7f-fe5a-42a2-bd62-f2a6cf6fc83e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXCoreAdapter
{
	#region IDXCoreAdapter

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsValid();

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsAttributeSupported(
		in Guid attributeGUID);

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsPropertySupported(
		DXCoreAdapterProperty property);

	[PreserveSig]
	int GetProperty(
		DXCoreAdapterProperty property,
		nuint bufferSize,
		ref byte propertyData);

	[PreserveSig]
	int GetPropertySize(
		DXCoreAdapterProperty property,
		out nuint bufferSize);

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsQueryStateSupported(
		DXCoreAdapterState property);

	[PreserveSig]
	int QueryState(
		DXCoreAdapterState state,
		nuint inputStateDetailsSize,
		in byte inputStateDetails,
		nuint outputBufferSize,
		ref byte outputBuffer);

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsSetStateSupported(
		DXCoreAdapterState property);

	[PreserveSig]
	int SetState(
		DXCoreAdapterState state,
		nuint inputStateDetailsSize,
		in byte inputStateDetails,
		nuint inputDataSize,
		in byte inputData);

	[PreserveSig]
	int GetFactory(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvFactory);

	#endregion IDXCoreAdapter
}