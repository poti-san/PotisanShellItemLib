using System.Collections.Immutable;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaUpdateDownloader(object? o) : ComUnknownWrapperBase<IUpdateDownloader>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	public static ComResult<WuaUpdateDownloader> CreateNoThrow()
	{
		// {5BAF654A-5A07-4264-A255-9FF54C7151E7}
		Guid CLSID_UpdateDownloader = new(0x5BAF654A, 0x5A07, 0x4264, 0xA2, 0x55, 0x9F, 0xF5, 0x4C, 0x71, 0x51, 0xE7);
		// Apartment: Both
		return ComHelper.CreateInstanceNoThrow<WuaUpdateDownloader, IUpdateDownloader>(CLSID_UpdateDownloader, ComClassContext.InProcServer);
	}

	public static WuaUpdateDownloader Create()
		=> CreateNoThrow().Value;

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
	public ComResult<bool> IsForcedNoThrow
		=> new(_obj.get_IsForced(out var x), x!);

	public ComResult SetIsForcedNoThrow(bool value)
		=> new(_obj.put_IsForced(value));

	public bool IsForced
	{
		get => IsForcedNoThrow.Value;
		set => SetIsForcedNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaDownloadPriority> PriorityNoThrow
		=> new(_obj.get_Priority(out var x), x!);

	public ComResult SetPriorityNoThrow(WuaDownloadPriority value)
		=> new(_obj.put_Priority(value));

	public WuaDownloadPriority Priority
	{
		get => PriorityNoThrow.Value;
		set => SetPriorityNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateCollection> UpdateCollectionNoThrow
		=> new(_obj.get_Updates(out var x), new(x));

	public ComResult SetUpdateCollectionNoThrow(WuaUpdateCollection value)
		=> new(_obj.put_Updates((IUpdateCollection)value.WrappedObject!));

	public WuaUpdateCollection UpdateCollection
	{
		get => UpdateCollectionNoThrow.Value;
		set => SetUpdateCollectionNoThrow(value).ThrowIfError();
	}

	// TODO set
	public ImmutableArray<WuaUpdate> Update => [.. UpdateCollection];

	public ComResult<WuaDownloadJob> BeginDownloadNoThrow(
		WuaDownloadProgressChangedCallback? onProgressChanged = null,
		WuaDownloadCompletedCallback? onCompleted = null,
		object? state = null)
	{
		return new(_obj.BeginDownload(onProgressChanged, onCompleted, state, out var x), new(x));
	}

	public WuaDownloadJob BeginDownload(
		WuaDownloadProgressChangedCallback? onProgressChanged = null,
		WuaDownloadCompletedCallback? onCompleted = null,
		object? state = null)
		=> BeginDownloadNoThrow(onProgressChanged, onCompleted, state).Value;

	public ComResult<WuaDownloadResult> DownloadNoThrow()
		=> new(_obj.Download(out var x), new(x));

	public WuaDownloadResult Download()
		=> DownloadNoThrow().Value;

	public ComResult<WuaDownloadResult> EndDownloadNoThrow(WuaDownloadJob value)
		=> new(_obj.EndDownload((IDownloadJob)value.WrappedObject!, out var x), new(x));

	public WuaDownloadResult EndDownload(WuaDownloadJob value)
		=> EndDownloadNoThrow(value).Value;
}
