using System.Runtime.InteropServices.ComTypes;

namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("55ce16ba-3014-41c1-9ceb-fade1446ac6c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfInsertAtSelection
{
	[PreserveSig]
	int InsertTextAtSelection(
		TFEditCookie ec,
		uint dwFlags,
		[MarshalAs(UnmanagedType.I2)] in char pchText,
		int cch,
		out ITfRange ppRange);

	[PreserveSig]
	int InsertEmbeddedAtSelection(
		TFEditCookie ec,
		uint dwFlags,
		IDataObject pDataObject,
		out ITfRange ppRange);
}
