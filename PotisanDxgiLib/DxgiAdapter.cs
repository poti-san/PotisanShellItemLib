using Potisan.Windows.Com;
using Potisan.Windows.Dxgi.ComTypes;

namespace Potisan.Windows.Dxgi;

/// <summary>
/// ディスプレイサブシステム（GPU、DAC、ビデオメモリ）。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.Dxgi;
///
///var dxgiFactory = DxgiFactory.Create();
///
///foreach (var adapter in dxgiFactory.AdapterEnumerable)
///{
///	Console.WriteLine(adapter.Description.Description);
///}]]>
/// </code>
/// </example>
public class DxgiAdapter(object? o) : ComUnknownWrapperBase<IDXGIAdapter>(o)
{
	public DxgiObject DxgiObject { get; } = new(o);

	public ComResult<DxgiOutput> GetOutputNoThrow(uint index)
		=> new(_obj.EnumOutputs(index, out var x), new(x));

	public DxgiOutput GetOutput(uint index)
		=> GetOutputNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<DxgiOutput> OutputEnumerable
	{
		get
		{
			const int DXGI_ERROR_NOT_FOUND = unchecked((int)0x887A0002);
			for (uint i = 0; ; i++)
			{
				var cr = GetOutputNoThrow(i);
				if (cr.HResult == DXGI_ERROR_NOT_FOUND) break;
				cr.ThrowIfError();
				yield return cr.ValueUnchecked;
			}
		}
	}

	public DxgiOutput[] Outputs
		=> [.. OutputEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DxgiAdapterDesc> DescriptionNoThrow
		=> new(_obj.GetDesc(out var x), x);

	public DxgiAdapterDesc Description
		=> DescriptionNoThrow.Value;

	public ComResult<long> CheckInterfaceSupportNoThrow(in Guid interfaceName)
		=> new(_obj.CheckInterfaceSupport(interfaceName, out var x), x);

	public long CheckInterfaceSupport(in Guid interfaceName)
		=> CheckInterfaceSupportNoThrow(interfaceName).Value;
}
