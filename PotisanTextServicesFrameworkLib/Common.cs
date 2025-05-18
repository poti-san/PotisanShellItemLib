using Potisan.Windows.Text.Tsf.ComTypes;

namespace Potisan.Windows.Text.Tsf;

public enum TFAnchor
{
	Start = 0,
	End = 1,
}

/// <summary>
/// TfClientId
/// </summary>
public record struct TFClientID(uint Value);

public record struct TFEditCookie(uint Value)
{
	public static TFEditCookie Invalid => new(0);
}

public record struct TFGuidAtom(uint Value);

/// <summary>
/// 
/// </summary>
/// <remarks>
/// <c>TF_PERSISTENT_PROPERTY_HEADER_ACP</c>。
/// </remarks>
public readonly struct TFPersistentPropertyHeaderAcp
{
	public readonly Guid guidType;
	public readonly int ichStart;
	public readonly int cch;
	public readonly uint cb;
	public readonly uint dwPrivate;
	public readonly Guid clsidTIP;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TF_LANGUAGEPROFILE</c></remarks>
public readonly struct TFLanguageProfile
{
	public readonly Guid clsid;
	public readonly uint langid;
	public readonly Guid catid;
	[MarshalAs(UnmanagedType.Bool)]
	public readonly bool fActive;
	public readonly Guid guidProfile;
}

public enum TFActiveSelEnd
{
	None = 0,
	Start = 1,
	End = 2,
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TF_SELECTIONSTYLE</c></remarks>
public struct TFSelectionStyle
{
	public TFActiveSelEnd ase;
	[MarshalAs(UnmanagedType.Bool)]
	public bool fInterimChar;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TF_SELECTION</c></remarks>
public struct TFSelection
{
	public ITfRange range;
	public TFSelectionStyle style;
}

/// <summary>
/// <c>POINT</c>構造体。
/// </summary>
public record struct NativePoint(int X, int Y);

/// <summary>
/// <c>RECT</c>構造体。
/// </summary>
public record struct NativeRectangle(int Left, int Top, int Right, int Bottom)
{
	public readonly int Width => Right - Left;
	public readonly int Height => Bottom - Top;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TF_PROPERTYVAL</c></remarks>
public struct TFPropertyValue
{
	public Guid guidId;
	[MarshalAs(UnmanagedType.Struct)]
	public object varValue;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TF_STATUS</c></remarks>
public struct TF_STATUS
{
	public uint dwDynamicFlags;
	public uint dwStaticFlags;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TF_HALTCOND</c></remarks>
public readonly struct TF_HALTCOND
{
	public readonly ITfRange pHaltRange;
	public readonly TFAnchor aHaltPos;
	public readonly uint dwFlags;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TFGravity</c></remarks>
public enum TFGravity
{
	Backward = 0,
	Forward = 1,
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>TfShiftDir</c></remarks>
public enum TFShiftDirection
{
	Backward = 0,
	Forward = 1,
}
