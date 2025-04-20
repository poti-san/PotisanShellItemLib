using System.Drawing;

using Potisan.Windows.Shell.SafeHandles;

namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellItemImageFactory
{
	[PreserveSig]
	int GetImage(
		Size size,
		ShellItemImageFactoryGetBitmapFlag flags,
		out SafeGdiObjectHandle phbm);
}