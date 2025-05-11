#pragma warning disable CA1051 // 参照可能なインスタンス フィールドを宣言しません

using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFClock(object? o) : ComUnknownWrapperBase<IMFClock>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFClockCharacteristicsFlag> CharacteristicsNoThrow
		=> new(_obj.GetClockCharacteristics(out var x), (MFClockCharacteristicsFlag)x);

	public MFClockCharacteristicsFlag Characteristics
		=> CharacteristicsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(long ClockTime, long SystemTime100NS)> CorrelatedTimeNoThrow
		=> new(_obj.GetCorrelatedTime(0, out var x1, out var x2), (x1, x2));

	public (long ClockTime, long SystemTime100NS) CorrelatedTime
		=> CorrelatedTimeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> ContinuityKeyNoThrow
		=> new(_obj.GetContinuityKey(out var x), x);

	public uint ContinuityKey
		=> ContinuityKeyNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFClockState> StateNoThrow
		=> new(_obj.GetState(0, out var x), x);

	public MFClockState State
		=> StateNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFClockProperties> PropertiesNoThrow
		=> new(_obj.GetProperties(out var x), x);

	public MFClockProperties Properties
		=> PropertiesNoThrow.Value;
}

public enum MFClockCharacteristicsFlag : uint
{
	Frequency10MHZ = 0x2,
	AlwayaRunning = 0x4,
	IsSystemClock = 0x8,
}

public enum MFClockState : uint
{
	Invalid = 0,
	Running,
	Stopped,
	Paused,
}

/// <summary>
/// MFCLOCK_RELATIONAL_FLAGS
/// </summary>
public enum MFClockRelationalFlag : uint
{
	JutterNeverAhead = 0x1,
}

/// <summary>
/// MFCLOCK_PROPERTIES
/// </summary>
public struct MFClockProperties
{
	public const ulong ClockFrequency100NS = 10000000;
	public const uint ClockToleranceUnknown = 50000;
	public const uint ClockJitterIsr = 1000;
	public const uint ClockJitterDpc = 4000;
	public const uint ClockJitterPassive = 10000;

	public long CorrelationRate;
	public Guid ClockID;
	public uint ClockFlags;
	public ulong ClockFrequency;
	public uint ClockTolerance;
	public uint ClockJitter;
}
