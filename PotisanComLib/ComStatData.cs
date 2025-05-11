using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// ADVF
/// </summary>
public enum ADVF : uint
{
	NoData = 1,
	PrimeFirst = 2,
	OnlyOnce = 4,
	DataOnStop = 64,
	NoHandler = 8,
	ForceBuildin = 16,
	OnSave = 32
}

/// <summary>
/// COMデータ統計。STATDATA構造体。
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class ComStatData
{
	public ComFormatEtc? FormatEtc;
	public uint Advance;
	[MarshalAs(UnmanagedType.IUnknown)]
	public IAdviseSink? AdvanceSink;
	public uint Connection;
}
