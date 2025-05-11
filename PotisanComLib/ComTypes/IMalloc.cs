namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000002-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IMalloc
{
	[PreserveSig]
	nint Alloc(
		nuint cb);

	[PreserveSig]
	nint Realloc(
		nint pv,
		nuint cb);

	[PreserveSig]
	void Free(
		nint pv);

	[PreserveSig]
	nuint GetSize(
		nint pv);

	[PreserveSig]
	int DidAlloc(
		nint pv);

	[PreserveSig]
	void HeapMinimize();
}