// TODO

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("92218CAB-ECAA-4335-8133-807FD234C2EE")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAssocHandlerInvoker
{
	[PreserveSig]
	int SupportsSelection();

	[PreserveSig]
	int Invoke();
}

[ComImport]
[Guid("F04061AC-1659-4a3f-A954-775AA57FC083")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAssocHandler
{
	[PreserveSig]
	int GetName(
		[MarshalAs(UnmanagedType.LPWStr)] out string ppsz);

	[PreserveSig]
	int GetUIName(
		[MarshalAs(UnmanagedType.LPWStr)] out string ppsz);

	[PreserveSig]
	int GetIconLocation(
		[MarshalAs(UnmanagedType.LPWStr)] out string ppszPath,
		out int pIndex);

	[PreserveSig]
	int IsRecommended();

	[PreserveSig]
	int MakeDefault(
		[MarshalAs(UnmanagedType.LPWStr)] string pszDescription);

	[PreserveSig]
	int Invoke(
		IDataObject pdo);

	[PreserveSig]
	int CreateInvoker(
		IDataObject pdo,
		out IAssocHandlerInvoker ppInvoker);
}

[ComImport]
[Guid("973810ae-9599-4b88-9e4d-6ee98c9552da")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IEnumAssocHandlers
{
	[PreserveSig]
	int Next(
		uint celt,
		out IAssocHandler rgelt,
		out uint pceltFetched);
}