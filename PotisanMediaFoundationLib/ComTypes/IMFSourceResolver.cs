using Potisan.Windows.MediaFoundation.Async.ComTypes;
using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("FBE5A32D-A497-4b61-BB85-97B1A848A6E3")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFSourceResolver
{
	[PreserveSig]
	int CreateObjectFromURL(
		string pwszURL,
		uint dwFlags,
		IPropertyStore? pProps,
		out MFObjectType pObjectType,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppObject);

	[PreserveSig]
	int CreateObjectFromByteStream(
		IMFByteStream? pByteStream,
		string? pwszURL,
		uint dwFlags,
		IPropertyStore? pProps,
		out MFObjectType pObjectType,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppObject);

	[PreserveSig]
	int BeginCreateObjectFromURL(
		[MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
		uint dwFlags,
		IPropertyStore? pProps,
		out nint ppIUnknownCancelCookie,
		IMFAsyncCallback pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndCreateObjectFromURL(
		IMFAsyncResult pResult,
		out MFObjectType pObjectType,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppObject);

	[PreserveSig]
	int BeginCreateObjectFromByteStream(
		IMFByteStream pByteStream,
		[MarshalAs(UnmanagedType.LPWStr)] string pwszURL,
		uint dwFlags,
		IPropertyStore? pProps,
		out nint ppIUnknownCancelCookie,
		IMFAsyncCallback pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndCreateObjectFromByteStream(
		IMFAsyncResult pResult,
		out MFObjectType pObjectType,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppObject);

	[PreserveSig]
	int CancelObjectCreation(
		[MarshalAs(UnmanagedType.IUnknown)] object? pIUnknownCancelCookie);
}
