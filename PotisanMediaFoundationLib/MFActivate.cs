using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <summary>
/// MFアクティベータ。IMFActivate COMインターフェイスのラッパーです。
/// </summary>
public class MFActivate(object? o) : ComUnknownWrapperWithMFAttribute<IMFActivate>(o)
{
	public bool ShutdownsOnDelete { get; set; } = true;

	~MFActivate()
	{
		if (ShutdownsOnDelete)
			_obj.ShutdownObject();
	}

#pragma warning disable CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります
	/// <inheritdoc/>
	public override void Dispose()
	{
		base.Dispose();

		if (_obj == null) return;
		if (ShutdownsOnDelete) _obj.ShutdownObject();
	}
#pragma warning restore CA1816 // Dispose メソッドは、SuppressFinalize を呼び出す必要があります

	public ComResult<TWrapper> ActivateObjectNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.ActivateObject(typeof(TInterface).GUID, out var x), x);

	public void ActivateObject<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> ActivateObjectNoThrow<TWrapper, TInterface>().ThrowIfError();

	public ComResult ShutdownObjectNoThrow()
		=> new(_obj.ShutdownObject());

	public void ShutdownObject()
		=> ShutdownObjectNoThrow().ThrowIfError();

	public ComResult DetachObjectNoThrow()
		=> new(_obj.DetachObject());

	public void DetachObject()
		=> DetachObjectNoThrow().ThrowIfError();

	public static ComResult<MFActivate[]> GetDeviceSourcesNoThrow(MFAttributes attrs)
	{
		[DllImport("mf.dll")]
		static extern int MFEnumDeviceSources(IMFAttributes pAttributes, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out IMFActivate[] pppSourceActivate, out uint pcSourceActivate);

		var hr = MFEnumDeviceSources((IMFAttributes)attrs.WrappedObject!, out var x, out _);
		if (hr != 0) return new(hr, []);

		return new(hr, [.. x.Select(p => new MFActivate(p))]);
	}

	public static MFActivate[] GetDeviceSources(MFAttributes attrs) => GetDeviceSourcesNoThrow(attrs).Value;

	public static ComResult<MFActivate[]> GetVideoDeviceSourcesNoThrow(MFAttributes? attrs = null)
	{
		try
		{
			attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(1);
			var srcDevAttrs = new MFDeviceSourceAttributes(attrs)
			{
				SourceType = MFSourceTypeGuids.VideoCapture
			};
			return GetDeviceSourcesNoThrow(attrs);
		}
		catch (Exception ex)
		{
			return new(ex.HResult, []);
		}
	}

	public static MFActivate[] GetVideoDeviceSources(MFAttributes? attrs = null)
		=> GetVideoDeviceSourcesNoThrow(attrs).Value;

	public static ComResult<MFActivate[]> GetVideoInputDeviceSourcesNoThrow(MFAttributes? attrs = null)
	{
		try
		{
			attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(2);
			var srcDevAttrs = new MFDeviceSourceAttributes(attrs)
			{
				SourceType = MFSourceTypeGuids.VideoCapture,
				VideoCaptureCategory = MFVideoCaptureCategoryClsids.VideoInputDeviceCategory
			};
			return GetDeviceSourcesNoThrow(attrs);
		}
		catch (Exception ex)
		{
			return new(ex.HResult, []);
		}
	}

	public static MFActivate[] GetVideoInputDeviceSources(MFAttributes? attrs = null)
		=> GetVideoInputDeviceSourcesNoThrow(attrs).Value;

	public static ComResult<MFActivate[]> GetAudioDeviceSourcesNoThrow(MFRole role, MFAttributes? attrs = null)
	{
		try
		{
			attrs = attrs != null ? attrs.Clone() : MFAttributes.Create(2);
			var srcDevAttrs = new MFDeviceSourceAttributes(attrs)
			{
				SourceType = MFSourceTypeGuids.VideoCapture,
				SourceTypeAudioCaptureRole = role
			};
			return GetDeviceSourcesNoThrow(attrs);
		}
		catch (Exception ex)
		{
			return new(ex.HResult, []);
		}
	}

	public static MFActivate[] GetAudioDeviceSources(MFRole role, MFAttributes? attrs = null)
		=> GetAudioDeviceSourcesNoThrow(role, attrs).Value;

	public ComResult<MFMediaSource> ActivateMediaSourceNoThrow() => ActivateObjectNoThrow<MFMediaSource, IMFMediaSource>();

	public MFMediaSource ActivateMediaSource() => ActivateMediaSourceNoThrow().Value;
}
// TODO
//TDAPI MFCreateDeviceSourceActivate(
//	_In_ IMFAttributes*   pAttributes,
//	_Outptr_ IMFActivate**   ppActivate
//);
