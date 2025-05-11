namespace Potisan.Windows.DXCore.ComTypes;

[ComImport]
[Guid("d5682e19-6d21-401c-827a-9a51a4ea35d7")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXCoreAdapterFactory1 // IDXCoreAdapterFactory
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

	[PreserveSig]
	int CreateAdapterListByWorkload(
		DXCoreWorkload workload,
		DXCoreRuntimeFilterFlags runtimeFilter,
		DXCoreHardwareTypeFilterFlags hardwareTypeFilter,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvAdapterList);
}