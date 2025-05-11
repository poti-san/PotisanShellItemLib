namespace Potisan.Windows.DXCore.ComTypes;

[ComImport]
[Guid("78ee5945-c36e-4b13-a669-005dd11c0f06")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXCoreAdapterFactory
{
	#region IDXCoreAdapterFactory

	[PreserveSig]
	int CreateAdapterList(
		uint numAttributes,
		[MarshalAs(UnmanagedType.LPArray)] Guid[]? filterAttributes,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvAdapterList);

	[PreserveSig]
	int GetAdapterByLuid(
		in Luid adapterLUID,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvAdapter);

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsNotificationTypeSupported(
		DXCoreNotificationType notificationType);

	[PreserveSig]
	int RegisterEventNotification(
		[MarshalAs(UnmanagedType.IUnknown)] object? dxCoreObject,
		DXCoreNotificationType notificationType,
		DXCoreNotificationCallback callbackFunction,
		nuint callbackContext,
		out uint eventCookie);

	[PreserveSig]
	int UnregisterEventNotification(
		uint eventCookie);

	#endregion IDXCoreAdapterFactory
}