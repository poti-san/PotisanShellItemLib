using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("07f7438c-7709-4ca5-b518-91279288134e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IUpdateCollection // IDispatch
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
	int get_Item(
		int index,
		out IUpdate retval);

	[PreserveSig]
	int put_Item(
		int index,
		IUpdate value);

	[PreserveSig]
	int get__NewEnum(
		[MarshalAs(UnmanagedType.IUnknown)] out object? retval);

	[PreserveSig]
	int get_Count(
		out int retval);

	[PreserveSig]
	int get_ReadOnly(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int Add(
		IUpdate value,
		out int retval);

	[PreserveSig]
	int Clear();

	[PreserveSig]
	int Copy(
		out IUpdateCollection retval);

	[PreserveSig]
	int Insert(
		int index,
		IUpdate value);

	[PreserveSig]
	int RemoveAt(
		int index);
}
