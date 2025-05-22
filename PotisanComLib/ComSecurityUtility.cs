using System.ComponentModel;

namespace Potisan.Windows.Com;

/// <summary>
/// COMセキュリティ機能の便利機能。
/// </summary>
public static class ComSecurityUtility
{
	// TODO
	// CoQueryClientBlanket 
	// CoQueryProxyBlanket

	/// <summary>
	/// プロセスのCOMセキュリティ設定を初期化します。
	/// </summary>
	/// <remarks>
	/// この関数はWinFormsでは失敗します。WinFormsは初期化時にCOMセキュリティを初期化するためです。
	/// </remarks>
	public static ComResult InitializeSecurityNoThrow(
			ComAuthenticationLevel authnLevel,
			ComImpersonateLevel impLevel,
			OleAuthenticationCap authnCaps)
	{
		[DllImport("ole32.dll")]
		static extern int CoInitializeSecurity(
			nint/*PSECURITY_DESCRIPTOR*/ pSecDesc,
			int cAuthSvc,
			nint/*SOLE_AUTHENTICATION_SERVICE* */ asAuthSvc,
			nint pReserved1,
			ComAuthenticationLevel dwAuthnLevel,
			ComImpersonateLevel dwImpLevel,
			nint pAuthList,
			OleAuthenticationCap dwCapabilities,
			nint pReserved3);

		return new(CoInitializeSecurity(0, -1, 0, 0, authnLevel, impLevel, 0, authnCaps, 0));
	}

	/// <inheritdoc cref="InitializeSecurityNoThrow(ComAuthenticationLevel, ComImpersonateLevel, OleAuthenticationCap)"/>
	public static void InitializeSecurity(
		ComAuthenticationLevel authnLevel,
		ComImpersonateLevel impLevel,
		OleAuthenticationCap authnCaps)
		=> InitializeSecurityNoThrow(authnLevel, impLevel, authnCaps).ThrowIfError();

