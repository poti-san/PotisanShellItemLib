using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem;

namespace Potisan.Windows.Shell.ComTypes;

/// <summary>
/// SIATTRIBFLAGS。
/// </summary>
[Flags]
public enum ShellItemAttributeOp : uint
{
	And = 0x1,
	Or = 0x2,
	AppCompat = 0x3,
	AllItems = 0x4000,
}

[ComImport]
[Guid("b63ea76d-1f85-456f-a19c-48159efa858b")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellItemArray
{
	[PreserveSig]
	int BindToHandler(
		IBindCtx? pbc,
		in Guid bhid,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppvOut);

	[PreserveSig]
	int GetPropertyStore(
		GetPropertyStoreFlag flags,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[PreserveSig]
	int GetPropertyDescriptionList(
		PropertyKey keyType,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[PreserveSig]
	int GetAttributes(
		ShellItemAttributeOp AttribFlags,
		ShellItemAttribute sfgaoMask,
		out ShellItemAttribute psfgaoAttribs);

	[PreserveSig]
	int GetCount(
		out uint pdwNumItems);

	[PreserveSig]
	int GetItemAt(
		uint dwIndex, out IShellItem? ppsi);

	[PreserveSig]
	int EnumItems(
		out IEnumShellItems ppenumShellItems);
}