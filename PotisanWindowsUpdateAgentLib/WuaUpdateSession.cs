using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// WUA更新セッション。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <see cref="Create"/>または<see cref="WuaUpdateSession.CreateUpdateSearcher"/>から作成できます。
/// </remarks>
public sealed class WuaUpdateSession(object? o) : ComUnknownWrapperBase<IUpdateSession>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	public static ComResult<WuaUpdateSession> CreateNoThrow()
	{
		// {4CB43D7F-7EEE-4906-8698-60DA1C38F2FE}
		Guid CLSID_UpdateSession = new(0x4CB43D7F, 0x7EEE, 0x4906, 0x86, 0x98, 0x60, 0xDA, 0x1C, 0x38, 0xF2, 0xFE);
		// Both
		return ComHelper.CreateInstanceNoThrow<WuaUpdateSession, IUpdateSession>(CLSID_UpdateSession, ComClassContext.InProcServer);
	}

	public static WuaUpdateSession Create()
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
	public ComResult<bool> ReadOnlyNoThrow
		=> new(_obj.get_ReadOnly(out var x), x!);

	public bool ReadOnly
		=> ReadOnlyNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaWebProxy> WebProxyNoThrow
		=> new(_obj.get_WebProxy(out var x), new(x));

	public ComResult SetWebProxyNoThrow(WuaWebProxy value)
		=> new(_obj.put_WebProxy((IWebProxy)value.WrappedObject!));

	public WuaWebProxy WebProxy
	{
		get => WebProxyNoThrow.Value;
		set => SetWebProxyNoThrow(value).ThrowIfError();
	}

	public ComResult<WuaUpdateSearcher> CreateUpdateSearcherNoThrow()
		=> new(_obj.CreateUpdateSearcher(out var x), new(x));

	public WuaUpdateSearcher CreateUpdateSearcher()
		=> CreateUpdateSearcherNoThrow().Value;

	public ComResult<WuaUpdateDownloader> CreateUpdateDownloaderNoThrow()
		=> new(_obj.CreateUpdateDownloader(out var x), new(x));

	public WuaUpdateDownloader CreateUpdateDownloader()
		=> CreateUpdateDownloaderNoThrow().Value;

	public ComResult<WuaUpdateInstaller> CreateUpdateInstallerNoThrow()
		=> new(_obj.CreateUpdateInstaller(out var x), new(x));

	public WuaUpdateInstaller CreateUpdateInstaller()
		=> CreateUpdateInstallerNoThrow().Value;
}
