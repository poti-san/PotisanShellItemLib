#pragma warning disable CA1707

namespace Potisan.Windows.Shell.Window.ComTypes;

public struct POINT
{
	public int x;
	public int y;
}

public struct RECT
{
	public int left;
	public int top;
	public int right;
	public int bottom;
}

/// <summary>
/// <c>MSG</c>
/// </summary>
public struct Msg
{
	public nint WindowHandle;
	public uint Message;
	public nint WParam;
	public nint LParam;
	public uint Time;
	public POINT Point;
}

public struct FOLDERSETTINGS
{
	public uint ViewMode;
	public uint fFlags;
}

public enum SVGIO
{
	BACKGROUND = 0,
	SELECTION = 0x1,
	ALLVIEW = 0x2,
	CHECKED = 0x3,
	// TYPE_MASK = 0xf,
	FLAG_VIEWORDER = unchecked((int)0x80000000),
}