using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFPluginControl2(object? o) : MFPluginControl(o)
{
	protected new readonly IMFPluginControl2 _obj = o != null ? (IMFPluginControl2)o : null!;

	public new static ComResult<MFPluginControl2> CreateNoThrow()
	{
		[DllImport("mfplat.dll")]
		static extern int MFGetPluginControl(out IMFPluginControl ppPluginControl);

		var hr = MFGetPluginControl(out var x);
		if (hr != 0) return new(hr, new(null));

		return IComUnknownWrapper.Casted<MFPluginControl2, IMFPluginControl2>(x);
	}

	public ComResult SetPolicyNoThrow(MFPluginControlPolicy policy)
		=> new(_obj.SetPolicy(policy));

	public void SetPolicy(MFPluginControlPolicy policy)
		=> SetPolicyNoThrow(policy).ThrowIfError();
}

/// <summary>
/// MF_PLUGIN_CONTROL_POLICY
/// </summary>
public enum MFPluginControlPolicy
{
	UseAllPlugins = 0,
	UseApprovedPlugins = 1,
	UseWebPlugins = 2,
	UseWebPluginsEdgeMode = 3
}
