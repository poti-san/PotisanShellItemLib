using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem;

namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("7e9fb0d3-919f-4307-ab2e-9b1860310c93")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellItem2 // : public IShellItem
{
	#region IShellItem
	[PreserveSig]
	int BindToHandler(
		IBindCtx? pbc,
		in Guid bhid,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int GetParent(
		out IShellItem? ppsi);

	[PreserveSig]
	int GetDisplayName(
		ShellItemDisplayName sigdnName,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszName);

	[PreserveSig]
	int GetAttributes(
		ShellItemAttribute sfgaoMask,
		out ShellItemAttribute psfgaoAttribs);

	[PreserveSig]
	int Compare(
		IShellItem psi,
		ShellItemCompareHint hint,
		out int piOrder);
	#endregion

	[PreserveSig]
	int GetPropertyStore(
		GetPropertyStoreFlag flags,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[PreserveSig]
	int GetPropertyStoreWithCreateObject(
		GetPropertyStoreFlag flags,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkCreateObject,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[PreserveSig]
	int GetPropertyStoreForKeys(
		[MarshalAs(UnmanagedType.LPArray)] PropertyKey[] rgKeys,
		uint cKeys,
		GetPropertyStoreFlag flags,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[PreserveSig]
	int GetPropertyDescriptionList(
		PropertyKey keyType,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[PreserveSig]
	int Update(
		IBindCtx? pbc);

	[PreserveSig]
	int GetProperty(
		PropertyKey key,
		[Out] PropVariant ppropvar);

	[PreserveSig]
	int GetCLSID(
		PropertyKey key,
		out Guid pclsid);

	[PreserveSig]
	int GetFileTime(
		PropertyKey key,
		out FileTime pft);

	[PreserveSig]
	int GetInt32(
		PropertyKey key,
		out int pi);

	[PreserveSig]
	int GetString(
		PropertyKey key,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppsz);

	[PreserveSig]
	int GetUInt32(
		PropertyKey key,
		out uint pui);

	[PreserveSig]
	int GetUInt64(
		PropertyKey key,
		out ulong pull);

	[PreserveSig]
	int GetBool(
		PropertyKey key,
		[MarshalAs(UnmanagedType.Bool)] out bool pf);
}