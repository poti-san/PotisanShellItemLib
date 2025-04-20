using Potisan.Windows.MediaFoundation.Async.ComTypes;
using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFSampleOutputStream(object? o) : ComUnknownWrapperBase<IMFSampleOutputStream>(o)
{
	public ComResult BeginWriteSampleNoThrow(MFSample sample, IMFAsyncCallback? callback = null, object? unkState = null)
		=> new(_obj.BeginWriteSample((IMFSample)sample.WrappedObject!, callback, unkState));

	public void BeginWriteSample(MFSample sample, IMFAsyncCallback? callback = null, object? unkState = null)
		=> BeginWriteSampleNoThrow(sample, callback, unkState).ThrowIfError();

	public ComResult EndWriteSampleNoThrow(IMFAsyncResult result)
		=> new(_obj.EndWriteSample(result));

	public void EndWriteSample(IMFAsyncResult result)
		=> EndWriteSampleNoThrow(result).ThrowIfError();

	public ComResult CloseNoThrow()
		=> new(_obj.Close());

	public void Close()
		=> CloseNoThrow().ThrowIfError();
}
