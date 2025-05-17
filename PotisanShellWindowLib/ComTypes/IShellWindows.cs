#pragma warning disable IDE1006, CA1707

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Shell.Window.ComTypes;

[ComImport]
[Guid("85CB6900-4D95-11CF-960C-0080C7F4EE85")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellWindows // IDispatch
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

	#endregion

	[PreserveSig]
	int get_Count(
		out int Count);

	[PreserveSig]
	int Item(
		[MarshalAs(UnmanagedType.Struct)] object index,
		[MarshalAs(UnmanagedType.IDispatch)] out object? Folder);

	[PreserveSig]
	int _NewEnum(
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunk);

	[PreserveSig]
	int Register(
		[MarshalAs(UnmanagedType.IDispatch)] object pid,
		int hwnd,
		int swClass,
		out int plCookie);

	[PreserveSig]
	int RegisterPending(
		int lThreadId,
		[MarshalAs(UnmanagedType.Struct)] in object pvarloc,
		[MarshalAs(UnmanagedType.Struct)] in object pvarlocRoot,
		/* [in] */ int swClass,
		out int plCookie);

	[PreserveSig]
	int Revoke(
		int lCookie);

	[PreserveSig]
	int OnNavigate(
		int lCookie,
		[MarshalAs(UnmanagedType.Struct)] in object pvarLoc);

	[PreserveSig]
	int OnActivated(
		int lCookie,
		[MarshalAs(UnmanagedType.VariantBool)] bool fActive);

	[PreserveSig]
	int FindWindowSW(
		[MarshalAs(UnmanagedType.Struct)] in object pvarLoc,
		[MarshalAs(UnmanagedType.Struct)] in object pvarLocRoot,
		/* [in] */ int swClass,
		out int phwnd,
		/* [in] */ int swfwOptions,
		[MarshalAs(UnmanagedType.IDispatch)] out object? ppdispOut);

	[PreserveSig]
	int OnCreated(
		int lCookie,
		[MarshalAs(UnmanagedType.IUnknown)] object punk);

	[PreserveSig]
	int ProcessAttachDetach(
		[MarshalAs(UnmanagedType.VariantBool)] bool fAttach);
}