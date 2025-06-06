using System.Drawing;

using Potisan.Windows.Shell.ComTypes;
using Potisan.Windows.Shell.SafeHandles;

namespace Potisan.Windows.Shell;

/// <summary>
/// シェルアイコンのファクトリ。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// WinFormsプロジェクトでデスクトップのアイコンをフォームに表示します。
/// <code>
/// <![CDATA[using Potisan.Windows.Shell;
///
/// namespace WinFormsApp1;
///
/// internal static class Program
/// {
/// 	[STAThread]
/// 	internal static void Main()
/// 	{
/// 		ApplicationConfiguration.Initialize();
///
/// 		var form = new Form();
///
/// 		var item = ShellItem2.CreateKnownFolderItem(KnownFolderID.Desktop);
/// 		using var bmp = item.AsImageFactory.GetImage(new(64, 64));
/// 		form.Controls.Add(new PictureBox { Dock = DockStyle.Fill, Image = Bitmap.FromHbitmap(bmp.DangerousGetHandle()) });
///
/// 		Application.Run(form);
/// 	}
/// }]]>
/// </code>
/// </example>
/// <remarks>
/// <para><see cref="ShellItem.ImageFactory"/>で作成できます。</para>
/// <para><c>IShellItemImageFactory</c> COMインターフェイスのラッパーです。</para>
/// </remarks>
public class ShellItemImageFactory(object? o) : ComUnknownWrapperBase<IShellItemImageFactory>(o)
{
	public ComResult<SafeGdiObjectHandle> GetImageNoThrow(Size size, ShellItemImageFactoryGetBitmapFlag flags = 0)
		=> new(_obj.GetImage(size, flags, out var x), x);

	public SafeGdiObjectHandle GetImage(Size size, ShellItemImageFactoryGetBitmapFlag flags = 0)
		=> GetImageNoThrow(size, flags).Value;
}

/// <summary>
/// SIIGBF
/// </summary>
public enum ShellItemImageFactoryGetBitmapFlag : uint
{
	ResizeToFit = 0,
	BiggerSizeOK = 0x1,
	MemoryOnly = 0x2,
	IconOnly = 0x4,
	ThumbnailOnly = 0x8,
	InCacheOnly = 0x10,
	CropToSquare = 0x20,
	WideThumbnails = 0x40,
	IconBackground = 0x80,
	Scaleup = 0x100
}