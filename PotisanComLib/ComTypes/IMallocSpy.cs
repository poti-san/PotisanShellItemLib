namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000001d-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IMallocSpy
{
	[PreserveSig]
	nuint PreAlloc(
		nuint RequestSize);

	[PreserveSig]
	nint PostAlloc(
		nint ActualPointer);

	[PreserveSig]
	nint PreFree(
		nint RequestSize,
		[MarshalAs(UnmanagedType.Bool)] bool Spyed);

	[PreserveSig]
	void PostFree(
		[MarshalAs(UnmanagedType.Bool)] bool Spyed);

	[PreserveSig]
	nuint PreRealloc(
		nint RequestPointer,
		nuint RequestSize,
		out nint NewRequestPointer,
		[MarshalAs(UnmanagedType.Bool)] bool Spyed);

	[PreserveSig]
	nint PostRealloc(
		nint ActualPointer,
		[MarshalAs(UnmanagedType.Bool)] bool Spyed);

	[PreserveSig]
	nint PreGetSize(
		nint RequestPointer,
		[MarshalAs(UnmanagedType.Bool)] bool Spyed);

	[PreserveSig]
	nuint PostGetSize(
		nuint ActualSize,
		[MarshalAs(UnmanagedType.Bool)] bool Spyed);

	[PreserveSig]
	nint PreDidAlloc(
		nint RequestPointer,
		[MarshalAs(UnmanagedType.Bool)] bool Spyed);

	[PreserveSig]
	int PostDidAlloc(
		nint RequestPointer,
		[MarshalAs(UnmanagedType.Bool)] bool Spyed,
		int Actual);

	[PreserveSig]
	void PreHeapMinimize();

	[PreserveSig]
	void PostHeapMinimize();
}
