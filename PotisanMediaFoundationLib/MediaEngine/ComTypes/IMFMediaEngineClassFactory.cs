using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

[ComImport]
[Guid("4D645ACE-26AA-4688-9BE1-DF3516990B93")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaEngineClassFactory
{
	[PreserveSig]
	int CreateInstance(
		uint dwFlags,
		IMFAttributes? pAttr,
		out IMFMediaEngine ppPlayer);

	[PreserveSig]
	int CreateTimeRange(
		out IMFMediaTimeRange ppTimeRange);

	[PreserveSig]
	int CreateError(
		out IMFMediaError ppError);
}