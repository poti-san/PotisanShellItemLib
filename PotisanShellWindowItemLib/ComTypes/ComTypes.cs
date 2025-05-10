namespace Potisan.Windows.Shell.ComTypes;

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

public struct MSG
{
	public nint hwnd;
	public uint message;
	public nint wParam;
	public nint lParam;
	public uint time;
	public POINT pt;
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
	TYPE_MASK = 0xf,
	FLAG_VIEWORDER = unchecked((int)0x80000000),
}