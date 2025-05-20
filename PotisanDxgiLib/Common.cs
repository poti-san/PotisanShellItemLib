using System.Drawing;

namespace Potisan.Windows.Dxgi;

//#define DXGI_CPU_ACCESS_FIELD        15

/// <summary>
/// RECT
/// </summary>
[DebuggerDisplay("({Left}, {Top}, {Right}, {Bottom})")]
public readonly struct NativeRectangle
{
	public readonly int Left;
	public readonly int Top;
	public readonly int Right;
	public readonly int Bottom;

	public readonly Point LeftTop => new(Left, Top);
	public readonly Point RightBottom => new(Right, Bottom);

	public readonly int Width => Right - Left;
	public readonly int Height => Bottom - Top;
	public readonly Size Size => new(Width, Height);
}

/// <summary>
/// DXGI_RATIONAL
/// </summary>
public readonly struct DxgiRational
{
	public readonly uint Numerator;
	public readonly uint Denominator;
}

public enum DxgiSampleQuality : uint
{
	StandardMultiSampleQualityPattern = 0xffffffff,
	CenterMultiSampleQualityPattern = 0xfffffffe,
}

/// <summary>
/// DXGI_SAMPLE_DESC
/// </summary>
public readonly struct DxgiSampleDesc
{
	public readonly uint Count;
	public readonly uint Quality;
}

/// <summary>
/// DXGI_COLOR_SPACE_TYPE
/// </summary>
public enum DxgiColorSpaceType : uint
{
	RgbFullG22NoneP709 = 0,
	RgbFullG10NoneP709 = 1,
	RgbStudioG22NoneP709 = 2,
	RgbStudioG22NoneP2020 = 3,
	Reserved = 4,
	YcbcrFullG22NoneP709X601 = 5,
	YcbcrStudioG22LeftP601 = 6,
	YcbcrFullG22LeftP601 = 7,
	YcbcrStudioG22LeftP709 = 8,
	YcbcrFullG22LeftP709 = 9,
	YcbcrStudioG22LeftP2020 = 10,
	YcbcrFullG22LeftP2020 = 11,
	RgbFullG2084NoneP2020 = 12,
	YcbcrStudioG2084LeftP2020 = 13,
	RgbStudioG2084NoneP2020 = 14,
	YcbcrStudioG22TopLeftP2020 = 15,
	YcbcrStudioG2084TopLeftP2020 = 16,
	RgbFullG22NoneP2020 = 17,
	YcbcrStudioGHLGTopLeftP2020 = 18,
	YcbcrFullGHLGTopLeftP2020 = 19,
	RgbStudioG24NoneP709 = 20,
	RgbStudioG24NoneP2020 = 21,
	YcbcrStudioG24LeftP709 = 22,
	YcbcrStudioG24LeftP2020 = 23,
	YcbcrStudioG24TopLeftP2020 = 24,
	Custom = 0xFFFFFFFF,
}

/// <summary>
/// DXGI_RGB
/// </summary>
public readonly struct DxgiRgb
{
	public readonly float Red;
	public readonly float Green;
	public readonly float Blue;
}

/// <summary>
/// D3DCOLORVALUE
/// </summary>
public readonly struct D2DColorValue
{
	public readonly float r;
	public readonly float g;
	public readonly float b;
	public readonly float a;
}

/// <summary>
/// DXGI_GAMMA_CONTROL
/// </summary>
public readonly struct DxgiGammaControl
{
	public readonly DxgiRgb Scale;
	public readonly DxgiRgb Offset;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
	public readonly DxgiRgb[] GammaCurve;
}

/// <summary>
/// DXGI_GAMMA_CONTROL_CAPABILITIES
/// </summary>
public readonly struct DxgiGammaControlCapabilities
{
	[MarshalAs(UnmanagedType.Bool)]
	public readonly bool ScaleAndOffsetSupported;
	public readonly float MaxConvertedValue;
	public readonly float MinConvertedValue;
	public readonly uint NumGammaControlPoints;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1025)]
	public readonly float[] ControlPointPositions;
}

/// <summary>
/// DXGI_MODE_SCANLINE_ORDER
/// </summary>
public enum DxgiModeScanlineOrder
{
	Unspecified = 0,
	Progressive = 1,
	UpperFieldFirst = 2,
	LowerFieldFirst = 3
}

/// <summary>
/// DXGI_MODE_SCALING
/// </summary>
public enum DxgiModeScaling
{
	Unspecified = 0,
	Centered = 1,
	Stretched = 2
}

/// <summary>
/// DXGI_MODE_ROTATION
/// </summary>
public enum DxgiModeRotation
{
	Unspecified = 0,
	Identity = 1,
	Rotate90 = 2,
	Rotate180 = 3,
	Rotate270 = 4,
}

/// <summary>
/// DXGI_MODE_DESC
/// </summary>
public readonly struct DxgiModeDesc
{
	public readonly uint Width;
	public readonly uint Height;
	public readonly DxgiRational RefreshRate;
	public readonly DxgiFormat Format;
	public readonly DxgiModeScanlineOrder ScanlineOrdering;
	public readonly DxgiModeScaling Scaling;
}

/// <summary>
/// DXGI_JPEG_DC_HUFFMAN_TABLE
/// </summary>
public readonly struct DxgiJpegDCHuffmanTable
{
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
	public readonly byte[] CodeCounts;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
	public readonly byte[] CodeValues;
}

