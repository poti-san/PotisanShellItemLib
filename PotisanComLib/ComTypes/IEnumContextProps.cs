using System;

namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("000001c1-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumContextProps
{
	[PreserveSig]
	int Next(
		uint celt,
		out ComContextProperty pContextProperties,
		out uint pceltFetched);

	[PreserveSig]
	int Skip(
		uint celt);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Clone(
		out IEnumContextProps ppEnumContextProps);

	[PreserveSig]
	int Count(
		out uint pcelt);
}