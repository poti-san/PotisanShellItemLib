namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("000001c0-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IContext
{
	[PreserveSig]
	int SetProperty(
		in Guid rpolicyId,
		uint flags,
		[MarshalAs(UnmanagedType.IUnknown)] object pUnk);

	[PreserveSig]
	int RemoveProperty(
		in Guid rPolicyId);

	[PreserveSig]
	int GetProperty(
		in Guid rGuid,
		out uint pFlags,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppUnk);

	[PreserveSig]
	int EnumContextProps(
		out IEnumContextProps ppEnumContextProps);
}