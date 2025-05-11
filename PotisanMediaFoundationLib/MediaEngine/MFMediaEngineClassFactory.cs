using Potisan.Windows.MediaFoundation.ComTypes;
using Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

namespace Potisan.Windows.MediaFoundation.MediaEngine;

public class MFMediaEngineClassFactory(object? o) : ComUnknownWrapperBase<IMFMediaEngineClassFactory>(o)
{
	public static ComResult<MFMediaEngineClassFactory> CreateNoThrow()
	{
		Guid CLSID_MFMediaEngineClassFactory = new(0xb44392da, 0x499b, 0x446b, 0xa4, 0xcb, 0x0, 0x5f, 0xea, 0xd0, 0xe6, 0xd5);
		return ComHelper.CreateInstanceNoThrow<MFMediaEngineClassFactory, IMFMediaEngineClassFactory>(CLSID_MFMediaEngineClassFactory);
	}

	public static MFMediaEngineClassFactory Create()
		=> CreateNoThrow().Value;

	public ComResult<MFMediaEngine> CreateInstanceNoThrow(
		MFMediaEngineNotify notify,
		MFMediaEngineCreateFlag flags = 0,
		MFAttributes? attrs = null)
	{
		if (attrs != null)
		{
			attrs = attrs.Clone();
		}
		else
		{
			var cr = MFAttributes.CreateNoThrow(1);
			if (!cr) return new(cr.HResult, new(null));
			attrs = cr.ValueUnchecked;
		}
		attrs.ForMediaEngine.Callback = notify;

		return new(_obj.CreateInstance((uint)flags, (IMFAttributes)attrs.WrappedObject!, out var x), new(x));
	}

	public MFMediaEngine CreateInstance(
		MFMediaEngineNotify notify,
		MFMediaEngineCreateFlag flags = 0,
		MFAttributes? attrs = null)
		=> CreateInstanceNoThrow(notify, flags, attrs).Value;

	public ComResult<MFMediaTimeRange> CreateTimeRangeNoThrow()
		=> new(_obj.CreateTimeRange(out var x), new(x));

	public MFMediaTimeRange CreateTimeRange()
		=> CreateTimeRangeNoThrow().Value;

	public ComResult<MFMediaError> CreateErrorNoThrow()
		=> new(_obj.CreateError(out var x), new(x));

	public MFMediaError CreateError()
		=> CreateErrorNoThrow().Value;
}

/// <summary>
/// MF_MEDIA_ENGINE_CREATEFLAGS
/// </summary>
public enum MFMediaEngineCreateFlag : uint
{
	AudioOnly = 0x1,
	WaitForStableState = 0x2,
	ForceMute = 0x4,
	RealTimeMode = 0x8,
	DisableLocalPlugins = 0x10,
}
