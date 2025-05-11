using System.ComponentModel;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public static partial class ComHelper
{
	/// <summary>
	/// CLSIDを指定してCOMオブジェクトのRCWを作成します。
	/// </summary>
	public static ComResult<object> CreateInstanceNoThrow(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
	{
		Guid IID_IUnknown = new("00000000-0000-0000-C000-000000000046");
		return new(NativeMethods.CoCreateInstance(clsid, null, (uint)clsctx, IID_IUnknown, out var x), x!);
	}

	/// <inheritdoc cref="CreateInstanceNoThrow(in Guid, ComClassContext)"/>
	public static object CreateInstance(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		=> CreateInstanceNoThrow(clsid, clsctx).Value;

	/// <summary>
	/// CLSIDとCOMインターフェイスを指定してCOMオブジェクトのラッパーを作成します。
	/// </summary>
	public static ComResult<TWrapper> CreateInstanceNoThrow<TWrapper, TInterface>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
	{
		return IComUnknownWrapper.Wrap<TWrapper>(
			NativeMethods.CoCreateInstance(clsid, null, (uint)clsctx, typeof(TInterface).GUID, out var x), x);
	}

	/// <inheritdoc cref="CreateInstanceNoThrow{TWrapper, TInterface}(in Guid, ComClassContext)"/>
	public static TWrapper CreateInstance<TWrapper, TInterface>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
		=> CreateInstanceNoThrow<TWrapper, TInterface>(clsid, clsctx).Value;

	/// <summary>
	/// CLSIDを指定してCOMオブジェクトのラッパーを作成します。
	/// </summary>
	public static ComResult<TWrapper> CreateInstanceNoThrow<TWrapper>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
	{
		return CreateInstanceNoThrow<TWrapper, IUnknown>(clsid, clsctx);
	}

	/// <inheritdoc cref="CreateInstanceNoThrow{TWrapper}(in Guid, ComClassContext)"/>
	public static TWrapper CreateInstance<TWrapper>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
		=> CreateInstanceNoThrow<TWrapper>(clsid, clsctx).Value;

	private static class NativeMethods
	{
		[DllImport("ole32.dll")]
		public static extern int CoCreateInstance(
			in Guid rclsid,
			[MarshalAs(UnmanagedType.IUnknown)] object? pUnkOuter,
			uint dwClsContext,
			in Guid riid,
			[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);
	}
}

/// <summary>
/// COMクラスの実行コンテキスト。
/// </summary>
[Flags]
public enum ComClassContext : uint
{
	/// <summary>
	/// 同一プロセスで実行されるDLLです。
	/// </summary>
	InProcServer = 0x1,
	/// <summary>
	/// 同一プロセスで実行されるDLLです。
	/// </summary>
	InProcHandler = 0x2,
	/// <summary>
	/// 同一コンピューターの別プロセスで実行されます。
	/// </summary>
	LocalServer = 0x4,
	/// <summary>
	/// 別コンピューターで実行されます。
	/// </summary>
	RemoteServer = 0x10,
	/// <summary>
	/// ディレクトリサービスによるインターネットからのコードダウンロードを無効化します。
	/// </summary>
	NoCodeDownload = 0x400,
	/// <summary>
	/// カスタムマーシャリングの使用時にマーシャリングを失敗させます。
	/// </summary>
	NoCustomMarshal = 0x1000,
	/// <summary>
	/// ディレクトリサービスによるインターネットからのコードダウンロードを有効化します。
	/// </summary>
	EnableCodeDownload = 0x2000,
	/// <summary>
	/// エラー時のログ記録を無効化します。
	/// </summary>
	NoFailureLog = 0x4000,
	/// <summary>
	/// Activate-as-Activatorを無効化します。
	/// 特権アカウントのアプリケーション等に信頼されていないコンポーネントの起動を制限させられます。
	/// </summary>
	DisableAaa = 0x8000,
	/// <summary>
	/// Activate-as-Activatorを有効化します。
	/// アプリケーションはアクティブ化されたコンポーネントにIDを転送します。
	/// </summary>
	EnableAaa = 0x10000,
	/// <summary>
	/// 現在のアパートメントの既定コンテキストから開始します。
	/// </summary>
	FromDefaultContext = 0x20000,
	/// <summary>
	/// 32ビット版を使用します。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
	ActivateX86Server = 0x40000,
	/// <summary>
	/// 32ビット版を使用します。
	/// </summary>
	Activate32BitServer = ActivateX86Server,
	/// <summary>
	/// 64ビット版を使用します。
	/// </summary>
	Activate64BitServer = 0x80000,
	/// <summary>
	/// スレッドの偽装トークンを使用します。
	/// </summary>
	EnableCloaking = 0x100000,
	/// <summary>
	/// システムが使用します。アプリコンテナー用です。
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	AppContainer = 0x400000,
	/// <summary>
	/// As-Activatorサーバーの相互ユーザーアクティベーション動作を指定します。
	/// </summary>
	ActivateAaaAsUI = 0x800000,
	/// <summary>
	/// 説明なし。
	/// </summary>
	ActivateArm32Server = 0x2000000,
	/// <summary>
	/// 説明なし。
	/// </summary>
	AllowLowerTrustRegistration = 0x4000000,
	/// <summary>
	/// システムが使用します。プロキシ／スタブDLLの読み込みに使用します。
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	ProxyStubDll = 0x80000000,

	/// <summary>
	/// <see cref="RemoteServer"/>以外の全ての種類フラグを指定します。
	/// </summary>
	All = InProcServer | InProcHandler | LocalServer,
	/// <summary>
	/// <see cref="RemoteServer"/>を含む全ての種類フラグを指定します。
	/// </summary>
	AllWithRemote = InProcServer | InProcHandler | LocalServer | RemoteServer,
	/// <summary>
	/// サーバーを指定する種類フラグです。
	/// </summary>
	Server = InProcServer | LocalServer,
}
