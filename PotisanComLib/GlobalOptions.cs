using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMグローバルオプション設定。
/// </summary>
/// <param name="o">RCWインスタンス</param>
/// <remarks>
/// <c>IGlobalOptions</c> COMインターフェイスのラッパーです。
/// </remarks>
public class GlobalOptions(object? o) : ComUnknownWrapperBase<IGlobalOptions>(o)
{
	public static ComResult<GlobalOptions> CreateNoThrow()
	{
		Guid CLSID_GlobalOptions = new("0000034B-0000-0000-C000-000000000046");
		return ComHelper.CreateInstanceNoThrow<GlobalOptions, IGlobalOptions>(CLSID_GlobalOptions);
	}

	public static GlobalOptions Create()
		=> CreateNoThrow().Value;

	public ComResult SetNoThrow(GlobalOptionProperties property, nuint value)
		=> new(_obj.Set(property, value));

	public void Set(GlobalOptionProperties property, nuint value)
		=> SetNoThrow(property, value).ThrowIfError();

	public ComResult<nuint> QueryNoThrow(GlobalOptionProperties property)
		=> new(_obj.Query(property, out var x), x);

	public nuint Query(GlobalOptionProperties property)
		=> QueryNoThrow(property).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<GlobalOptionExceptionHandleValue> ExceptionHandlingNoThrow
		=> QueryNoThrow(GlobalOptionProperties.ExceptionHandling) switch
		{ { HResult: var hr, ValueUnchecked: var x } => new(hr, (GlobalOptionExceptionHandleValue)x) };

	public ComResult SetExceptionHandlingNoThrow(GlobalOptionExceptionHandleValue value)
		=> SetNoThrow(GlobalOptionProperties.ExceptionHandling, (nuint)value);

	public GlobalOptionExceptionHandleValue ExceptionHandling
	{
		get => ExceptionHandlingNoThrow.Value;
		set => SetExceptionHandlingNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid?> AppIDNoThrow
		=> QueryNoThrow(GlobalOptionProperties.AppID) switch
		{ { HResult: var hr, ValueUnchecked: var x } => new(hr, x != 0 ? Marshal.PtrToStructure<Guid>((nint)x) : null) };

	public Guid? AppID
		=> AppIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<GlobalOptionRpcThreadPoolSetting> RpcThreadPoolSettingNoThrow
		=> QueryNoThrow(GlobalOptionProperties.RpcThreadPoolSetting) switch
		{ { HResult: var hr, ValueUnchecked: var x } => new(hr, (GlobalOptionRpcThreadPoolSetting)x) };

	public ComResult SetRpcThreadPoolSettingNoThrow(GlobalOptionRpcThreadPoolSetting value)
		=> SetNoThrow(GlobalOptionProperties.RpcThreadPoolSetting, (nuint)value);

	public GlobalOptionRpcThreadPoolSetting RpcThreadPoolSetting
	{
		get => RpcThreadPoolSettingNoThrow.Value;
		set => SetRpcThreadPoolSettingNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<GlobalOptionROSetting> ROSettingsNoThrow
		=> QueryNoThrow(GlobalOptionProperties.ROSettings) switch
		{ { HResult: var hr, ValueUnchecked: var x } => new(hr, (GlobalOptionROSetting)x) };

	public ComResult SetROSettingsNoThrow(GlobalOptionROSetting value)
		=> SetNoThrow(GlobalOptionProperties.ROSettings, (nuint)value);

	public GlobalOptionROSetting ROSettings
	{
		get => ROSettingsNoThrow.Value;
		set => SetROSettingsNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<GlobalOptionUnmarshalingPolicy> UnmarshalingPolicyNoThrow
		=> QueryNoThrow(GlobalOptionProperties.UnmarshalingPolicy) switch
		{ { HResult: var hr, ValueUnchecked: var x } => new(hr, (GlobalOptionUnmarshalingPolicy)x) };

	public ComResult SetUnmarshalingPolicyNoThrow(GlobalOptionUnmarshalingPolicy value)
		=> SetNoThrow(GlobalOptionProperties.UnmarshalingPolicy, (nuint)value);

	public GlobalOptionUnmarshalingPolicy UnmarshalingPolicy
	{
		get => UnmarshalingPolicyNoThrow.Value;
		set => SetUnmarshalingPolicyNoThrow(value).ThrowIfError();
	}
}

/// <summary>
/// GLOBALOPT_PROPERTIES
/// </summary>
public enum GlobalOptionProperties : uint
{
	ExceptionHandling = 1,
	AppID = 2,
	RpcThreadPoolSetting = 3,
	ROSettings = 4,
	UnmarshalingPolicy = 5,
	Reserved1 = 6,
	Reserved2 = 7,
	Reserved3 = 8
}

/// <summary>
/// GLOBALOPT_EH_VALUES
/// </summary>
public enum GlobalOptionExceptionHandleValue : uint
{
	Handle = 0,
	DonotHandleFatal = 1,
	DonotHandle = DonotHandleFatal,
	DonotHandleAny = 2
}

/// <summary>
/// GLOBALOPT_RPCTP_VALUES
/// </summary>
public enum GlobalOptionRpcThreadPoolSetting : uint
{
	DefaultPool = 0,
	PrivatePool = 1
}

/// <summary>
/// GLOBALOPT_RO_FLAGS
/// </summary>
[Flags]
public enum GlobalOptionROSetting : uint
{
	StaModelLoopRemoveTouchMessages = 0x1,
	StaModelLoopSharedQueueRemoveInputMessages = 0x2,
	StaModelLoopSharedQueueDonotRemoveInputMessages = 0x4,
	FastRunDown = 0x8,
	Reserved1 = 0x10,
	Reserved2 = 0x20,
	Reserved3 = 0x40,
	StaModelLoopSharedQueueReorderPointerMessages = 0x80,
	Reserved4 = 0x100,
	Reserved5 = 0x200,
	Reserved6 = 0x400
}

/// <summary>
/// GLOBALOPT_UNMARSHALING_POLICY_VALUES
/// </summary>
public enum GlobalOptionUnmarshalingPolicy : uint
{
	Normal = 0,
	Strong = 1,
	Hybrid = 2
}
