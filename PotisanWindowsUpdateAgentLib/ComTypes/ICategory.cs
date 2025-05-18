using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("81ddc1b8-9d35-47a6-b471-5b80f519223b")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ICategory // IDispatch
{
	#region IDispatch

	[PreserveSig]
	int GetTypeInfoCount(
		out uint pctinfo);

	[PreserveSig]
	int GetTypeInfo(
		uint iTInfo,
		Lcid lcid,
		out ITypeInfo ppTInfo);

	[PreserveSig]
	int GetIDsOfNames(
		in Guid riid,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rgszNames,
		uint cNames,
		Lcid lcid,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out int[] rgDispId);

	[PreserveSig]
	int Invoke(
		ComMemberID dispIdMember,
		in Guid riid,
		Lcid lcid,
		ushort wFlags,
		[In, Out] DISPPARAMS pDispParams,
		[MarshalAs(UnmanagedType.Struct)] out object pVarResult,
		[Out] ComExceptionInfo pExcepInfo,
		out uint puArgErr);

	#endregion IDispatch

	[PreserveSig]
	int get_Name(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_CategoryID(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_Children(
		out ICategoryCollection retval);

	[PreserveSig]
	int get_Description(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_Image(
		out IImageInformation retval);

	[PreserveSig]
	int get_Order(
		out int retval);

	[PreserveSig]
	int get_Parent(
		out ICategory retval);

	[PreserveSig]
	int get_Type(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int get_Updates(
		out IUpdateCollection retval);
}