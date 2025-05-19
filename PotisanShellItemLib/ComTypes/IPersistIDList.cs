using Potisan.Windows.Com.SafeHandles;

namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("1079acfc-29bd-11d3-8e0d-00c04f6837d5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPersistIDList // IPersist
{
	#region IPersist

	[PreserveSig]
	int GetClassID(out Guid pClassID);

	#endregion IPersist

	[PreserveSig]
	int SetIDList(
		nint pidl);

	[PreserveSig]
	int GetIDList(
		out nint ppidl);
}