/// <summary>
/// DXGI_JPEG_AC_HUFFMAN_TABLE
/// </summary>
public readonly struct DxgiJpegACHuffmanTable
{
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public readonly byte[] CodeCounts;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 162)]
	public readonly byte[] CodeValues;
}

/// <summary>
/// DXGI_JPEG_QUANTIZATION_TABLE
/// </summary>
public readonly struct DxgiJpegQuantizationTable
{
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
	public readonly byte[] Elements;
}

/// <summary>
/// DXGI_CPU_*
/// </summary>
public enum DxgiCpuAccess : uint
{
	None = 0,
	Dynamic = 1,
	ReadWrite = 2,
	Scratch = 3,
}

/// <summary>
/// DXGI_USAGE
/// </summary>
[Flags]
public enum DxgiUsage : uint
{
	ShaderInput = 0x00000010,
	RenderTargetOutput = 0x00000020,
	BackBuffer = 0x00000040,
	Shared = 0x00000080,
	ReadOnly = 0x00000100,
	DiscardOnPresent = 0x00000200,
	UnorderedAccess = 0x00000400,
}

/// <summary>
/// DXGI_FRAME_STATISTICS
/// </summary>
public readonly struct DxgiFrameStatistics
{
	public readonly uint PresentCount;
	public readonly uint PresentRefreshCount;
	public readonly uint SyncRefreshCount;
	public readonly long SyncQPCTime;
	public readonly long SyncGPUTime;
}

/// <summary>
/// DXGI_MAPPED_RECT
/// </summary>
public readonly struct DxgiMappedRect
{
	public readonly int Pitch;
	public readonly nint pBits0; // BYTE*
}

/// <summary>
/// LUID
/// </summary>
public record struct Luid(uint LowPart, int HighPart);

/// <summary>
/// DXGI_ADAPTER_DESC
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public readonly struct DxgiAdapterDesc
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
	public readonly string Description;
	public readonly uint VendorId;
	public readonly uint DeviceId;
	public readonly uint SubSysId;
	public readonly uint Revision;
	public readonly nuint DedicatedVideoMemory;
	public readonly nuint DedicatedSystemMemory;
	public readonly nuint SharedSystemMemory;
	public readonly Luid AdapterLuid;
}

/// <summary>
/// DXGI_OUTPUT_DESC
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public readonly struct DxgiOutputDesc
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	public readonly string DeviceName;
	public readonly NativeRectangle DesktopCoordinates;
	[MarshalAs(UnmanagedType.Bool)]
	public readonly bool AttachedToDesktop;
	public readonly DxgiModeRotation Rotation;
	public readonly nint Monitor;
}

/// <summary>
/// DXGI_SHARED_RESOURCE
/// </summary>
public struct DxgiSharedResource
{
	public nint Handle;
}

/// <summary>
/// DXGI_RESOURCE_PRIORITY_*
/// </summary>
public enum DxgiResourcePriority : uint
{
	Minimum = 0x28000000,
	Low = 0x50000000,
	Normal = 0x78000000,
	High = 0xa0000000,
	Maximum = 0xc8000000,
}

/// <summary>
/// DXGI_RESIDENCY
/// </summary>
public enum DxgiResidency : uint
{
	FullyResident = 1,
	ResidentInSharedMemory = 2,
	EvictedToDisk = 3
}

/// <summary>
/// DXGI_SURFACE_DESC
/// </summary>
public readonly struct DxgiSurfaceDesc
{
	public readonly uint Width;
	public readonly uint Height;
	public readonly DxgiFormat Format;
	public readonly DxgiSampleDesc SampleDesc;
}

/// <summary>
/// DXGI_SWAP_EFFECT
/// </summary>
public enum DxgiSwapEffect : uint
{
	Discard = 0,
	Sequential = 1,
	FlipSequential = 3,
	FlipDiscard = 4,
}

/// <summary>
/// DXGI_SWAP_CHAIN_FLAG
/// </summary>
[Flags]
public enum DxgiSwapChainFlag : uint
{
	NonPreRotated = 1,
	AllowModeSwitch = 2,
	GdiCompatible = 4,
	RestrictedContent = 8,
	RestrictSharedResourceDriver = 16,
	DisplayOnly = 32,
	FrameLatencyWaitableObject = 64,
	ForegroundLayer = 128,
	FullscreenVideo = 256,
	YuvVideo = 512,
	HWProtected = 1024,
	AllowTearing = 2048,
	RestrictedToAllHolographicDisplays = 4096,
}

/// <summary>
/// DXGI_SWAP_CHAIN_DESC
/// </summary>
public readonly struct DxgiSwapChainDescription
{
	public readonly DxgiModeDesc BufferDesc;
	public readonly DxgiSampleDesc SampleDesc;
	public readonly DxgiUsage BufferUsage;
	public readonly uint BufferCount;
	public readonly nint OutputWindow;
	[MarshalAs(UnmanagedType.Bool)]
	public readonly bool Windowed;
	public readonly DxgiSwapEffect SwapEffect;
	public readonly uint Flags;
}

/// <summary>
/// DXGI_ENUM_MODES_*
/// </summary>
public enum DxgiEnumMode : uint
{
	Interlaced = 1,
	Scaling = 2,
}
