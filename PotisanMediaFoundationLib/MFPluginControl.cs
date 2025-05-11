using System.Collections.Immutable;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFPluginControl(object? o) : ComUnknownWrapperBase<IMFPluginControl>(o)
{
	public static ComResult<MFPluginControl> CreateNoThrow()
	{
		[DllImport("mfplat.dll")]
		static extern int MFGetPluginControl(out IMFPluginControl ppPluginControl);

		return new(MFGetPluginControl(out var x), new(x));
	}

	public static MFPluginControl Create()
		=> CreateNoThrow().Value;

	public ComResult<Guid> GetPreferredClsidNoThrow(MFPluginType pluginType, string selector)
		=> new(_obj.GetPreferredClsid((uint)pluginType, selector, out var x), x);

	public Guid GetPreferredClsid(MFPluginType pluginType, string selector)
		=> GetPreferredClsidNoThrow(pluginType, selector).Value;

	public ComResult<(string Selector, Guid Clsid)> GetPreferredClsidNoThrow(MFPluginType pluginType, uint index)
		=> new(_obj.GetPreferredClsidByIndex((uint)pluginType, index, out var x1, out var x2), (x1, x2));

	public (string Selector, Guid Clsid) GetPreferredClsid(MFPluginType pluginType, uint index)
		=> GetPreferredClsidNoThrow(pluginType, index).Value;

	public uint CountPreferredClsids(MFPluginType pluginType)
	{
		const int ERROR_NO_MORE_ITEMS = 259;
		var hrNoMoreItems = HResultHelper.FromWin32(ERROR_NO_MORE_ITEMS);

		checked
		{
			for (uint i = 0; ; i++)
			{
				var hr = _obj.GetPreferredClsidByIndex((uint)pluginType, i, out _, out _);
				if (hr == hrNoMoreItems)
					return i;
			}
		}
	}

	public IEnumerable<(string Selector, Guid Clsid)> EnumerateSelectorsAndClsids(MFPluginType pluginType)
	{
		var c = CountPreferredClsids(pluginType);
		for (uint i = 0; i < c; i++)
		{
			yield return GetPreferredClsid(pluginType, i);
		}
	}

	public ImmutableArray<(string Selector, Guid Clsid)> GetSelectorAndClsidArray(MFPluginType pluginType)
		=> [.. EnumerateSelectorsAndClsids(pluginType)];

	public IEnumerable<(string Selector, Guid Clsid)> MFTSelectorAndClsidEnumerator => EnumerateSelectorsAndClsids(MFPluginType.MFT);
	public IEnumerable<(string Selector, Guid Clsid)> MediaSourceSelectorAndClsidEnumerator => EnumerateSelectorsAndClsids(MFPluginType.MediaSource);
	public IEnumerable<(string Selector, Guid Clsid)> MFTMatchOutputTypeSelectorAndClsidEnumerator => EnumerateSelectorsAndClsids(MFPluginType.MFTMatchOutputType);

	public ImmutableArray<(string Selector, Guid Clsid)> MFTSelectorAndClsidArray => GetSelectorAndClsidArray(MFPluginType.MFT);
	public ImmutableArray<(string Selector, Guid Clsid)> MediaSourceSelectorAndClsidArray => GetSelectorAndClsidArray(MFPluginType.MediaSource);
	public ImmutableArray<(string Selector, Guid Clsid)> MFTMatchOutputTypeSelectorAndClsidArray => GetSelectorAndClsidArray(MFPluginType.MFTMatchOutputType);

	public ComResult SetPrefferedClsidNoThrow(MFPluginType pluginType, string selector, in Guid clsid)
		=> new(_obj.SetPreferredClsid((uint)pluginType, selector, clsid));

	public void SetPrefferedClsid(MFPluginType pluginType, string selector, in Guid clsid)
		=> SetPrefferedClsidNoThrow(pluginType, selector, clsid).ThrowIfError();

	public ComResult<bool> IsDisabledNoThrow(MFPluginType pluginType, in Guid clsid)
		=> ComResult.HRSuccess(_obj.IsDisabled((uint)pluginType, clsid));

	public bool IsDisabled(MFPluginType pluginType, in Guid clsid)
		=> IsDisabledNoThrow(pluginType, clsid).Value;

	public ComResult<Guid> GetDisabledClsidNoThrow(MFPluginType pluginType, uint index)
		=> new(_obj.GetDisabledByIndex((uint)pluginType, index, out var x), x);

	public Guid GetDisabledClsid(MFPluginType pluginType, uint index)
		=> GetDisabledClsidNoThrow(pluginType, index).Value;

	public uint CountDisabledClsids(MFPluginType pluginType)
	{
		const int ERROR_NO_MORE_ITEMS = 259;
		var hrNoMoreItems = HResultHelper.FromWin32(ERROR_NO_MORE_ITEMS);

		checked
		{
			for (uint i = 0; ; i++)
			{
				var hr = _obj.GetDisabledByIndex((uint)pluginType, i, out _);
				if (hr == hrNoMoreItems)
					return i;
			}
		}
	}

	public IEnumerable<Guid> EnumerateDisabledClsids(MFPluginType pluginType)
	{
		var c = CountDisabledClsids(pluginType);
		for (uint i = 0; i < c; i++)
		{
			yield return GetDisabledClsid(pluginType, i);
		}
	}

	public ImmutableArray<Guid> GetDisabledClsids(MFPluginType pluginType)
		=> [.. EnumerateDisabledClsids(pluginType)];

	public IEnumerable<Guid> MFTDisabledClsidsEnumerable => EnumerateDisabledClsids(MFPluginType.MFT);
	public IEnumerable<Guid> MediaSourceDisabledClsidsEnumerable => EnumerateDisabledClsids(MFPluginType.MediaSource);
	public IEnumerable<Guid> MFTMatchOutputTypeDisabledClsidsEnumerables => EnumerateDisabledClsids(MFPluginType.MFTMatchOutputType);

	public ImmutableArray<Guid> MFTDisabledClsids => GetDisabledClsids(MFPluginType.MFT);
	public ImmutableArray<Guid> MediaSourceDisabledClsids => GetDisabledClsids(MFPluginType.MediaSource);
	public ImmutableArray<Guid> MFTMatchOutputTypeDisabledClsids => GetDisabledClsids(MFPluginType.MFTMatchOutputType);

	public ComResult SetDisabledClsidNoThrow(MFPluginType pluginType, in Guid clsid, bool disabled)
		=> new(_obj.SetDisabled((uint)pluginType, clsid, disabled));

	public void SetDisabledClsid(MFPluginType pluginType, in Guid clsid, bool disabled)
		=> SetDisabledClsidNoThrow(pluginType, clsid, disabled).ThrowIfError();
}

public enum MFPluginType : uint
{
	MFT = 0,
	MediaSource = 1,
	MFTMatchOutputType = 2,
	Other = 0xffffffff,
}
