#pragma warning disable CA1707 // 識別子はアンダースコアを含むことはできません

using System.Diagnostics;

namespace Potisan.Windows.MediaFoundation;

public sealed class MFSourceReaderPresentationAttributes(MFSourceReader sourceReader, uint streamIndex)
{
	public MFSourceReader SourceReader { get; } = sourceReader;
	public uint StreamIndex { get; } = streamIndex;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> LanguageNoThrow
	{
		get
		{
			var cr = SourceReader.GetPresentationAttributeNoThrow(StreamIndex, MFSourceReaderPresentationAttributeGuids.MF_SD_LANGUAGE);
			return new(CommonHResults.SOK, cr.ValueUnchecked.GetStringUniWithDefault(""));
		}
	}
	public string Language => LanguageNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> ProtectedNoThrow
	{
		get
		{
			var cr = SourceReader.GetPresentationAttributeNoThrow(StreamIndex, MFSourceReaderPresentationAttributeGuids.MF_SD_PROTECTED);
			return new(CommonHResults.SOK, cr.ValueUnchecked.GetBoolWithDefault(false));
		}
	}
	public bool Protected => ProtectedNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> StreamNameNoThrow
	{
		get
		{
			var cr = SourceReader.GetPresentationAttributeNoThrow(StreamIndex, MFSourceReaderPresentationAttributeGuids.MF_SD_STREAM_NAME);
			return new(CommonHResults.SOK, cr.ValueUnchecked.GetStringUniWithDefault(""));
		}
	}
	public string StreamName => StreamNameNoThrow.Value;

	//public static Guid MF_SD_MUTUALLY_EXCLUSIVE => new(0x23ef79c, 0x388d, 0x487f, 0xac, 0x17, 0x69, 0x6c, 0xd6, 0xe3, 0xc6, 0xf5);
	//public static Guid MF_SD_SUPPORTS_PROTECTED_CODEC_SWITCH => new(0x8fb6b117, 0x862e, 0x4b31, 0x8d, 0xab, 0x5e, 0x0a, 0x43, 0x4c, 0xae, 0xf0);
}

public static class MFSourceReaderPresentationAttributeGuids
{
	public static Guid MF_SD_LANGUAGE => new(0xaf2180, 0xbdc2, 0x423c, 0xab, 0xca, 0xf5, 0x3, 0x59, 0x3b, 0xc1, 0x21);
	public static Guid MF_SD_PROTECTED => new(0xaf2181, 0xbdc2, 0x423c, 0xab, 0xca, 0xf5, 0x3, 0x59, 0x3b, 0xc1, 0x21);
	public static Guid MF_SD_STREAM_NAME => new(0x4f1b099d, 0xd314, 0x41e5, 0xa7, 0x81, 0x7f, 0xef, 0xaa, 0x4c, 0x50, 0x1f);
	public static Guid MF_SD_MUTUALLY_EXCLUSIVE => new(0x23ef79c, 0x388d, 0x487f, 0xac, 0x17, 0x69, 0x6c, 0xd6, 0xe3, 0xc6, 0xf5);
	public static Guid MF_SD_SUPPORTS_PROTECTED_CODEC_SWITCH => new(0x8fb6b117, 0x862e, 0x4b31, 0x8d, 0xab, 0x5e, 0x0a, 0x43, 0x4c, 0xae, 0xf0);
}