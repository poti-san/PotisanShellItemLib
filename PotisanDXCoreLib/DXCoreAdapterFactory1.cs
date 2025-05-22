using Potisan.Windows.Com;
using Potisan.Windows.DXCore.ComTypes;

namespace Potisan.Windows.DXCore;

/// <summary>
/// DXCoreのアダプタファクトリ（バージョン1.1）。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class DXCoreAdapterFactory1(object? o) : DXCoreAdapterFactory(o)
{
	private new readonly IDXCoreAdapterFactory1 _obj = o != null ? (IDXCoreAdapterFactory1)o : null!;

	public ComResult<TWrapper> CreateAdapterListByWorkloadTNoThrow<TWrapper, TInterface>(
		DXCoreWorkload workload,
		DXCoreRuntimeFilterFlags runtimeFilter,
		DXCoreHardwareTypeFilterFlags hardwareTypeFilter)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.CreateAdapterListByWorkload(
			workload, runtimeFilter, hardwareTypeFilter, typeof(TInterface).GUID, out var x), x);

	public TWrapper CreateAdapterListByWorkloadT<TWrapper, TInterface>(
		DXCoreWorkload workload,
		DXCoreRuntimeFilterFlags runtimeFilter,
		DXCoreHardwareTypeFilterFlags hardwareTypeFilter)
		where TWrapper : IComUnknownWrapper
		=> CreateAdapterListByWorkloadTNoThrow<TWrapper, TInterface>(workload, runtimeFilter, hardwareTypeFilter).Value;

	public ComResult<DXCoreAdapterList> CreateAdapterListByWorkloadNoThrow(
		DXCoreWorkload workload,
		DXCoreRuntimeFilterFlags runtimeFilter,
		DXCoreHardwareTypeFilterFlags hardwareTypeFilter)
		=> CreateAdapterListByWorkloadTNoThrow<DXCoreAdapterList, IDXCoreAdapterList>(workload, runtimeFilter, hardwareTypeFilter);

	public DXCoreAdapterList CreateAdapterListByWorkload(
		DXCoreWorkload workload,
		DXCoreRuntimeFilterFlags runtimeFilter,
		DXCoreHardwareTypeFilterFlags hardwareTypeFilter)
		=> CreateAdapterListByWorkloadNoThrow(workload, runtimeFilter, hardwareTypeFilter).Value;

	public new static ComResult<DXCoreAdapterFactory1> CreateNoThrow()
		=> CreateTNoThrow<DXCoreAdapterFactory1, IDXCoreAdapterFactory1>();

	public new static DXCoreAdapterFactory1 Create()
		=> CreateNoThrow().Value;
}
