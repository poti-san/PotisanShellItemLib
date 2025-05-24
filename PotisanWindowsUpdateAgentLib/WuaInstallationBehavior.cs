using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaInstallationBehavior(object? o) : ComUnknownWrapperBase<IInstallationBehavior>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> CanRequestUserInputNoThrow
		=> new(_obj.get_CanRequestUserInput(out var x), x!);

	public bool CanRequestUserInput
		=> CanRequestUserInputNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaInstallationImpact> ImpactNoThrow
		=> new(_obj.get_Impact(out var x), x!);

	public WuaInstallationImpact Impact
		=> ImpactNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaInstallationRebootBehavior> RebootBehaviorNoThrow
		=> new(_obj.get_RebootBehavior(out var x), x!);

	public WuaInstallationRebootBehavior RebootBehavior
		=> RebootBehaviorNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> RequiresNetworkConnectivityNoThrow
		=> new(_obj.get_RequiresNetworkConnectivity(out var x), x!);

	public bool RequiresNetworkConnectivity
		=> RequiresNetworkConnectivityNoThrow.Value;
}
