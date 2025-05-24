using System.Collections.Immutable;

using Potisan.Windows.Com.Automation.ComTypes;
using Potisan.Windows.Shell.Window.ComTypes;

namespace Potisan.Windows.Shell.Window;

/// <summary>
/// シェルウィンドウコレクション。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para>このコレクションは複数種類のウィンドウを扱うため、<see cref="IEnumerable{T}"/>等は実装しません。</para>
/// <para><c>IShellWindows</c> COMインターフェイスのラッパーです。</para>
/// </remarks>
public class ShellWindows(object? o) : ComUnknownWrapperBase<IShellWindows>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	/// <summary>
	/// シェルウィンドウコレクションのインターフェイスを作成します。
	/// </summary>
	/// <returns></returns>
	/// <remarks>
	///このCOMクラスはローカルセーバーです。スレッディングモデルに関係なく使用できます。
	/// </remarks>
	public static ComResult<ShellWindows> CreateNoThrow()
	{
		Guid CLSID_ShellWindows = new("9BA05972-F6A8-11CF-A442-00A0C90A8F39");
		return ComHelper.CreateInstanceNoThrow<ShellWindows>(CLSID_ShellWindows, ComClassContext.LocalServer);
	}

	/// <inheritdoc cref="CreateNoThrow"/>
	public static ShellWindows Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public ComResult<ShellWindowDispatch> ItemNoThrow(object index)
		=> new(_obj.Item(index, out var x), new(x));

	public ShellWindowDispatch Item(object index)
		=> ItemNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ShellWindowDispatch> ItemEnumerable
	{
		get
		{
			var c = Count;
			for (int i = 0; i < c; i++)
				yield return Item(i);
		}
	}

	public ImmutableArray<ShellWindowDispatch> Items
		=> [.. ItemEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ShellBrowser> ShellBrowserEnumerable
	{
		get
		{
			var c = Count;
			for (int i = 0; i < c; i++)
			{
				if (Item(i).QueryTopLevelShellBrowserNoThrow() is { Succeeded: true, ValueUnchecked: var value })
					yield return value;
			}
		}
	}

	public ImmutableArray<ShellBrowser> ShellBrowsers
		=> [.. ShellBrowserEnumerable];

	//[PreserveSig]
	//int _NewEnum(
	//	[MarshalAs(UnmanagedType.IUnknown)] out object? ppunk);

	public ComResult<int> RegisterNoThrow(ComDispatch id, int windowHandle, ShellWindowClass shellWindowClass)
		=> new(_obj.Register(id.WrappedObject!, windowHandle, (int)shellWindowClass, out var x), x);

	public int Register(ComDispatch id, int windowHandle, ShellWindowClass shellWindowClass)
		=> RegisterNoThrow(id, windowHandle, shellWindowClass).Value;

	public ComResult<int> RegisterPendingNoThrow(int threadId, SafeHandle pidlAbsolute, ShellWindowClass shellWindowClass)
		=> new(_obj.RegisterPending(threadId, pidlAbsolute.DangerousGetHandle(), null!, (int)shellWindowClass, out var x), x);

	public int RegisterPending(int threadId, SafeHandle pidlAbsolute, ShellWindowClass shellWindowClass)
		=> RegisterPendingNoThrow(threadId, pidlAbsolute, shellWindowClass).Value;

	public ComResult RevokeNoThrow(int cookie)
		=> new(_obj.Revoke(cookie));

	public void Revoke(int cookie)
		=> RevokeNoThrow(cookie).ThrowIfError();

	public ComResult OnNavigateNoThrow(int cookie, SafeHandle pidlAbsolute)
		=> new(_obj.OnNavigate(cookie, pidlAbsolute.DangerousGetHandle()));

	public void OnNavigate(int cookie, SafeHandle pidlAbsolute)
		=> OnNavigateNoThrow(cookie, pidlAbsolute).ThrowIfError();

	public ComResult OnActivatedNoThrow(int cookie, bool activate)
		=> new(_obj.OnActivated(cookie, activate));

	public void OnActivated(int cookie, bool activate)
		=> OnActivatedNoThrow(cookie, activate).ThrowIfError();

	private const int SWFO_NEEDDISPATCH = 0x1;
	private const int SWFO_INCLUDEPENDING = 0x2;
	private const int SWFO_COOKIEPASSED = 0x4;

	public ComResult<(int WindowHandle, ShellWindowDispatch Dispatch)> FindWindowNoThrow(
		SafeHandle pidl,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
	{
		var flags = (needDispatch ? SWFO_NEEDDISPATCH : 0) | (includePending ? SWFO_INCLUDEPENDING : 0);
		return new(_obj.FindWindowSW(pidl.DangerousGetHandle(), null!, (int)shellWindowClass, out var x1, flags, out var x2), (x1, new(x2)));
	}

	public (int WindowHandle, ShellWindowDispatch Dispatch) FindWindow(
		SafeHandle pidl,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
		=> FindWindowNoThrow(pidl, shellWindowClass, needDispatch, includePending).Value;

	public ComResult<int> FindWindowHandleNoThrow(
		SafeHandle pidl,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
	{
		var flags = (needDispatch ? SWFO_NEEDDISPATCH : 0) | (includePending ? SWFO_INCLUDEPENDING : 0);
		return new(_obj.FindWindowSW(pidl.DangerousGetHandle(), null!, (int)shellWindowClass, out var x, flags, out Unsafe.NullRef<object?>()), x);
	}

	public int FindWindowHandle(
		SafeHandle pidl,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
		=> FindWindowHandleNoThrow(pidl, shellWindowClass, needDispatch, includePending).Value;

	public ComResult<ShellWindowDispatch> FindWindowDispatchNoThrow(
		SafeHandle pidl,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
	{
		var flags = (needDispatch ? SWFO_NEEDDISPATCH : 0) | (includePending ? SWFO_INCLUDEPENDING : 0);
		return new(_obj.FindWindowSW(pidl.DangerousGetHandle(), null!, (int)shellWindowClass, out Unsafe.NullRef<int>(), flags, out var x), new(x));
	}

	public ShellWindowDispatch FindWindowDispatch(
		SafeHandle pidl,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
		=> FindWindowDispatchNoThrow(pidl, shellWindowClass, needDispatch, includePending).Value;

	public ComResult<(int WindowHandle, ShellWindowDispatch Dispatch)> FindWindowByCookieNoThrow(
		int cookie,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
	{
		var flags = (needDispatch ? SWFO_NEEDDISPATCH : 0) | (includePending ? SWFO_INCLUDEPENDING : 0) | SWFO_COOKIEPASSED;
		return new(_obj.FindWindowSW(cookie, null!, (int)shellWindowClass, out var x1, flags, out var x2), (x1, new(x2)));
	}

	public (int WindowHandle, ShellWindowDispatch Dispatch) FindWindowByCookie(
		int cookie,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
		=> FindWindowByCookieNoThrow(cookie, shellWindowClass, needDispatch, includePending).Value;

	public ComResult<int> FindWindowByCookieHandleNoThrow(
		int cookie,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
	{
		var flags = (needDispatch ? SWFO_NEEDDISPATCH : 0) | (includePending ? SWFO_INCLUDEPENDING : 0) | SWFO_COOKIEPASSED;
		return new(_obj.FindWindowSW(cookie, null!, (int)shellWindowClass, out var x, flags, out Unsafe.NullRef<object?>()), x);
	}

	public int FindWindowByCookieHandle(
		int cookie,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
		=> FindWindowByCookieHandleNoThrow(cookie, shellWindowClass, needDispatch, includePending).Value;

	public ComResult<ShellWindowDispatch> FindWindowByCookieDispatchNoThrow(
		int cookie,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
	{
		var flags = (needDispatch ? SWFO_NEEDDISPATCH : 0) | (includePending ? SWFO_INCLUDEPENDING : 0) | SWFO_COOKIEPASSED;
		return new(_obj.FindWindowSW(cookie, null!, (int)shellWindowClass, out Unsafe.NullRef<int>(), flags, out var x), new(x));
	}

	public ShellWindowDispatch FindWindowByCookieDispatch(
		int cookie,
		ShellWindowClass shellWindowClass,
		bool needDispatch = false,
		bool includePending = false)
		=> FindWindowByCookieDispatchNoThrow(cookie, shellWindowClass, needDispatch, includePending).Value;

	public ComResult OnCreatedNoThrow(int cookie, object newWindow)
		=> new(_obj.OnCreated(cookie, newWindow));

	public void OnCreated(int cookie, object newWindow)
		=> OnCreatedNoThrow(cookie, newWindow).ThrowIfError();

	public ComResult ProcessAttachDetachNoThrow(bool attach)
		=> new(_obj.ProcessAttachDetach(attach));

	public void ProcessAttachDetach(bool attach)
		=> ProcessAttachDetachNoThrow(attach).ThrowIfError();
}

public enum ShellWindowClass
{
	Explorer = 0,
	Browser = 0x1,
	ThirdParty = 0x2,
	Callback = 0x4,
	Desktop = 0x8,
}
