using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.Com;
using Potisan.Windows.DXCore.ComTypes;

namespace Potisan.Windows.DXCore;

/// <summary>
/// DXCoreのアダプタ情報リスト。<see cref="DXCoreAdapterFactory"/>やその派生クラスから作成します。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class DXCoreAdapterList(object? o) : ComUnknownWrapperBase<IDXCoreAdapterList>(o)
{
	public uint AdapterCount
		=> _obj.GetAdapterCount();

	public ComResult<object> GetAdapterNoThrow(uint index, in Guid iid)
		=> new(_obj.GetAdapter(index, iid, out var x), x!);

	public object GetAdapter(uint index, in Guid iid)
		=> GetAdapterNoThrow(index, iid).Value;

	public ComResult<TWrapper> GetAdapterTNoThrow<TWrapper, TInterface>(uint index)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetAdapter(index, typeof(TInterface).GUID, out var x), x);

	public TWrapper GetAdapterT<TWrapper, TInterface>(uint index)
		where TWrapper : IComUnknownWrapper
		=> GetAdapterTNoThrow<TWrapper, TInterface>(index).Value;

	public ComResult<DXCoreAdapter> GetAdapterNoThrow(uint index)
		=> GetAdapterTNoThrow<DXCoreAdapter, IDXCoreAdapter>(index);

	public DXCoreAdapter GetAdapter(uint index)
		=> GetAdapterNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<DXCoreAdapter> AdapterEnumerable
	{
		get
		{
			var c = AdapterCount;
			for (uint i = 0; i < c; i++)
				yield return GetAdapter(i);
		}
	}

	public ImmutableArray<DXCoreAdapter> Adapters
		=> [.. AdapterEnumerable];

	public bool IsScale
		=> _obj.IsStale();

	#region GetFactory

	public ComResult<object> GetFactoryNoThrow(in Guid iid)
		=> new(_obj.GetFactory(iid, out var x), x!);

	public object GetFactory(in Guid iid)
		=> GetFactoryNoThrow(iid).Value;

	public ComResult<TWrapper> GetFactoryTNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetFactory(typeof(TInterface).GUID, out var x), x);

	public TWrapper GetFactoryT<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetFactoryTNoThrow<TWrapper, TInterface>().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DXCoreAdapterFactory> FactoryNoThrow
		=> GetFactoryTNoThrow<DXCoreAdapterFactory, IDXCoreAdapterFactory>();

	public DXCoreAdapterFactory Factory
		=> FactoryNoThrow.Value;

	#endregion GetFactory

	public ComResult SortNoThrow(DXCoreAdapterPreference[] preferences)
		=> new(_obj.Sort((uint)preferences.Length, preferences));

	public void Sort(DXCoreAdapterPreference[] preferences)
		=> SortNoThrow(preferences).ThrowIfError();

	public bool IsAdapterPreferenceSupported(DXCoreAdapterPreference preference)
		=> _obj.IsAdapterPreferenceSupported(preference);
}
