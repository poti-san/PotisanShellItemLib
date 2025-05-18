using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaImageInformation(object? o) : ComUnknownWrapperBase<IImageInformation>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> AltTextNoThrow
		=> new(_obj.get_AltText(out var x), x!);

	public string AltText
		=> AltTextNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> HeightNoThrow
		=> new(_obj.get_Height(out var x), x!);

	public int Height
		=> HeightNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> SourceNoThrow
		=> new(_obj.get_Source(out var x), x!);

	public string Source
		=> SourceNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> WidthNoThrow
		=> new(_obj.get_Width(out var x), x!);

	public int Width
		=> WidthNoThrow.Value;
}
