using Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

namespace Potisan.Windows.MediaFoundation.MediaEngine;

public class MFMediaError(object? o) : ComUnknownWrapperBase<IMFMediaError>(o)
{
	public MFMediaEngineError ErrorCode
	{
		get => (MFMediaEngineError)_obj.GetErrorCode();
		set => SetErrorCodeNoThrow(value).ThrowIfError();
	}

	public int ExtendedErrorCode
	{
		get => _obj.GetExtendedErrorCode();
		set => SetExtendedErrorCodeNoThrow(value).ThrowIfError();
	}

	public ComResult SetErrorCodeNoThrow(MFMediaEngineError error)
		=> new(_obj.SetErrorCode(error));

	public ComResult SetExtendedErrorCodeNoThrow(int error)
		=> new(_obj.SetExtendedErrorCode(error));
}

/// <summary>
/// MF_MEDIA_ENGINE_ERR
/// </summary>
public enum MFMediaEngineError : ushort
{
	NoError = 0,
	Aborted = 1,
	Network = 2,
	Decode = 3,
	SourceNotsupported = 4,
	Encrypted = 5,
}
