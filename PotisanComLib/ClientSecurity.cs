using System.Runtime.CompilerServices;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// クライアントセキュリティ。IClientSecurity COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class ClientSecurity(object? o) : ComUnknownWrapperBase<IClientSecurity>(o)
{
	public static ComResult<ClientSecurity> CreateFromProxyNoThrow(object o)
	{
		return o is IClientSecurity p ? new(CommonHResults.SOK, new(p)) : new(CommonHResults.ENoInterface, new(null));
	}

	public static ClientSecurity CreateFromProxy(object o)
		=> CreateFromProxyNoThrow(o).Value;

	public static ComResult<ClientSecurity> CreateFromProxyNoThrow(IComUnknownWrapper o)
		=> CreateFromProxyNoThrow(o.WrappedObject!);

	public static ClientSecurity CreateFromProxy(IComUnknownWrapper o)
		=> CreateFromProxyNoThrow(o).Value;

	public ComResult<ComSecurityBlanket> QueryBlanketNoThrow(object o)
	{
		return new(
			_obj.QueryBlanket(o, out var a, out var b, out var c, out var d, out var e, out var f, out var g),
			new(a, b, c, d, e, f, g));
	}
	public ComSecurityBlanket QueryBlanket(object o) => QueryBlanketNoThrow(o).Value;

	public ComResult<ComSecurityBlanket> QueryBlanketNoThrow(IComUnknownWrapper o) => QueryBlanketNoThrow(o.WrappedObject!);

	public ComSecurityBlanket QueryBlanket(IComUnknownWrapper o) => QueryBlanketNoThrow(o).Value;

	public ComResult SetBlanketNoThrow(
		object o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
	{
		const nint COLE_DEFAULT_PRINCIPAL = -1;
		const nint COLE_DEFAULT_AUTHINFO = -1;

		unsafe
		{
			return new(_obj.SetBlanket(
				o,
				authnService,
				authzService,
				ref serverPrincipalName != null ? ref MemoryMarshal.GetReference(serverPrincipalName.AsSpan()) : ref Unsafe.AsRef<char>((void*)COLE_DEFAULT_PRINCIPAL),
				authLevel,
				impLevel,
				ref authInfo != null ? ref MemoryMarshal.GetArrayDataReference(authInfo) : ref Unsafe.AsRef<byte>((void*)COLE_DEFAULT_AUTHINFO),
				authCaps));
		}
	}

	public void SetBlanket(
		object o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
	{
		SetBlanketNoThrow(o, authnService, authzService, serverPrincipalName, authLevel, impLevel, authInfo, authCaps).ThrowIfError();
	}

	public ComResult SetBlanketNoThrow(
		IComUnknownWrapper o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
		=> SetBlanketNoThrow(o.WrappedObject!, authnService, authzService, serverPrincipalName, authLevel, impLevel, authInfo, authCaps);

	public void SetBlanket(
		IComUnknownWrapper o,
		ComAuthenticationService authnService,
		ComAuthorizationService authzService,
		string? serverPrincipalName,
		ComAuthenticationLevel authLevel,
		ComImpersonateLevel impLevel,
		byte[]? authInfo,
		OleAuthenticationCap authCaps)
		=> SetBlanketNoThrow(o, authnService, authzService, serverPrincipalName, authLevel, impLevel, authInfo, authCaps).ThrowIfError();

	public ComResult<object> CopyProxyNoThrow(object proxy)
	{
		return new(_obj.CopyProxy(proxy, out var x), x);
	}

	public object CopyProxy(object proxy)
	{
		return CopyProxyNoThrow(proxy).Value;
	}

	public ComResult<TWrapper> CopyProxyNoThrow<TWrapper>(TWrapper proxy)
		where TWrapper : IComUnknownWrapper
	{
		return IComUnknownWrapper.Wrap<TWrapper>(_obj.CopyProxy(proxy.WrappedObject!, out var x), x);
	}

	public TWrapper CopyProxy<TWrapper>(TWrapper proxy)
		where TWrapper : IComUnknownWrapper
	{
		return CopyProxyNoThrow(proxy).Value;
	}
}