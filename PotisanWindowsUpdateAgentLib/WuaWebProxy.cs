using System.ComponentModel;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaWebProxy(object? o) : ComUnknownWrapperBase<IWebProxy>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public static ComResult<WuaWebProxy> CreateNoThrow()
	{
		// {650503cf-9108-4ddc-a2ce-6c2341e1c582}
		Guid CLSID_WebProxy = new(0x650503cf, 0x9108, 0x4ddc, 0xa2, 0xce, 0x6c, 0x23, 0x41, 0xe1, 0xc5, 0x82);
		// Both
		return ComHelper.CreateInstanceNoThrow<WuaWebProxy, IWebProxy>(CLSID_WebProxy, ComClassContext.InProcServer);
	}

	public static WuaWebProxy Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> AddressNoThrow
		=> new(_obj.get_Address(out var x), x!);

	public ComResult SetAddressNoThrow(string? value)
		=> new(_obj.put_Address(value));

	public string? Address
	{
		get => AddressNoThrow.Value;
		set => SetAddressNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StringCollection> BypassListNoThrow
		=> new(_obj.get_BypassList(out var x), new(x));

	public ComResult SetBypassListNoThrow(StringCollection value)
		=> new(_obj.put_BypassList((IStringCollection)value.WrappedObject!));

	public StringCollection BypassList
	{
		get => BypassListNoThrow.Value;
		set => SetBypassListNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> BypassProxyOnLocalNoThrow
		=> new(_obj.get_BypassProxyOnLocal(out var x), x!);

	public ComResult SetBypassProxyOnLocalNoThrow(bool value)
		=> new(_obj.put_BypassProxyOnLocal(value));

	public bool BypassProxyOnLocal
	{
		get => BypassProxyOnLocalNoThrow.Value;
		set => SetBypassProxyOnLocalNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> ReadOnlyNoThrow
		=> new(_obj.get_ReadOnly(out var x), x);

	public bool ReadOnly
		=> ReadOnlyNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string?> UserNameNoThrow
		=> new(_obj.get_UserName(out var x), x);

	public ComResult SetUserNameNoThrow(string? value)
		=> new(_obj.put_UserName(value));

	public string? UserName
	{
		get => UserNameNoThrow.Value;
		set => SetUserNameNoThrow(value).ThrowIfError();
	}

	public ComResult SetPasswordNoThrow(string value)
		=> new(_obj.SetPassword(value));

	public void SetPassword(string value)
		=> SetPasswordNoThrow(value).ThrowIfError();

	// TODO: IOleWindow？
	public ComResult PromptForCredentialsNoThrow(object? parentWindow, string title)
		=> new(_obj.PromptForCredentials(parentWindow, title));

	public void PromptForCredentials(object? parentWindow, string title)
		=> PromptForCredentialsNoThrow(parentWindow, title).ThrowIfError();

	public ComResult PromptForCredentialsNoThrow(nint parentWindow, string title)
		=> new(_obj.PromptForCredentialsFromHwnd(parentWindow, title));

	public void PromptForCredentials(nint parentWindow, string title)
		=> PromptForCredentialsNoThrow(parentWindow, title).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> AutoDetectNoThrow
		=> new(_obj.get_AutoDetect(out var x), x!);

	public ComResult SetAutoDetectNoThrow(bool value)
		=> new(_obj.put_AutoDetect(value));

	public bool AutoDetect
	{
		get => AutoDetectNoThrow.Value;
		set => SetAutoDetectNoThrow(value).ThrowIfError();
	}
}
