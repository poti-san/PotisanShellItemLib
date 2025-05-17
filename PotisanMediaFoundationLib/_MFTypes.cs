#pragma warning disable IDE0044 // 読み取り専用修飾子を追加します
#pragma warning disable CA1051 // 参照可能なインスタンス フィールドを宣言しません

// ひとまずまとめておくファイル
// 各型は適宜移動します。

using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Potisan.Windows.MediaFoundation;

// BITMAPINFOHEADER
// BITMAPINFO

/// <summary>
/// MFT_REGISTER_TYPE_INFO
/// </summary>
public struct MFTRegisterTypeInfo
{
	public Guid MajorType;
	public Guid Subtype;
}

public struct MFAYUVSample
{
	public byte bCrValue;
	public byte bCbValue;
	public byte bYValue;
	public byte bSampleAlpha8;
}

public struct MFARGB
{
	public byte rgbBlue;
	public byte rgbGreen;
	public byte rgbRed;
	public byte rgbAlpha;
}

[StructLayout(LayoutKind.Explicit)]
public struct MFPaletteEntry
{
	[FieldOffset(0)]
	MFARGB ARGB;
	[FieldOffset(0)]
	MFAYUVSample AYCbCr;
}

public enum MFVideoInterlaceMode
{
	Unknown = 0,
	Progressive = 2,
	FieldInterleavedUpperFirst = 3,
	FieldInterleavedLowerFirst = 4,
	FieldSingleUpper = 5,
	FieldSingleLower = 6,
	MixedInterlaceOrProgressive = 7,

	FieldSingleUpperFirst = 5,
	FieldSingleLowerFirst = 6,
}

public enum MFVideoTransferFunction : uint
{
	Unknown = 0,
	Gamma10 = 1,
	Gamma18 = 2,
	Gamma20 = 3,
	Gamma22 = 4,
	ItuRBT709 = 5,
	Epmte240M = 6,
	Srgb = 7,
	Gamma28 = 8,
	Log100 = 9,
	Log316 = 10,
	Spmte709Sym = 11,
	ItuRBT2020Const = 12,
	ItuRBT2020 = 13,
	True26 = 14,
	Smpte2084 = 15,
	HLG = 16,
	NoGamma10Rel = 17,
	BT1361Ecg = 18,
	Smpte428 = 19,
}

/// <summary>
/// MFVideoPrimaries
/// </summary>
public enum MFVideoPrimary : uint
{
	Unknown = 0,
	Reserved = 1,
	BT709 = 2,
	BT4702SysM = 3,
	BT4702SysBG = 4,
	Smpte170M = 5,
	Smpte240M = 6,
	Ebu3213 = 7,
	SmpteC = 8,
	BT2020 = 9,
	Xyz = 10,
	DciP3 = 11,
	Aces = 12,
	DisplayP3 = 13,
}

public enum MFVideoLighting : uint
{
	Unknown = 0,
	Bright = 1,
	Office = 2,
	Dim = 3,
	Dark = 4,
}

public enum MFVideoTransferMatrix : uint
{
	Unknown = 0,
	BT709 = 1,
	BT601 = 2,
	Smpte240M = 3,
	BT202010 = 4,
	BT202012 = 5,
	Identity = 6,
	Fcc47 = 7,
	YCgCo = 8,
	Smpte2085 = 9,
	Chroma = 10,
	ChromaConst = 11,
	ICtCP = 12,
}

[Flags]
public enum MFVideoChromaSubsampling : uint
{
	Unknown = 0,
	ProgressiveChroma = 0x8,
	HorizontallyCosited = 0x4,
	VerticallyCosited = 0x2,
	VerticallyAlignedChromaPlanes = 0x1,
	Mpeg2 = HorizontallyCosited | VerticallyAlignedChromaPlanes,
	Mpeg1 = VerticallyAlignedChromaPlanes,
	DVPal = HorizontallyCosited | VerticallyCosited,
	Cosited = HorizontallyCosited | VerticallyCosited | VerticallyAlignedChromaPlanes,
}

