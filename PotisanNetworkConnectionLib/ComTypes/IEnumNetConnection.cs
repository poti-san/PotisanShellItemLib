namespace Potisan.Windows.Network.ComTypes;

[ComImport]
[Guid("C08956A0-1CD3-11D1-B1C5-00805FC1270E")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumNetConnection
{
	[PreserveSig]
	int Next(
		uint celt,
		out INetConnection rgelt,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumNetConnection ppenum);
}