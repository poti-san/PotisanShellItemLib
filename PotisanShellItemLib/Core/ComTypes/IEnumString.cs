namespace PotisanShellItemLib.Core.ComTypes;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("00000101-0000-0000-C000-000000000046")]
public interface IEnumString
{
	[PreserveSig]
	int Next(
		uint celt,
		[MarshalAs(UnmanagedType.LPWStr)]out string rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumString ppenum);
}