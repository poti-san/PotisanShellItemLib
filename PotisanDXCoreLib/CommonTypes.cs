namespace Potisan.Windows.DXCore;

public enum DXCoreAdapterProperty : uint
{
	InstanceLuid = 0,
	DriverVersion = 1,
	DriverDescription = 2,
	/// <summary>
	/// 可能ならHardwareIDPartsを優先してください。
	/// </summary>
	HardwareID = 3,
	KmdModelVersion = 4,
	ComputePreemptionGranularity = 5,
	GraphicsPreemptionGranularity = 6,
	DedicatedAdapterMemory = 7,
	DedicatedSystemMemory = 8,
	SharedSystemMemory = 9,
	AcgCompatible = 10,
	IsHardware = 11,
	IsIntegrated = 12,
	IsDetachable = 13,
	HardwareIDParts = 14,
	PhysicalAdapterCount = 15,
	AdapterEngineCount = 16,
	AdapterEngineName = 17,
}

public enum DXCoreAdapterState : uint
{
	IsDriverUpdateInProgress = 0,
	AdapterMemoryBudget = 1,
	AdapterMemoryUsageBytes = 2,
	AdapterMemoryUsageByProcessBytes = 3,
	AdapterEngineRunningTimeMicroseconds = 4,
	AdapterEngineRunningTimeByProcessMicroseconds = 5,
	AdapterTemperatureCelsius = 6,
	AdapterInUseProcessCount = 7,
	AdapterInUseProcessSet = 8,
	AdapterEngineFrequencyHertz = 9,
	AdapterMemoryFrequencyHertz = 10
}

public enum DXCoreSegmentGroup : uint
{
	Local = 0,
	NonLocal = 1
}

public enum DXCoreNotificationType : uint
{
	AdapterListStale = 0,
	AdapterNoLongerValid = 1,
	AdapterBudgetChange = 2,
	AdapterHardwareContentProtectionTeardown = 3
}

public enum DXCoreAdapterPreference : uint
{
	Hardware = 0,
	MinimumPower = 1,
	HighPerformance = 2
}

public enum DXCoreWorkload : uint
{
	Graphics = 0,
	Compute = 1,
	Media = 2,
	MachineLearning = 3,
}

[Flags]
public enum DXCoreRuntimeFilterFlags : uint
{
	None = 0x0,
	D3D11 = 0x1,
	D3D12 = 0x2
}

[Flags]
public enum DXCoreHardwareTypeFilterFlags : uint
{
	None = 0x0,
	GPU = 0x1,
	ComputeAccelerator = 0x2,
	NPU = 0x4,
	MediaAccelerator = 0x8
}

public struct DXCoreHardwareID
{
	public uint vendorID;
	public uint deviceID;
	public uint subSysID;
	public uint revision;
}

public struct DXCoreHardwareIDParts
{
	public uint vendorID;
	public uint deviceID;
	public uint subSystemID;
	public uint subVendorID;
	public uint revisionID;
}

public struct DXCoreAdapterMemoryBudgetNodeSegmentGroup
{
	public uint nodeIndex;
	public DXCoreSegmentGroup segmentGroup;
}

public struct DXCoreAdapterMemoryBudget
{
	public ulong budget;
	public ulong currentUsage;
	public ulong availableForReservation;
	public ulong currentReservation;
}

public struct DXCoreAdapterEngineIndex
{
	public uint physicalAdapterIndex;
	public uint engineIndex;
}

public struct DXCoreEngineQueryInput
{
	public DXCoreAdapterEngineIndex adapterEngineIndex;
	public uint processId;
}

public struct DXCoreEngineQueryOutput
{
	public ulong runningTime;
	public bool processQuerySucceeded;
}

public enum DXCoreMemoryType : uint
{
	Dedicated = 0,
	Shared = 1
}

public struct DXCoreMemoryUsage
{
	public ulong committed;
	public ulong resident;
}

public struct DXCoreMemoryQueryInput
{
	public uint physicalAdapterIndex;
	public DXCoreMemoryType memoryType;
}

public struct DXCoreProcessMemoryQueryInput
{
	public uint physicalAdapterIndex;
	public DXCoreMemoryType memoryType;
	public uint processId;
}

public struct DXCoreProcessMemoryQueryOutput
{
	public DXCoreMemoryUsage memoryUsage;
	public bool processQuerySucceeded;
}

public struct DXCoreAdapterProcessSetQueryInput0
{
	public uint arraySize;
	public uint processIds0;
}

public struct DXCoreAdapterProcessSetQueryOutput
{
	public uint processesWritten;
	public uint processesTotal;
}

public struct DXCoreEngineNamePropertyInput0
{
	public DXCoreAdapterEngineIndex adapterEngineIndex;
	public uint engineNameLength;
	public char engineName0;
}

public struct DXCoreEngineNamePropertyOutput
{
	public uint engineNameLength;
}

public struct DXCoreFrequencyQueryOutput
{
	public ulong frequency;
	public ulong maxFrequency;
	public ulong maxOverclockedFrequency;
}

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate int DXCoreNotificationCallback(DXCoreNotificationType notificationType, [MarshalAs(UnmanagedType.IUnknown)] object? obj, nint context);

/// <summary>
/// LUID
/// </summary>
public record struct Luid(uint LowPart, int HighPart);
