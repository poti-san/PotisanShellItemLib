using Potisan.Windows.Com.Automation.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua.ComTypes;

[ComImport]
[Guid("174c81fe-aecd-4dae-b8a0-2c6318dd86a8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IWebProxy // IDispatch
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
	int get_Address(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int put_Address(
		[MarshalAs(UnmanagedType.BStr)] string? value);

	[PreserveSig]
	int get_BypassList(
		out IStringCollection retval);

	[PreserveSig]
	int put_BypassList(
		IStringCollection value);

	[PreserveSig]
	int get_BypassProxyOnLocal(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_BypassProxyOnLocal(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);

	[PreserveSig]
	int get_ReadOnly(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int get_UserName(
		[MarshalAs(UnmanagedType.BStr)] out string? retval);

	[PreserveSig]
	int put_UserName(
		[MarshalAs(UnmanagedType.BStr)] string? value);

	[PreserveSig]
	int SetPassword(
		[MarshalAs(UnmanagedType.BStr)] string value);

	[PreserveSig]
	int PromptForCredentials(
		[MarshalAs(UnmanagedType.IUnknown)] object? parentWindow,
		[MarshalAs(UnmanagedType.BStr)] string title);

	[PreserveSig]
	int PromptForCredentialsFromHwnd(
		nint parentWindow,
		[MarshalAs(UnmanagedType.BStr)] string title);

	[PreserveSig]
	int get_AutoDetect(
		[MarshalAs(UnmanagedType.VariantBool)] out bool retval);

	[PreserveSig]
	int put_AutoDetect(
		[MarshalAs(UnmanagedType.VariantBool)] bool value);
}