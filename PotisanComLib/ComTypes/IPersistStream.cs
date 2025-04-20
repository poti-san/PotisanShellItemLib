namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("00000109-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IPersistStream : IPersist
{
	[PreserveSig]
	int IsDirty();

	[PreserveSig]
	int Load(IStream pStm);

	[PreserveSig]
	int Save(
		IStream pStm,
		[MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

	[PreserveSig]
	int GetSizeMax(
		out ulong pcbSize);
}