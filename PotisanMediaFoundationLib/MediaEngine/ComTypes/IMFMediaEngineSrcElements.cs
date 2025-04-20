#pragma warning disable CA1716 // 識別子はキーワードと同一にすることはできません

namespace Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

[ComImport]
[Guid("7a5e5354-b114-4c72-b991-3131d75032ea")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaEngineSrcElements
{
	[PreserveSig]
	uint GetLength();

	[PreserveSig]
	int GetURL(
		uint index,
		[MarshalAs(UnmanagedType.BStr)] out string? pURL);

	[PreserveSig]
	int GetType(
		uint index,
		[MarshalAs(UnmanagedType.BStr)] out string? pType);

	[PreserveSig]
	int GetMedia(
		uint index,
		[MarshalAs(UnmanagedType.BStr)] out string? pMedia);

	[PreserveSig]
	int AddElement(
		[MarshalAs(UnmanagedType.BStr)] string? pURL,
		[MarshalAs(UnmanagedType.BStr)] string? pType,
		[MarshalAs(UnmanagedType.BStr)] string? pMedia);

	[PreserveSig]
	int RemoveAllElements();
}
