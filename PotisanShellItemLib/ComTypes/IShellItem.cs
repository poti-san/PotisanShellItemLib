using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellItem
{
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
}