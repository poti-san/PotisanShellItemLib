using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// 実行中オブジェクトテーブル(ROT)。IRunningObjectTable COMインターフェイスのラッパーです。
/// </summary>
public class RunningObjectTable(object? o) : ComUnknownWrapperBase<IRunningObjectTable>(o)
{
	/*
	[PreserveSig]
	int Register(
		uint grfFlags,
		[MarshalAs(UnmanagedType.IUnknown)] object? punkObject,
		IMoniker pmkObjectName,
		out uint pdwRegister);

	[PreserveSig]
	int Revoke(
		uint dwRegister);

	[PreserveSig]
	int IsRunning(
		IMoniker pmkObjectName);

	[PreserveSig]
	int GetObject(
		IMoniker pmkObjectName,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppunkObject);

	[PreserveSig]
	int NoteChangeTime(
		uint dwRegister,
		FILETIME pfiletime);

	[PreserveSig]
	int GetTimeOfLastChange(
		IMoniker pmkObjectName,
		out FILETIME pfiletime);
	*/

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
