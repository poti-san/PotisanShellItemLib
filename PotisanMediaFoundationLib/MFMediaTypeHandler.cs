using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <summary>
/// MFメディア種類ハンドラ。IMFMediaTypeHandler COMインターフェイスのラッパーです。
/// </summary>
public class MFMediaTypeHandler(object? o) : ComUnknownWrapperBase<IMFMediaTypeHandler>(o)
{
	public ComResult<bool> IsMediaTypeSupportedNoThrow(MFMediaType mediaType)
		=> ComResult.HRSuccess(
			_obj.IsMediaTypeSupported((IMFMediaType)mediaType.WrappedObject!,
			out Unsafe.NullRef<IMFMediaType?>()));

	public bool IsMediaTypeSupported(MFMediaType mediaType)
		=> IsMediaTypeSupportedNoThrow(mediaType).Value;

	public ComResult<MFMediaType> GetNearestMediaTypeNoThrow(MFMediaType mediaType)
		=> new(_obj.IsMediaTypeSupported((IMFMediaType)mediaType.WrappedObject!, out var x), new(x));

	public MFMediaType GetNearestMediaType(MFMediaType mediaType)
		=> GetNearestMediaTypeNoThrow(mediaType).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> MediaTypeCountNoThrow
		=> new(_obj.GetMediaTypeCount(out var x), x);

	public uint Count
		=> MediaTypeCountNoThrow.Value;

	public ComResult<MFMediaType> GetMediaTypeAtNoThrow(uint index)
		=> new(_obj.GetMediaTypeByIndex(index, out var x), new(x));

	public MFMediaType GetMediaTypeAt(uint index)
		=> GetMediaTypeAtNoThrow(index).Value;

	public IEnumerable<MFMediaType> EnumerateMediaTypes()
	{
		var c = Count;
		for (uint i = 0; i < c; i++)
			yield return GetMediaTypeAt(i);
	}

	public ImmutableArray<MFMediaType> MediaTypes
		=> [.. EnumerateMediaTypes()];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaType> CurrentMediaTypeNoThrow
		=> new(_obj.GetCurrentMediaType(out var x), new(x));

	public ComResult SetCurrentMediaTypeNoThrow(MFMediaType mediaType)
		=> new(_obj.SetCurrentMediaType((IMFMediaType)mediaType.WrappedObject!));

	public MFMediaType CurrentMediaType
	{
		get => CurrentMediaTypeNoThrow.Value;
		set => SetCurrentMediaTypeNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> MajorTypeNoThrow
		=> new(_obj.GetMajorType(out var x), x);

	public Guid MajorType
		=> MajorTypeNoThrow.Value;
}
