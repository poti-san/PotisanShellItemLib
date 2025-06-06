using Potisan.Windows.Com.SafeHandles;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// IDリストによる永続化サポート。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code>
/// <![CDATA[using Potisan.Windows.Shell;
///
/// var documentsFolder = ShellItem.CreateKnownFolderItem(KnownFolderID.Documents);
///
/// var persistIDList = documentsFolder.AsPersistIDList;]]>
/// </code>
/// </example>
/// <remarks>
/// <c>IPersistIDList</c> COMインターフェイスのラッパーです。
/// </remarks>
public class PersistIDList(object? o) : ComUnknownWrapperBase<IPersistIDList>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> ClassIDNoThrow
		=> new(_obj.GetClassID(out var x), x);

	public Guid ClassID
		=> ClassIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> DangerousIDListNoThrow
		=> new(_obj.GetIDList(out var x), x);

	public ComResult SetDangerousIDListNoThrow(nint value)
		=> new(_obj.SetIDList(value));

	public nint DangerousIDList
	{
		get => DangerousIDListNoThrow.Value;
		set => SetDangerousIDListNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<SafeCoTaskMemHandle> IDListNoThrow
		=> new(_obj.GetIDList(out var x), new(x, true));

	public ComResult SetIDListNoThrow(SafeCoTaskMemHandle value)
		=> new(_obj.SetIDList(value.DangerousGetHandle()));

	public SafeCoTaskMemHandle IDList
	{
		get => IDListNoThrow.Value;
		set => SetIDListNoThrow(value).ThrowIfError();
	}
}
