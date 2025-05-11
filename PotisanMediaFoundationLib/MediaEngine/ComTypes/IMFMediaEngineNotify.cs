namespace Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

[ComImport]
[Guid("fee7c112-e776-42b5-9bbf-0048524e2bd5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFMediaEngineNotify
{
	[PreserveSig]
	int EventNotify(
		uint eventType,
		nuint param1,
		uint param2);
}
