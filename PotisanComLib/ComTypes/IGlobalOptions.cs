namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000015B-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IGlobalOptions
{
	[PreserveSig]
	int Set(
		GlobalOptionProperties dwProperty,
		nuint dwValue);

	[PreserveSig]
	int Query(
		GlobalOptionProperties dwProperty,
		out nuint pdwValue);
}