	/// <summary>
	/// 対話中のサーバーのセキュリティを設定します。
	/// この関数はCOMクライアントでは使用されず、呼び出しても失敗します。
	/// </summary>
	public static ComResult SetProxyBlanketNoThrow(
		object o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
	{
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		static extern int CoSetProxyBlanket(
			[MarshalAs(UnmanagedType.IUnknown)] object pProxy,
			uint dwAuthnSvc,
			uint dwAuthzSvc,
			ref char pServerPrincName,
			uint dwAuthnLevel,
			uint dwImpLevel,
			ref byte pAuthInfo,
			uint dwCapabilities);

		const nint COLE_DEFAULT_PRINCIPAL = -1;
		const nint COLE_DEFAULT_AUTHINFO = -1;

		unsafe
		{
			return new(CoSetProxyBlanket(
				o,
				(uint)authnService,
				(uint)authzService,
				ref serverPrincipalName != null ? ref MemoryMarshal.GetReference(serverPrincipalName.AsSpan()) : ref Unsafe.AsRef<char>((void*)COLE_DEFAULT_PRINCIPAL),
				(uint)authLevel,
				(uint)impLevel,
				ref authInfo != null ? ref MemoryMarshal.GetArrayDataReference(authInfo) : ref Unsafe.AsRef<byte>((void*)COLE_DEFAULT_AUTHINFO),
				(uint)authCaps));
		}
	}
	/// <inheritdoc cref="SetProxyBlanketNoThrow"/>
	public static void SetProxyBlanket(
		object o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
	{
		SetProxyBlanketNoThrow(o, authnService, authzService, serverPrincipalName, authLevel, impLevel, authInfo, authCaps).ThrowIfError();
	}

	public static ComResult SetProxyBlanketNoThrow(
		IComUnknownWrapper o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
		=> SetProxyBlanketNoThrow(o.WrappedObject!, authnService, authzService, serverPrincipalName, authLevel, impLevel, authInfo, authCaps);

	public static void SetProxyBlanket(
		IComUnknownWrapper o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
		=> SetProxyBlanketNoThrow(o, authnService, authzService, serverPrincipalName, authLevel, impLevel, authInfo, authCaps).ThrowIfError();

	public static ComResult SetProxyBlanketWithDefaultsNoThrow(
		object o,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		OleAuthenticationCap authCaps)
	{
		return SetProxyBlanketNoThrow(o, ComAuthenticationService.Default, ComAuthorizationService.Default, null, authLevel, impLevel, null, authCaps);
	}
	public static void SetProxyBlanketWithDefaults(
		object o,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		OleAuthenticationCap authCaps)
		=> SetProxyBlanketWithDefaultsNoThrow(o, authLevel, impLevel, authCaps).ThrowIfError();

	public static ComResult SetProxyBlanketWithDefaultsNoThrow(
		IComUnknownWrapper o,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		OleAuthenticationCap authCaps)
		=> SetProxyBlanketWithDefaultsNoThrow(o.WrappedObject!, authLevel, impLevel, authCaps);

	public static void SetProxyBlanketWithDefaults(
		IComUnknownWrapper o,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		OleAuthenticationCap authCaps)
		=> SetProxyBlanketWithDefaultsNoThrow(o, authLevel, impLevel, authCaps).ThrowIfError();

	public static ComResult<ComSecurityBlanket> QueryProxyBlanketNoThrow(object o)
	{
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		static extern int CoQueryProxyBlanket(
			[MarshalAs(UnmanagedType.IUnknown)] object pProxy,
			out ComAuthenticationService pAuthnSvc,
			out ComAuthorizationService pAuthzSvc,
			[MarshalAs(UnmanagedType.LPWStr)] out string pServerPrincName,
			out ComAuthenticationLevel pAuthnLevel,
			out ComImpersonateLevel pImpLevel,
			out nint pAuthInfo,
			out OleAuthenticationCap pCapabilites);

		return new(CoQueryProxyBlanket(o, out var a, out var b, out var c, out var d, out var e, out var f, out var g),
			new(a, b, c, d, e, f, g));
	}

	public static ComSecurityBlanket QueryProxyBlanket(object o)
		=> QueryProxyBlanketNoThrow(o).Value;

	public static ComResult<ComSecurityBlanket> QueryProxyBlanketNoThrow(IComUnknownWrapper o)
		=> QueryProxyBlanketNoThrow(o.WrappedObject!);

	public static ComSecurityBlanket QueryProxyBlanket(IComUnknownWrapper o)
		=> QueryProxyBlanketNoThrow(o.WrappedObject!).Value;
}

public sealed record class ComSecurityBlanket(
	ComAuthenticationService AuthnSvc,
	ComAuthorizationService AuthzSvc,
	string? ServerPrincipalName,
	ComAuthenticationLevel AuthnLevel,
	ComImpersonateLevel ImpLevel,
	nint PrivilegeInfo,
	OleAuthenticationCap AuthnCaps);

/// <summary>
/// 認証サービス。
/// </summary>
public enum ComAuthenticationService : uint
{
	None = 0,
	DcePrivate = 1,
	DcePublic = 2,
	DecPublic = 4,
	GssNegotiate = 9,
	WinNT = 10,
	GssSChannel = 14,
	GssKerberos = 16,
	Dpa = 17,
	Msn = 18,
	Digest = 21,
	Kernel = 20,
	NegoExtender = 30,
	Pku2u = 31,
	LiveSSP = 32,
	LiveExpSsp = 35,
	CloudAP = 36,
	MSOnline = 82,
	MQ = 100,
	Default = 0xFFFFFFFF,
}

public enum ComAuthorizationService : uint
{
	None = 0,
	Name = 1,
	Dce = 2,
	Default = 0xffffffff,
}

/// <summary>
/// 認証レベル。
/// </summary>
public enum ComAuthenticationLevel : uint
{
	/// <summary>
	/// 既定の方法で認証レベルを選択します。
	/// </summary>
	Default = 0,

	/// <summary>
	/// 認証しません。
	/// </summary>
	None = 1,

	/// <summary>
	/// サーバーとの関係を確立した場合のみクライアントの資格情報を認証します。
	/// </summary>
	Connect = 2,

	/// <summary>
	/// サーバー要求の受信後、リモートプロシージャ呼び出し開始時のみ認証します。
	/// <see cref="Connect"/>の認証も使用します。
	/// </summary>
	Call = 3,

	/// <summary>
	/// 受信データが適切なクライアントのデータか認証します。
	/// <see cref="Call"/>の認証も使用します。
	/// </summary>
	Packet = 4,

	/// <summary>
	/// 転送データが適切なクライアントのデータか認証します。
	/// </summary>
	/// <see cref="Packet"/>の認証も使用します。
	PacketIntegrity = 5,

	/// <summary>
	/// リモートプロシージャの引数を暗号化します。
	/// <see cref="PacketIntegrity"/>の認証も使用します。
	/// </summary>
	PacketPrivacy = 6,
}

/// <summary>
/// クライアント偽装時にサーバーに与えられる権限の量。
/// </summary>
public enum ComImpersonateLevel : uint
{
	/// <summary>
	/// 既定の方法で偽装レベルを選択します。
	/// </summary>
	Default = 0,

	/// <summary>
	/// サーバーに対して匿名です。
	/// サーバーはクライアントを偽装できますが、偽装トークンは使用できません。
	/// </summary>
	Anonymous = 1,

	/// <summary>
	/// サーバーはクライアントIDを取得できます。
	/// サーバーはACLチェック用にクライアントを偽装できますが、システムオブジェクトにクライアントとしてアクセスできません。
	/// </summary>
	Identify = 2,

	/// <summary>
	/// サーバーはクライアントのセキュリティコンテキストを偽装できます。
	/// 偽装トークンは1つのマシン境界のみ越えられます。
	/// </summary>
	Impersonate = 3,

	/// <summary>
	/// サーバーはクライアントのセキュリティコンテキストを偽装できます。
	/// 偽装トークンは任意のマシン境界を越えられます。
	/// </summary>
	Delegate = 4,
}

/// <summary>
/// 機能フラグ。EOLE_AUTHENTICATION_CAPABILITIES。
/// </summary>
[Flags]
public enum OleAuthenticationCap : uint
{
	/// <summary>
	/// 機能フラグなし。
	/// </summary>
	None = 0,

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	[Obsolete("後方互換性のために残されています。")]
	MutalAuto = 0x1,

	/// <summary>
	/// 静的クローキングの設定。クライアントIDの決定時にスレッドトークンを使用します。
	/// スレッドトークンはプロキシの初回呼び出しまたは<c>CoSetProxyBlanket</c>呼び出し時に決定されます。
	/// <see cref="DynamicCloaking"/>と片方だけ指定できます。
	/// </summary>
	StaticCloaking = 0x20,

	/// <summary>
	/// 動的クローキングの設定。クライアントIDの決定時にスレッドトークンを使用します。
	/// プロキシの呼び出し毎にスレッドトークンが確認され、クライアントIDが変更されていれば再認証されます。
	/// <see cref="StaticCloaking"/>と片方だけ指定できます。
	/// </summary>
	DynamicCloaking = 0x40,

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("後方互換性のために残されています。")]
	AnyAuthority = 0x80,

	/// <summary>
	/// Schannelサーバープリンシパル名をfullsic形式にします。
	/// </summary>
	MakeFullsic = 0x100,

	/// <summary>
	/// <c>CoInitializeSecurity</c>の呼び出しで使用された<c>OleAuthenticationCap</c>を使用します。
	/// <c>IClientSecurity::SetBlanket</c>または<c>CoSetProxyBlanket</c>の呼び出しで使用されます。
	/// </summary>
	Default = 0x800,

	/// <summary>
	/// 悪意のあるユーザーによる使用中オブジェクトの解放を抑制するために分散参照カウント呼び出しを認証します。
	/// このフラグの指定時、認証レベルは0以外です。
	/// </summary>
	SecureRefs = 0x2,

	/// <summary>
	/// セキュリティデスクリプタは<c>IAccessControl</c>を指します。
	/// <see cref="AppID"/>とどちらかだけ指定できます。
	/// </summary>
	AccessControl = 0x4,

	/// <summary>
	/// セキュリティデスクリプタはAppIDのGUIDを指します。
	/// <see cref="AccessControl"/>とどちらかだけ指定できます。
	/// </summary>
	AppID = 0x8,

	[EditorBrowsable(EditorBrowsableState.Never)]
	/// <summary>
	/// システムの予約済みフラグ。
	/// </summary>
	Dynamic = 0x10,

	/// <summary>
	/// <c>SetProxyBlanket</c>でSchannelプリンシパル名にfullsic形式を要求します。
	/// </summary>
	RequireFullsic = 0x200,

	[EditorBrowsable(EditorBrowsableState.Never)]
	/// <summary>
	/// システムの予約済みフラグ。
	/// </summary>
	AutoInpersonate = 0x400,

	/// <summary>
	/// 呼び出し元IDによるサーバープロセス起動のアクティブ化(Activate-as-Activator)を失敗させます。
	/// 信用されていないコンポーネントによる特権アカウントで実行されるアプリケーションのID使用を抑制します。
	/// </summary>
	DisableAaa = 0x1000,

	/// <summary>
	/// CLSIDやカテゴリIDの制限によりサーバーのセキュリティを保護します。
	/// システム操作に不可欠なサービスで使用されます。
	/// </summary>
	NoCustomMarshal = 0x2000,

	[EditorBrowsable(EditorBrowsableState.Never)]
	/// <summary>
	/// システムの予約済みフラグ。
	/// </summary>
	Reserved1 = 0x4000
}
