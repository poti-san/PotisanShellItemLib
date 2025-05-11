namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("C1209904-E584-4752-A2D6-7F21693F8B21")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFSecureBuffer
{
	[PreserveSig]
	int GetIdentifier(
		out Guid pGuidIdentifier);
}