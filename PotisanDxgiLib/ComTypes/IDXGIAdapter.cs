namespace Potisan.Windows.Dxgi.ComTypes;

[ComImport]
[Guid("2411e7e1-12ac-4ccf-bd14-9798e8534dc0")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IDXGIAdapter // IDXGIObject
{
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
	int EnumOutputs(
		uint Output,
		out IDXGIOutput ppOutput);

	[PreserveSig]
	int GetDesc(
		out DxgiAdapterDesc pDesc);

	[PreserveSig]
	int CheckInterfaceSupport(
		in Guid InterfaceName,
		out long pUMDVersion);
}