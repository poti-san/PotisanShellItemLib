using System.Collections.Immutable;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaUpdateSearcher(object? o) : ComUnknownWrapperBase<IUpdateSearcher>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public static ComResult<WuaUpdateSearcher> CreateNoThrow()
	{
		// {B699E5E8-67FF-4177-88B0-3684A3388BFB}
		Guid CLSID_UpdateSearcher = new(0xB699E5E8, 0x67FF, 0x4177, 0x88, 0xB0, 0x36, 0x84, 0xA3, 0x38, 0x8B, 0xFB);
		// Both
		return ComHelper.CreateInstanceNoThrow<WuaUpdateSearcher, IUpdateSearcher>(CLSID_UpdateSearcher, ComClassContext.InProcServer);
	}

	public static WuaUpdateSearcher Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> CanAutomaticallyUpgradeServiceNoThrow
		=> new(_obj.get_CanAutomaticallyUpgradeService(out var x), x!);

	public ComResult SetCanAutomaticallyUpgradeServiceNoThrow(bool value)
		=> new(_obj.put_CanAutomaticallyUpgradeService(value));

	public bool CanAutomaticallyUpgradeService
	{
		get => CanAutomaticallyUpgradeServiceNoThrow.Value;
		set => SetCanAutomaticallyUpgradeServiceNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ClientApplicationIDNoThrow
		=> new(_obj.get_ClientApplicationID(out var x), x!);

	public ComResult SetClientApplicationIDNoThrow(string value)
		=> new(_obj.put_ClientApplicationID(value));

	public string ClientApplicationID
	{
		get => ClientApplicationIDNoThrow.Value;
		set => SetClientApplicationIDNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IncludePotentiallySupersededUpdatesNoThrow
		=> new(_obj.get_IncludePotentiallySupersededUpdates(out var x), x!);

	public ComResult SetIncludePotentiallySupersededUpdatesNoThrow(bool value)
		=> new(_obj.put_IncludePotentiallySupersededUpdates(value));

	public bool IncludePotentiallySupersededUpdates
	{
		get => IncludePotentiallySupersededUpdatesNoThrow.Value;
		set => SetIncludePotentiallySupersededUpdatesNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaServerSelection> ServerSelectionNoThrow
		=> new(_obj.get_ServerSelection(out var x), x!);

	public ComResult SetServerSelectionNoThrow(WuaServerSelection value)
		=> new(_obj.put_ServerSelection(value));

	public WuaServerSelection ServerSelection
	{
		get => ServerSelectionNoThrow.Value;
		set => SetServerSelectionNoThrow(value).ThrowIfError();
	}

	public ComResult<WuaSearchJob> BeginSearchNoThrow(string critetia, WuaSearchCompletedCallback? onCompleted = null, object? state = null)
		=> new(_obj.BeginSearch(critetia, onCompleted, state, out var x), new(x));

	public WuaSearchJob BeginSearch(string critetia, WuaSearchCompletedCallback? onCompleted = null, object? state = null)
		=> BeginSearchNoThrow(critetia, onCompleted, state).Value;

	public ComResult<WuaSearchResult> EndSearchNoThrow(WuaSearchJob searchJob)
		=> new(_obj.EndSearch((ISearchJob)searchJob.WrappedObject!, out var x), new(x));

	public WuaSearchResult EndSearch(WuaSearchJob searchJob)
		=> EndSearchNoThrow(searchJob).Value;

	public ComResult<string> EscapeStringNoThrow(string unescaped)
		=> new(_obj.EscapeString(unescaped, out var x), x!);

	public string EscapeString(string unescaped)
		=> EscapeStringNoThrow(unescaped).Value;

	public ComResult<WuaUpdateHistoryEntryCollection> QueryHistoryNoThrow(int startIndex = 0, int? count = null)
	{
		var cr = TotalHistoryCountNoThrow;
		if (!cr) return new(cr.HResult, null!);
		return new(_obj.QueryHistory(startIndex, count ?? cr.ValueUnchecked, out var x), new(x));
	}

	public WuaUpdateHistoryEntryCollection QueryHistory(int startIndex = 0, int? count = null)
		=> QueryHistoryNoThrow(startIndex, count).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateHistoryEntryCollection> AllHistoryCollectionNoThrow
		=> QueryHistoryNoThrow();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public WuaUpdateHistoryEntryCollection AllHistoryCollection
		=> AllHistoryCollectionNoThrow.Value;

	public ImmutableArray<WuaUpdateHistoryEntry> AllHistories
		=> [.. AllHistoryCollection];

	public ComResult<WuaSearchResult> SearchNoThrow(string criteria)
		=> new(_obj.Search(criteria, out var x), new(x));

	public WuaSearchResult Search(string criteria)
		=> SearchNoThrow(criteria).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> OnlineNoThrow
		=> new(_obj.get_Online(out var x), x!);

	public ComResult SetOnlineNoThrow(bool value)
		=> new(_obj.put_Online(value));

	public bool Online
	{
		get => OnlineNoThrow.Value;
		set => SetOnlineNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> TotalHistoryCountNoThrow
		=> new(_obj.GetTotalHistoryCount(out var x), x!);

	public int TotalHistoryCount
		=> TotalHistoryCountNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ServiceIDNoThrow
		=> new(_obj.get_ServiceID(out var x), x!);

	public ComResult SetServiceIDNoThrow(string value)
		=> new(_obj.put_ServiceID(value));

	public string ServiceID
	{
		get => ServiceIDNoThrow.Value;
		set => SetServiceIDNoThrow(value).ThrowIfError();
	}
}