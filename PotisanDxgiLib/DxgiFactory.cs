using Potisan.Windows.Com;
using Potisan.Windows.Dxgi.ComTypes;

namespace Potisan.Windows.Dxgi;

/// <summary>
/// DXGIオブジェクトの生成オブジェクト。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class DxgiFactory(object? o) : ComUnknownWrapperBase<IDXGIFactory>(o)
{
	public DxgiObject DxgiObject { get; } = new(o);

	public static ComResult<DxgiFactory> CreateNoThrow()
	{
		[DllImport("dxgi.dll")]
		static extern int CreateDXGIFactory(in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppFactory);

		return new(CreateDXGIFactory(typeof(IDXGIFactory).GUID, out var x), new(x));
	}

	public static DxgiFactory Create()
		=> CreateNoThrow().Value;

	public ComResult<DxgiAdapter> GetAdapterNoThrow(uint index)
		=> new(_obj.EnumAdapters(index, out var x), new(x));

	public DxgiAdapter GetAdapter(uint index)
		=> GetAdapterNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<DxgiAdapter> AdapterEnumerable
	{
		get
		{
			const int DXGI_ERROR_NOT_FOUND = unchecked((int)0x887A0002);
			for (uint i = 0; ; i++)
			{
				var cr = GetAdapterNoThrow(i);
				if (cr.HResult == DXGI_ERROR_NOT_FOUND) break;
				cr.ThrowIfError();
				yield return cr.ValueUnchecked;
			}
		}
	}

	public DxgiAdapter[] Adapters
		=> [.. AdapterEnumerable];

	public ComResult MakeWindowAssociationNoThrow(nint windowHandle, DxgiMakeWindowAssociation flags)
		=> new(_obj.MakeWindowAssociation(windowHandle, (uint)flags));

	public void MakeWindowAssociation(nint windowHandle, DxgiMakeWindowAssociation flags)
		=> MakeWindowAssociationNoThrow(windowHandle, flags).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<nint> WindowAssociationNoThrow
		=> new(_obj.GetWindowAssociation(out var x), x);

	public nint WindowAssociation
		=> WindowAssociationNoThrow.Value;

	public ComResult<DxgiSwapChain> CreateSwapChainNoThrow(object device, in DxgiSwapChainDescription desc)
		=> new(_obj.CreateSwapChain(device, desc, out var x), new(x));

	public DxgiSwapChain CreateSwapChain(object device, in DxgiSwapChainDescription desc)
		=> CreateSwapChainNoThrow(device, desc).Value;

	public ComResult<DxgiSwapChain> CreateSwapChainNoThrow(IComUnknownWrapper device, in DxgiSwapChainDescription desc)
		=> CreateSwapChainNoThrow(device.WrappedObject!, desc);

	public DxgiSwapChain CreateSwapChain(IComUnknownWrapper device, in DxgiSwapChainDescription desc)
		=> CreateSwapChainNoThrow(device, desc).Value;

	public ComResult<DxgiAdapter> CreateSoftwareAdapterNoThrow(nint moduleHandle)
		=> new(_obj.CreateSoftwareAdapter(moduleHandle, out var x), new(x));

	public DxgiAdapter CreateSoftwareAdapter(nint moduleHandle)
		=> CreateSoftwareAdapterNoThrow(moduleHandle).Value;
}

/// <summary>
/// DXGI_MWA_*
/// </summary>
[Flags]
public enum DxgiMakeWindowAssociation : uint
{
	NoWindowChanges = 1 << 0,
	NoAltEnter = 1 << 1,
	NoPrintScreen = 1 << 2,
}
