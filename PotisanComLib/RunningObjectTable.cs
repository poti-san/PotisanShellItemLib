using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// 実行中オブジェクトテーブル(ROT)。IRunningObjectTable COMインターフェイスのラッパーです。
/// </summary>
public class RunningObjectTable(object? o) : ComUnknownWrapperBase<IRunningObjectTable>(o)
{
	public ComResult<uint> RegisterNoThrow(RunningObjectTableFlag flags, object rcwObject, Moniker objectName)
		=> new(_obj.Register((uint)flags, rcwObject, (IMoniker)objectName.WrappedObject!, out var x), x);

	public uint Register(RunningObjectTableFlag flags, object rcwObject, Moniker objectName)
		=> RegisterNoThrow(flags, rcwObject, objectName).Value;

	public ComResult<uint> RegisterNoThrow(RunningObjectTableFlag flags, IComUnknownWrapper rcwObject, Moniker objectName)
		=> RegisterNoThrow(flags, rcwObject.WrappedObject!, objectName);

	public uint Register(RunningObjectTableFlag flags, IComUnknownWrapper rcwObject, Moniker objectName)
		=> RegisterNoThrow(flags, rcwObject, objectName).Value;

	public ComResult RevokeNoThrow(uint register)
		=> new(_obj.Revoke(register));

	public void Revoke(uint register)
		=> RevokeNoThrow(register);

	public ComResult IsRunningNoThrow(Moniker moniker)
		=> new(_obj.IsRunning((IMoniker)moniker.WrappedObject!));

	public void IsRunning(Moniker moniker)
		=> IsRunningNoThrow(moniker).ThrowIfError();

	public ComResult<object> GetObjectNoThrow(Moniker objectName)
		=> new(_obj.GetObject((IMoniker)objectName.WrappedObject!, out var x), x!);

	public object GetObject(Moniker objectName)
		=> GetObjectNoThrow(objectName).Value;

	public ComResult<TWrapper> GetObjectNoThrow<TWrapper>(Moniker objectName)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(GetObjectNoThrow(objectName));

	public TWrapper GetObject<TWrapper>(Moniker objectName)
		where TWrapper : IComUnknownWrapper
		=> GetObjectNoThrow<TWrapper>(objectName).Value;

	public ComResult NoteChangeTimeNoThrow(uint register, DateTime time)
		=> new(_obj.NoteChangeTime(register, new FileTime(time, false)));

	public void NoteChangeTime(uint register, DateTime time)
		=> NoteChangeTimeNoThrow(register, time).ThrowIfError();

	public ComResult<DateTime> GetTimeOfLastChangeNoThrow(Moniker objectName)
		=> new(_obj.GetTimeOfLastChange((IMoniker)objectName.WrappedObject!, out var x), x.ToDateTime());

	public DateTime GetTimeOfLastChange(Moniker objectName)
		=> GetTimeOfLastChangeNoThrow(objectName).Value;

	public ComResult<MonikerEnumerable> RunningEnumeableNoThrow
		=> new(_obj.EnumRunning(out var x), new(x));

	public MonikerEnumerable RunningEnumerable
		=> RunningEnumeableNoThrow.Value;

	public ImmutableArray<Moniker> Runnings
		=> [.. RunningEnumerable];

	public static ComResult<RunningObjectTable> CreateLocalNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int GetRunningObjectTable(uint reserved, out IRunningObjectTable pprot);

		return new(GetRunningObjectTable(0, out var x), new(x));
	}

	public static RunningObjectTable CreateLocal()
		=> CreateLocalNoThrow().Value;
}

[Flags]
public enum RunningObjectTableFlag : uint
{
	REGISTRATIONKEEPSALIVE = 0x1,
	ALLOWANYCLIENT = 0x2,
}
