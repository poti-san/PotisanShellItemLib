namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("45c35144-154e-4797-bed8-d33ae7bf8794")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfCleanupContextDurationSink
{
	[PreserveSig]
	int OnStartCleanupContext();

	[PreserveSig]
	int OnEndCleanupContext();
}