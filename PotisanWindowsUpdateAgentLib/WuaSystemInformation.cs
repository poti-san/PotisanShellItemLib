using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// WUAに関係するシステム情報。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <see cref="Create"/>で作成できます。
/// </remarks>
public class WuaSystemInformation(object? o) : ComUnknownWrapperBase<ISystemInformation>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	public static ComResult<WuaSystemInformation> CreateNoThrow()
	{
		Guid CLSID_SystemInformation = new(0xC01B9BA0, 0xBEA7, 0x41BA, 0xB6, 0x04, 0xD0, 0xA3, 0x6F, 0x46, 0x91, 0x33);
		// Both
		return ComHelper.CreateInstanceNoThrow<WuaSystemInformation, ISystemInformation>(
			CLSID_SystemInformation, ComClassContext.InProcServer);
	}

	public static WuaSystemInformation Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> OemHardwareSupportLinkNoThrow
		=> new(_obj.get_OemHardwareSupportLink(out var x), x!);

	public string OemHardwareSupportLink
		=> OemHardwareSupportLinkNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> RebootRequiredNoThrow
		=> new(_obj.get_RebootRequired(out var x), x!);

	public bool RebootRequired
		=> RebootRequiredNoThrow.Value;
}
