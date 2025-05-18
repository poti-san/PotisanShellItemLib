namespace Potisan.Windows.Com.Automation.ComTypes;

[ComImport]
[Guid("00020403-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITypeComp
{
	[PreserveSig]
	int Bind(
		[MarshalAs(UnmanagedType.LPWStr)] string szName,
		uint lHashVal,
		ComInvokeKind wFlags,
		out ITypeInfo ppTInfo,
		out ComDescriptionKind pDescKind,
		out BINDPTR pBindPtr);

	[PreserveSig]
	int BindType(
		[MarshalAs(UnmanagedType.LPWStr)] string szName,
		uint lHashVal,
		out ITypeInfo ppTInfo,
		out ITypeComp ppTComp);
}

[StructLayout(LayoutKind.Explicit)]
public struct BINDPTR
{
	[FieldOffset(0)]
	public nint lpfuncdesc; // FUNCDESC*
	[FieldOffset(0)]
	public nint lpvardesc; // VARDESC*
	[FieldOffset(0)]
	public nint lptcomp; // ITypeComp*
}
