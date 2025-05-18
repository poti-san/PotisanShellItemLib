using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("2433bf8e-0f9b-435c-ba2c-180611978c30")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfContextView
{
	[PreserveSig]
	int GetRangeFromPoint(
		TFEditCookie ec,
		in NativePoint ppt,
		uint dwFlags,
		out ITfRange ppRange);

	[PreserveSig]
	int GetTextExt(
		TFEditCookie ec,
		ITfRange? pRange,
		out NativeRectangle prc,
		[MarshalAs(UnmanagedType.Bool)] out bool pfClipped);

	[PreserveSig]
	int GetScreenExt(
		out NativeRectangle prc);

	[PreserveSig]
	int GetWnd(
		out nint phwnd);
}