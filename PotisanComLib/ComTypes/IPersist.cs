namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000010c-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IPersist
{
	[PreserveSig]
	int GetClassID(out Guid pClassID);
}