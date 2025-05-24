using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// WUA更新履歴項目。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.Diagnostics.Wua;
///
///var updateSession = WuaUpdateSession.Create();
///var updateSearcher = updateSession.CreateUpdateSearcher();
///
///foreach (var updateHistory in updateSearcher.AllHistoryCollection)
///{
///	Console.WriteLine(updateHistory.Title);
///}
///]]>
/// </code>
/// </example>
public sealed class WuaUpdateHistoryEntry(object? o) : ComUnknownWrapperBase<IUpdateHistoryEntry>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateOperation> OperationNoThrow
		=> new(_obj.get_Operation(out var x), x);

	public WuaUpdateOperation Operation
		=> OperationNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaOperationResultCode> ResultCodeNoThrow
		=> new(_obj.get_ResultCode(out var x), x);

	public WuaOperationResultCode ResultCode
		=> ResultCodeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> HResultNoThrow
		=> new(_obj.get_HResult(out var x), x);

	public int HResult
		=> HResultNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DateTime> DateNoThrow
		=> new(_obj.get_Date(out var x), x.ToDateTime());

	public DateTime Date
		=> DateNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateIdentity> UpdateIdentityNoThrow
		=> new(_obj.get_UpdateIdentity(out var x), new(x));

	public WuaUpdateIdentity UpdateIdentity
		=> UpdateIdentityNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> TitleNoThrow
		=> new(_obj.get_Title(out var x), x!);

	public string Title
		=> TitleNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DescriptionNoThrow
		=> new(_obj.get_Description(out var x), x!);

	public string Description
		=> DescriptionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> UnmappedResultCodeNoThrow
		=> new(_obj.get_UnmappedResultCode(out var x), x);

	public int UnmappedResultCode
		=> UnmappedResultCodeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ClientApplicationIDNoThrow
		=> new(_obj.get_ClientApplicationID(out var x), x!);

	public string ClientApplicationID
		=> ClientApplicationIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaServerSelection> ServerSelectionNoThrow
		=> new(_obj.get_ServerSelection(out var x), x);

	public WuaServerSelection ServerSelection
		=> ServerSelectionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ServiceIDNoThrow
		=> new(_obj.get_ServiceID(out var x), x!);

	public string ServiceID
		=> ServiceIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> UninstallationStepsNoThrow
		=> new(_obj.get_UninstallationSteps(out var x), new(x));

	public StringCollection UninstallationSteps
		=> UninstallationStepsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> UninstallationNotesNoThrow
		=> new(_obj.get_UninstallationNotes(out var x), x!);

	public string UninstallationNotes
		=> UninstallationNotesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> SupportUrlNoThrow
		=> new(_obj.get_SupportUrl(out var x), x!);

	public string SupportUrl
		=> SupportUrlNoThrow.Value;

	public override string ToString()
		=> TitleNoThrow.Or(null) ?? base.ToString()!;
}
