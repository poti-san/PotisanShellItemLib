namespace Potisan.Windows.DXCore.ComTypes;

[ComImport]
[Guid("526c7776-40e9-459b-b711-f32ad76dfc28")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXCoreAdapterList
{
	[PreserveSig]
	int GetAdapter(
		uint index,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvAdapter);

	[PreserveSig]
	uint GetAdapterCount();

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsStale();

	[PreserveSig]
	int GetFactory(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvFactory);

	[PreserveSig]
	int Sort(
		uint numPreferences,
		[MarshalAs(UnmanagedType.LPArray)] DXCoreAdapterPreference[] preferences);

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Bool)]
	bool IsAdapterPreferenceSupported(
		DXCoreAdapterPreference preference);
}