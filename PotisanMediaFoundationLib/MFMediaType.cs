using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <summary>
/// MFメディア種類。IMFMediaType COMインターフェイスのラッパーです。
/// </summary>
public class MFMediaType(object? o) : ComUnknownWrapperWithMFAttribute<IMFMediaType>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> MajorTypeNoThrow
		=> new(_obj.GetMajorType(out var x), x);

	public Guid MajorType
		=> MajorTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsCompressedFormatNoThrow
		=> new(_obj.IsCompressedFormat(out var x), x);

	public bool IsCompressedFormat
		=> IsCompressedFormatNoThrow.Value;

	public ComResult<MFMediaTypeEqual> IsEqualNoThrow(MFMediaType other)
		=> new(_obj.IsEqual((IMFMediaType)other.WrappedObject!, out var x), (MFMediaTypeEqual)x);

	public MFMediaTypeEqual IsEqual(MFMediaType other)
		=> IsEqualNoThrow(other).Value;

	public sealed class Representation(MFMediaType owner, Guid guid, nint pointer) : IDisposable
	{
		public MFMediaType? Owner { get; private set; } = owner;
		public Guid Guid { get; private set; } = guid;
		public nint Pointer { get; private set; } = pointer;

		public void Dispose()
		{
			if (Owner == null) return;
			Owner._obj.FreeRepresentation(Guid, Pointer);
			Owner = null;
			Guid = new();
			Pointer = 0;
		}
	}

	public ComResult<Representation> GetRepresentationNoThrow(in Guid guid)
		=> new(_obj.GetRepresentation(guid, out var x), new(this, guid, x));

	public Representation GetRepresentation(in Guid guid)
		=> GetRepresentationNoThrow(guid).Value;

	public static ComResult<MFMediaType> CreateNoThrow()
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateMediaType(out IMFMediaType? ppMFType);

		return new(MFCreateMediaType(out var x), new(x));
	}

	public static MFMediaType Create()
		=> CreateNoThrow().Value;

	/// <summary>
	/// 属性のコピーを持つ新しいメディアタイプを作成します。
	/// </summary>
	public MFMediaType CreateWithSameAttributes()
	{
		var mf = Create();
		Attributes.CopyAllItemsTo(mf.Attributes);
		return mf;
	}

	/// <summary>
	/// RGB32形式用の属性のコピーを持つ新しいメディアタイプを作成します。
	/// </summary>
	public MFMediaType CreateForRgb32Bitmap()
	{
		var rgbMediaType = CreateWithSameAttributes();
		var (w, h) = rgbMediaType.Attributes.ForMediaType.FrameSize ?? throw new InvalidDataException();
		var rgbSize = w * h * 4;
		rgbMediaType.Attributes.ForMediaType.SubType = MFVideoSubTypeGuids.Rgb32;
		rgbMediaType.Attributes.ForMediaType.DefaultStride = w * 4;
		rgbMediaType.Attributes.ForMediaType.SampleSize = rgbSize;
		return rgbMediaType;
	}

	#region Common Attributes

	public Guid SubType => Attributes.ForMediaType.SubType ?? throw new InvalidDataException();
	public string VideoSubTypeName => MFVideoSubTypeGuids.GuidToString(SubType);

	#endregion
}

public enum MFMediaTypeEqual : uint
{
	MajorTypes = 0x00000001,
	FormatTypes = 0x00000002,
	FormatData = 0x00000004,
	FormatUserData = 0x00000008,
}