namespace Potisan.Windows.Com.Automation.ComTypes;

[ComImport]
[Guid("00020404-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumVARIANT
{
	[PreserveSig]
	int Next(
		uint celt,
		[MarshalAs(UnmanagedType.Struct)] out object? rgVar,
		out uint pCeltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumVARIANT ppEnum);
}