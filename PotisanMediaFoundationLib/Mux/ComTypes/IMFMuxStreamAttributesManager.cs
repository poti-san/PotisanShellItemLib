using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation.Mux.ComTypes;

[ComImport]
[Guid("CE8BD576-E440-43B3-BE34-1E53F565F7E8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMuxStreamAttributesManager
{
	[PreserveSig]
	int GetStreamCount(
		out uint pdwMuxStreamCount);

	[PreserveSig]
	int GetAttributes(
		uint dwMuxStreamIndex,
		out IMFAttributes ppStreamAttributes);
}