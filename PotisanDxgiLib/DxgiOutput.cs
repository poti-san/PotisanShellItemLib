using Potisan.Windows.Com;
using Potisan.Windows.Dxgi.ComTypes;

namespace Potisan.Windows.Dxgi;

/// <summary>
/// アダプター出力デバイス（モニター等）。
/// </summary>
/// <param name="o"></param>
public class DxgiOutput(object? o) : ComUnknownWrapperBase<IDXGIOutput>(o)
{
	public DxgiObject DxgiObject { get; } = new(o);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DxgiOutputDesc> DescriptionNoThrow
		=> new(_obj.GetDesc(out var x), x);

	public DxgiOutputDesc Description
		=> DescriptionNoThrow.Value;

	public ComResult<DxgiModeDesc[]> GetDisplayModeListNoThrow(DxgiFormat format, DxgiEnumMode mode)
	{
		uint c = 0;
		_obj.GetDisplayModeList(format, (uint)mode, ref c, null);

		var buffer = new DxgiModeDesc[c];
		return new(_obj.GetDisplayModeList(format, (uint)mode, ref c, buffer), buffer);
	}

	public DxgiModeDesc[] GetDisplayModeList(DxgiFormat format, DxgiEnumMode mode)
		=> GetDisplayModeListNoThrow(format, mode).Value;

	public ComResult<DxgiModeDesc> FindClosestMatchingModeNoThrow(in DxgiModeDesc modeToMatch, object? concernedDevice = null)
		=> new(_obj.FindClosestMatchingMode(modeToMatch, out var x, concernedDevice), x);

	public DxgiModeDesc FindClosestMatchingMode(in DxgiModeDesc modeToMatch, object? concernedDevice = null)
		=> FindClosestMatchingModeNoThrow(modeToMatch, concernedDevice).Value;

	public ComResult<DxgiModeDesc> FindClosestMatchingModeNoThrow(in DxgiModeDesc modeToMatch, IComUnknownWrapper? concernedDevice)
		=> FindClosestMatchingModeNoThrow(modeToMatch, concernedDevice?.WrappedObject);

	public DxgiModeDesc FindClosestMatchingMode(in DxgiModeDesc modeToMatch, IComUnknownWrapper? concernedDevice)
		=> FindClosestMatchingModeNoThrow(modeToMatch, concernedDevice).Value;

	public ComResult WaitForVBlankNoThrow()
		=> new(_obj.WaitForVBlank());

	public void WaitForVBlank()
		=> WaitForVBlankNoThrow().ThrowIfError();

	public ComResult TakeOwnershipNoThrow(object device, bool exclusive)
		=> new(_obj.TakeOwnership(device, exclusive));

	public void TakeOwnership(object device, bool exclusive)
		=> TakeOwnershipNoThrow(device, exclusive).ThrowIfError();

	public ComResult TakeOwnershipNoThrow(IComUnknownWrapper device, bool exclusive)
		=> new(_obj.TakeOwnership(device.WrappedObject!, exclusive));

	public void TakeOwnership(IComUnknownWrapper device, bool exclusive)
		=> TakeOwnershipNoThrow(device, exclusive).ThrowIfError();

	public void ReleaseOwnership()
		=> _obj.ReleaseOwnership();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DxgiGammaControlCapabilities> GammaControlCapabilitiesNoThrow
		=> new(_obj.GetGammaControlCapabilities(out var x), x);

	public DxgiGammaControlCapabilities GammaControlCapabilities
		=> GammaControlCapabilitiesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DxgiGammaControl> GammaControlNoThrow
		=> new(_obj.GetGammaControl(out var x), x);

	public ComResult SetGammaControlNoThrow(in DxgiGammaControl array)
		=> new(_obj.SetGammaControl(array));

	public DxgiGammaControl GammaControl
	{
		get => GammaControlNoThrow.Value;
		set => SetGammaControlNoThrow(value).ThrowIfError();
	}

	/// <summary>
	/// 通常は直接呼出しません。スワップチェーンの中で呼び出されます。
	/// </summary>
	/// <returns></returns>
	public ComResult SetDisplaySurfaceNoThrow(DxgiSurface scanoutSurface)
		=> new(_obj.SetDisplaySurface((IDXGISurface)scanoutSurface.WrappedObject!));

	/// <inheritdoc cref="SetDisplaySurfaceNoThrow(DxgiSurface)"/>
	public void SetDisplaySurface(DxgiSurface scanoutSurface)
		=> SetDisplaySurfaceNoThrow(scanoutSurface).ThrowIfError();

	/// <summary>
	/// 現在の表示サーフェイスのコピーを取得します。
	/// Direct3D 11.1以降では非推奨です。ステレオ表示モードをサポートする<c>IDXGIOutput1::GetDisplaySurfaceData1</c>を使用してください。
	/// </summary>
	public ComResult GetDisplaySurfaceDataNoThrow(DxgiSurface destination)
		=> new(_obj.GetDisplaySurfaceData((IDXGISurface)destination.WrappedObject!));

	/// <inheritdoc cref="GetDisplaySurfaceDataNoThrow(DxgiSurface)"/>
	public void GetDisplaySurfaceData(DxgiSurface destination)
		=> GetDisplaySurfaceDataNoThrow(destination).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DxgiFrameStatistics> FrameStatisticsNoThrow
		=> new(_obj.GetFrameStatistics(out var x), x);

	public DxgiFrameStatistics FrameStatistics
		=> FrameStatisticsNoThrow.Value;
}
