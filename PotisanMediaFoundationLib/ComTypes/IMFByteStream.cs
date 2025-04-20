using Potisan.Windows.MediaFoundation.Async.ComTypes;

namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("ad4c1b00-4bf7-422f-9175-756693d9130d")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFByteStream
{
	[PreserveSig]
	int GetCapabilities(
		out uint pdwCapabilities);

	[PreserveSig]
	int GetLength(
		out ulong pqwLength);

	[PreserveSig]
	int SetLength(
		ulong qwLength);

	[PreserveSig]
	int GetCurrentPosition(
		out ulong pqwPosition);

	[PreserveSig]
	int SetCurrentPosition(
		ulong qwPosition);

	[PreserveSig]
	int IsEndOfStream(
		[MarshalAs(UnmanagedType.Bool)] out bool pfEndOfStream);

	[PreserveSig]
	int Read(
		ref byte pb,
		uint cb,
		out uint pcbRead);

	[PreserveSig]
	int BeginRead(
		ref byte pb,
		uint cb,
		IMFAsyncCallback pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndRead(
		IMFAsyncResult pResult,
		out uint pcbRead);

	[PreserveSig]
	int Write(
		in byte pb,
		uint cb,
		out uint pcbWritten);

	[PreserveSig]
	int BeginWrite(
		in byte pb,
		uint cb,
		IMFAsyncCallback pCallback,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndWrite(
		IMFAsyncResult pResult,
		out uint pcbWritten);

	[PreserveSig]
	int Seek(
		MFByteStreamSeekOrigin SeekOrigin,
		long llSeekOffset,
		uint dwSeekFlags,
		out ulong pqwCurrentPosition);

	[PreserveSig]
	int Flush();

	[PreserveSig]
	int Close();
}

//EXTERN_GUID(MF_BYTESTREAM_ORIGIN_NAME, 0xfc358288, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
//	EXTERN_GUID(MF_BYTESTREAM_CONTENT_TYPE, 0xfc358289, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
//	EXTERN_GUID(MF_BYTESTREAM_DURATION, 0xfc35828a, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
//	EXTERN_GUID(MF_BYTESTREAM_LAST_MODIFIED_TIME, 0xfc35828b, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
//#if (WINVER >= _WIN32_WINNT_WIN7) 
//EXTERN_GUID( MF_BYTESTREAM_IFO_FILE_URI, 0xfc35828c, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
//EXTERN_GUID( MF_BYTESTREAM_DLNA_PROFILE_ID, 0xfc35828d, 0x3cb6, 0x460c, 0xa4, 0x24, 0xb6, 0x68, 0x12, 0x60, 0x37, 0x5a);
//EXTERN_GUID( MF_BYTESTREAM_EFFECTIVE_URL, 0x9afa0209, 0x89d1, 0x42af, 0x84, 0x56, 0x1d, 0xe6, 0xb5, 0x62, 0xd6, 0x91);
//EXTERN_GUID( MF_BYTESTREAM_TRANSCODED, 0xb6c5c282, 0x4dc9, 0x4db9, 0xab, 0x48, 0xcf, 0x3b, 0x6d, 0x8b, 0xc5, 0xe0 );
