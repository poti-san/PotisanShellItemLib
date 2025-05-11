namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("70629033-e363-4a28-a567-0db78006e6d7")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumShellItems
{
	[PreserveSig]
	int Next(
		uint celt,
		out IShellItem? rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumShellItems? ppenum);
}