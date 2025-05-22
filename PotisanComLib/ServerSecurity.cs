using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// サーバーセキュリティ。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para><c>IServerSecurity</c> COMインターフェイスのラッパーです。</para>
/// </remarks>
public class ServerSecurity(object? o) : ComUnknownWrapperBase<IServerSecurity>(o)
{
	public static ComResult<ServerSecurity> GetCallContextNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CoGetCallContext(in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppInterface);

		return new(CoGetCallContext(typeof(IServerSecurity).GUID, out var x), new(x));
	}
	public static ServerSecurity GetCallContext()
		=> GetCallContextNoThrow().Value;

	public ComResult<ComSecurityBlanket> BlanketNoThrow
		=> new(
			_obj.QueryBlanket(out var a, out var b, out var c, out var d, out var e, out var f, out var g),
			new((ComAuthenticationService)a, (ComAuthorizationService)b, c, (ComAuthenticationLevel)d, (ComImpersonateLevel)e, f, (OleAuthenticationCap)g));

	public ComSecurityBlanket Blanket
		=> BlanketNoThrow.Value;

	public ComResult ImpersonateClientNoThrow()
		=> new(_obj.ImpersonateClient());

	public void ImpersonateClient()
		=> ImpersonateClientNoThrow().ThrowIfError();

	public ComResult RevertToSelfNoThrow()
		=> new(_obj.RevertToSelf());

	public void RevertToSelf()
		=> RevertToSelfNoThrow().ThrowIfError();

	public ComResult<bool> IsImpersonatingNoThrow
		=> ComResult.HRSuccess(_obj.IsImpersonating());

	public bool IsImpersonating
		=> IsImpersonatingNoThrow.Value;
}
