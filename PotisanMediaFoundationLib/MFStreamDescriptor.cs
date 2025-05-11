using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <summary>
/// MFストリーム記述子。IMFStreamDescriptor COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class MFStreamDescriptor(object? o) : ComUnknownWrapperWithMFAttribute<IMFStreamDescriptor>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> StreamIDNoThrow => new(_obj.GetStreamIdentifier(out var x), x);

	public uint StreamID => StreamIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFMediaTypeHandler> MediaTypeHandlerNoThrow => new(_obj.GetMediaTypeHandler(out var x), new(x));

	public MFMediaTypeHandler MediaTypeHandler => MediaTypeHandlerNoThrow.Value;

	public static ComResult<MFStreamDescriptor> CreateNoThrow(uint streamId, MFMediaType[] mediaTypes)
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateStreamDescriptor(
			uint dwStreamIdentifier,
			uint cMediaTypes,
			[MarshalAs(UnmanagedType.LPArray)] IMFMediaType[] apMediaTypes,
			out IMFStreamDescriptor? ppDescriptor);

		var t = mediaTypes.Select(mediaType => (IMFMediaType)mediaType.WrappedObject!).ToArray();
		return new(MFCreateStreamDescriptor(streamId, unchecked((uint)mediaTypes.Length), t, out var x), new(x));
	}

	public static MFStreamDescriptor Create(uint streamId, MFMediaType[] mediaTypes)
		=> CreateNoThrow(streamId, mediaTypes).Value;
}
