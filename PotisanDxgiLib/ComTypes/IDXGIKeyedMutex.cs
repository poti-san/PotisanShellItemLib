using System.Runtime.InteropServices;

namespace Potisan.Windows.Dxgi.ComTypes;

[ComImport]
[Guid("9d8e1289-d7b3-465f-8126-250e349af85d")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXGIKeyedMutex // IDXGIDeviceSubObject
{
	#region IDXGIDeviceSubObject

	#region IDXGIObject

	[PreserveSig]
	int SetPrivateData(
		in Guid Name,
		uint DataSize,
		in byte pData);

	[PreserveSig]
	int SetPrivateDataInterface(
		in Guid Name,
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnknown);

	[PreserveSig]
	int GetPrivateData(
		in Guid Name,
		ref uint pDataSize,
		ref byte pData);

	[PreserveSig]
	int GetParent(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppParent);

	#endregion IDXGIObject

	[PreserveSig]
	int GetDevice(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppDevice);

	#endregion IDXGIDeviceSubObject

	[PreserveSig]
	int AcquireSync(
		ulong Key,
		uint dwMilliseconds);

	[PreserveSig]
	int ReleaseSync(
		ulong Key);
}