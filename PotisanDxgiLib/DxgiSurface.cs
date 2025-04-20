using Potisan.Windows.Com;
using Potisan.Windows.Dxgi.ComTypes;

namespace Potisan.Windows.Dxgi;

public class DxgiSurface(object? o) : ComUnknownWrapperBase<IDXGISurface>(o)
{
	public DxgiDeviceSubObject DxgiDeviceSubOejct { get; } = new(o);

	public ComResult<DxgiSurfaceDesc> DescriptionNoThrow
		=> new(_obj.GetDesc(out var x), x);

	public DxgiSurfaceDesc Description
		=> DescriptionNoThrow.Value;

	public ComResult<DxgiMappedRect> MapNoThrow(DxgiMap flags)
		=> new(_obj.Map(out var x, (uint)flags), x);

	public DxgiMappedRect Map(DxgiMap flags)
		=> MapNoThrow(flags).Value;

	public sealed class DxgiSurfaceMapped : IDisposable
	{
		private WeakReference<DxgiSurface>? _surface;
		public DxgiSurface? Surface => _surface != null ? (_surface.TryGetTarget(out var x) ? x : throw new InvalidDataException()) : null;
		public DxgiMappedRect? MappedRect { get; private set; }

		internal DxgiSurfaceMapped(DxgiSurface surface, DxgiMappedRect mappedRect)
		{
			_surface = new(surface);
			MappedRect = mappedRect;
		}

		~DxgiSurfaceMapped()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!MappedRect.HasValue) return;

			Surface?._obj.Unmap();
			_surface = null;
			MappedRect = null;

			GC.SuppressFinalize(this);
		}

		public void DangerousDetach()
		{
			_surface = null;
			MappedRect = null;

#pragma warning disable CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります
			GC.SuppressFinalize(this);
#pragma warning restore CA1816
		}
	}
}

/// <summary>
/// DXGI_MAP_*
/// </summary>
[Flags]
public enum DxgiMap : uint
{
	Read = 1,
	Write = 2,
	Discard = 4,
}