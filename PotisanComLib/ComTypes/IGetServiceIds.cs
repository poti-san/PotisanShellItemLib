namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("4A073526-6103-4E21-B7BC-F519D1524E5D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IGetServiceIds
{
	[PreserveSig]
	int GetServiceIds(
		out uint serviceIdCount,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] out Guid[] serviceIds);
}