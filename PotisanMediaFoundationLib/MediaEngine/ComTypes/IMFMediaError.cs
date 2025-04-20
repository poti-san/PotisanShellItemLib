#pragma warning disable CA1716 // 識別子はキーワードと同一にすることはできません

namespace Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

[ComImport]
[Guid("fc0e10d2-ab2a-4501-a951-06bb1075184c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaError
{
	[PreserveSig]
	ushort GetErrorCode();

	[PreserveSig]
	int GetExtendedErrorCode();

	[PreserveSig]
	int SetErrorCode(
		MFMediaEngineError error);

	[PreserveSig]
	int SetExtendedErrorCode(
		int error);
}