public enum MFNominalRange : uint
{
	Unknown = 0,
	Normal = 1,
	Wide = 2,
	Range0to255 = 1,
	Range16to235 = 2,
	Range48to208 = 3,
	Range64to127 = 4,
}

/// <summary>
/// MFVideoFlags
/// </summary>
[Flags]
public enum MFVideoFlag : uint
{
	MFVideoFlagPadToMask = (0x1 | 0x2),
	PadToNone = 0 * 0x1,
	PadTo4x3 = 1 * 0x1,
	PadTo16x9 = 2 * 0x1,
	SrcContentHintMask = 0x4 | 0x8 | 0x10,
	SrcContentHintNone = 0 * 0x4,
	SrcContentHint16x9 = 1 * 0x4,
	SrcContentHint2351 = 2 * 0x4,
	AnalogProtected = 0x20,
	DigitallyProtected = 0x40,
	ProgressiveContent = 0x80,
	FieldRepeatCountMask = 0x100 | 0x200 | 0x400,
	FieldRepeatCountShift = 8,
	ProgressiveSeqReset = 0x800,
	PanScanEnabled = 0x20000,
	LowerFieldFirst = 0x40000,
	BottomUpLinearRep = 0x80000,
	DXVASurface = 0x100000,
	RenderTargetSurface = 0x400000,
}

public struct MFRatio
{
	public uint Numerator;
	public uint Denominator;
}

public struct MFOffset
{
	public ushort Fract;
	public short Value;
}

public struct MFVideoArea
{
	public MFOffset OffsetX;
	public MFOffset OffsetY;
	public Size Area;
}

[StructLayout(LayoutKind.Sequential)]
public class MFVideoInfo
{
	public uint dwWidth;
	public uint dwHeight;
	public MFRatio PixelAspectRatio;
	public MFVideoChromaSubsampling SourceChromaSubsampling;
	public MFVideoInterlaceMode InterlaceMode;
	public MFVideoTransferFunction TransferFunction;
	public MFVideoPrimary ColorPrimaries;
	public MFVideoTransferMatrix TransferMatrix;
	public MFVideoLighting SourceLighting;
	public MFRatio FramesPerSecond;
	public MFNominalRange NominalRange;
	public MFVideoArea GeometricAperture;
	public MFVideoArea MinimumDisplayAperture;
	public MFVideoArea PanScanAperture;
	public ulong VideoFlags;
}

[StructLayout(LayoutKind.Sequential)]
public class MFVideoCompressedInfo
{
	public long AvgBitRate;
	public long AvgBitErrorRate;
	public uint MaxKeyFrameSpacing;
}

public struct MFVideoSurfaceInfo
{
	public uint Format;
	public uint PaletteEntries;
	public MFPaletteEntry Palette0; // [1]
}

/// <summary>
/// MFVIDEOFORMAT
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class MFVideoFormat
{
	private readonly uint dwSize;
	public required MFVideoInfo videoInfo;
	public Guid guidFormat;
	public required MFVideoCompressedInfo compressedInfo;
	public MFVideoSurfaceInfo surfaceInfo;

	public MFVideoFormat()
	{
		dwSize = (uint)Marshal.SizeOf<MFVideoFormat>();
	}
}

public enum MFStandardVideoFormat
{
	Reserved = 0,
	Ntsc,
	Pal,
	DvdNtsc,
	DvdPal,
	DVPal,
	DVNts,
	AtscSD480i,
	AtscHD1080i,
	AtscHD720p,
}

/// <summary>
/// MF_STREAM_STATE
/// </summary>
public enum MFStreamState
{
	Stopped = 0,
	Paused,
	Running,
}

/// <summary>
/// AUDIO_STREAM_CATEGORY
/// </summary>
public enum MFAudioStreamCategory : uint
{
	Other = 0,
	ForegroundOnlyMedia,
	BackgroundCapableMedia,
	Communications,
	Alerts,
	SoundEffects,
	GameEffects,
	GameMedia,
	GameChat,
	Speech,
	Movie,
	Media,
	FarFieldSpeech,
	UniformSpeech,
	VoiceTyping,
}