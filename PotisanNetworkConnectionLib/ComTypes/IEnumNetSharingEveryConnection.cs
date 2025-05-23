namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("C08956B8-1CD3-11D1-B1C5-00805FC1270E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumNetSharingEveryConnection // IUnknown
{
	[PreserveSig]
	int Next(
		uint celt,
		[MarshalAs(UnmanagedType.Struct)] out object rgVar,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumNetSharingEveryConnection ppenum);
}