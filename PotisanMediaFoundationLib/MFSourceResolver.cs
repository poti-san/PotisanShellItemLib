using Potisan.Windows.MediaFoundation.Async;
using Potisan.Windows.MediaFoundation.Async.ComTypes;
using Potisan.Windows.MediaFoundation.ComTypes;
using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFSourceResolver(object? o) : ComUnknownWrapperBase<IMFSourceResolver>(o)
{
	public static ComResult<MFSourceResolver> CreateNoThrow()
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateSourceResolver(out IMFSourceResolver ppISourceResolver);

		return new(MFCreateSourceResolver(out var x), new(x));
	}

	public static MFSourceResolver Create()
		=> CreateNoThrow().Value;

	public ComResult<MFMediaSource> CreateMediaSourceFromUrlNoThrow(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_MEDIASOURCE = 0x1;
		if (((uint)resolution & 0x3) is not 0 or MF_RESOLUTION_MEDIASOURCE)
			return new(CommonHResults.EInvalidArg, new(null));
		resolution |= (MFResolutionFlag)MF_RESOLUTION_MEDIASOURCE;

		var hr = _obj.CreateObjectFromURL(
			url,
			(uint)resolution,
			(IPropertyStore?)propStore?.WrappedObject,
			out _,
			out var x);
		return new(hr, new(x));
	}

	public MFMediaSource CreateMediaSourceFromUrl(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
		=> CreateMediaSourceFromUrlNoThrow(url, resolution, propStore).Value;

	public ComResult<MFByteStream> CreateByteStreamFromUrlNoThrow(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_BYTESTREAM = 0x2;
		if (((uint)resolution & 0x3) is not 0 or MF_RESOLUTION_BYTESTREAM)
			return new(CommonHResults.EInvalidArg, new(null));
		resolution |= (MFResolutionFlag)MF_RESOLUTION_BYTESTREAM;

		var hr = _obj.CreateObjectFromURL(
			url,
			(uint)resolution,
			(IPropertyStore?)propStore?.WrappedObject,
			out _,
			out var x);
		return new(hr, new(x));
	}

	public MFByteStream CreateByteStreamFromUrl(
		string url,
		MFResolutionFlag resolution = MFResolutionFlag.Read,
		PropertyStore? propStore = null)
		=> CreateByteStreamFromUrlNoThrow(url, resolution, propStore).Value;

	public ComResult<MFMediaSource> CreateMediaSourceFromByteStreamNoThrow(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_MEDIASOURCE = 0x1;
		if (((uint)resolution & 0x3) is not 0 or MF_RESOLUTION_MEDIASOURCE)
			return new(CommonHResults.EInvalidArg, new(null));
		resolution |= (MFResolutionFlag)MF_RESOLUTION_MEDIASOURCE;

		var hr = _obj.CreateObjectFromByteStream(
			null,
			url,
			(uint)resolution,
			(IPropertyStore?)propStore?.WrappedObject,
			out _,
			out var x);
		return new(hr, new(x));
	}

	public MFMediaSource CreateMediaSourceFromByteStream(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
		=> CreateMediaSourceFromByteStreamNoThrow(url, resolution, propStore).Value;

	public ComResult<MFMediaSource> CreateMediaSourceFromByteStreamNoThrow(
		MFByteStream stream,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_MEDIASOURCE = 0x1;
		if (((uint)resolution & 0x3) is not 0 or MF_RESOLUTION_MEDIASOURCE)
			return new(CommonHResults.EInvalidArg, new(null));
		resolution |= (MFResolutionFlag)MF_RESOLUTION_MEDIASOURCE;

		var hr = _obj.CreateObjectFromByteStream(
			(IMFByteStream)stream.WrappedObject!,
			null,
			(uint)resolution,
			(IPropertyStore?)propStore?.WrappedObject,
			out _,
			out var x);
		return new(hr, new(x));
	}

	public MFMediaSource CreateMediaSourceFromByteStream(
		MFByteStream stream,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
		=> CreateMediaSourceFromByteStreamNoThrow(stream, resolution, propStore).Value;

	public ComResult<MFMediaSource> CreateByteStreamFromByteStreamNoThrow(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_BYTESTREAM = 0x2;
		if (((uint)resolution & 0x3) is not 0 or MF_RESOLUTION_BYTESTREAM)
			return new(CommonHResults.EInvalidArg, new(null));
		resolution |= (MFResolutionFlag)MF_RESOLUTION_BYTESTREAM;

		var hr = _obj.CreateObjectFromByteStream(
			null,
			url,
			(uint)resolution,
			(IPropertyStore?)propStore?.WrappedObject,
			out _,
			out var x);
		return new(hr, new(x));
	}

	public MFMediaSource CreateByteStreamFromByteStream(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
		=> CreateByteStreamFromByteStreamNoThrow(url, resolution, propStore).Value;

	public ComResult<MFMediaSource> CreateByteStreamFromByteStreamNoThrow(
		MFByteStream stream,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_BYTESTREAM = 0x2;
		if (((uint)resolution & 0x3) is not 0 or MF_RESOLUTION_BYTESTREAM)
			return new(CommonHResults.EInvalidArg, new(null));
		resolution |= (MFResolutionFlag)MF_RESOLUTION_BYTESTREAM;

		var hr = _obj.CreateObjectFromByteStream(
			(IMFByteStream)stream.WrappedObject!,
			null,
			(uint)resolution,
			(IPropertyStore?)propStore?.WrappedObject,
			out _,
			out var x);
		return new(hr, new(x));
	}

	public MFMediaSource CreateByteStreamFromByteStream(
		MFByteStream stream,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
		=> CreateByteStreamFromByteStreamNoThrow(stream, resolution, propStore).Value;

	// キャンセル不可版
	// TODO: タスクは不慣れなので、適切なコードか振り返る。
	private Task<TWrapper> CreateObjectFromUrlAsync<TWrapper, TInterface>(
		uint mediaTypeFlag,
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
		where TWrapper : IComUnknownWrapper
		where TInterface : class
	{
		var mediaType = (uint)resolution & 0x3;
		if (mediaType != 0 && mediaType != mediaTypeFlag)
			Marshal.ThrowExceptionForHR(CommonHResults.EInvalidArg);
		resolution |= (MFResolutionFlag)mediaTypeFlag;

		return Task<TWrapper>.Run(() =>
		{
			var x = default(object);
			var wait = new EventWaitHandle(false, EventResetMode.ManualReset);
			var hr = _obj.BeginCreateObjectFromURL(
				url,
				(uint)resolution,
				propStore?.WrappedObject as IPropertyStore,
				out Unsafe.NullRef<nint>(),
				new MFAsyncCallbackStatic(0, MFAsyncCallbackQueue.MultiThreaded, result =>
				{
					Marshal.ThrowExceptionForHR(_obj.EndCreateObjectFromURL(
							(IMFAsyncResult)result.WrappedObject!,
							out var ot,
							out x));
					wait.Set();
				}),
				null);
			Marshal.ThrowExceptionForHR(hr);
			try
			{
				wait.WaitOne();
			}
			catch (AggregateException ex)
			{
				throw (ex?.InnerException ?? ex)!;
			}
			return IComUnknownWrapper.Casted<TWrapper, TInterface>(x!).Value;
		});
	}

	// キャンセル可版
	// TODO: タスクは不慣れなので、適切なコードか振り返る。
	private Task<TWrapper> CreateObjectFromUrlAsync<TWrapper, TInterface>(
		uint mediaTypeFlag,
		string url,
		MFResolutionFlag resolution,
		CancellationToken cancelToken,
		PropertyStore? propStore = null)
		where TWrapper : IComUnknownWrapper
		where TInterface : class
	{
		var mediaType = (uint)resolution & 0x3;
		if (mediaType != 0 && mediaType != mediaTypeFlag)
			Marshal.ThrowExceptionForHR(CommonHResults.EInvalidArg);
		resolution |= (MFResolutionFlag)mediaTypeFlag;

		return Task.Run(() =>
		{
			var tcs = new TaskCompletionSource<TWrapper>();
			using var pcookie = new ComUnknownHelper.IUnknownPointer();
			var hr = _obj.BeginCreateObjectFromURL(
				url,
				(uint)resolution,
				propStore?.WrappedObject as IPropertyStore,
				out Unsafe.NullRef<nint>(),
				new MFAsyncCallbackStatic(0, MFAsyncCallbackQueue.MultiThreaded, result =>
				{
					Marshal.ThrowExceptionForHR(_obj.EndCreateObjectFromURL(
						(IMFAsyncResult)result.WrappedObject!, out var ot, out var x));
					tcs.SetResult(IComUnknownWrapper.Casted<TWrapper, TInterface>(x!).Value);
				}),
				null);
			Marshal.ThrowExceptionForHR(hr);
			try
			{
				tcs.Task.Wait(cancelToken);
			}
			catch (AggregateException ex)
			{
				throw (ex?.InnerException ?? ex)!;
			}
			return tcs.Task.Result;
		});
	}

	public Task<MFMediaSource> CreateMediaSourceFromUrlAsync(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_MEDIASOURCE = 0x1;
		return CreateObjectFromUrlAsync<MFMediaSource, IMFMediaSource>(MF_RESOLUTION_MEDIASOURCE, url, resolution, propStore);
	}

	public Task<MFMediaSource> CreateMediaSourceFromUrlAsync(
		string url,
		MFResolutionFlag resolution,
		CancellationToken cancelToken,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_MEDIASOURCE = 0x1;
		return CreateObjectFromUrlAsync<MFMediaSource, IMFMediaSource>(MF_RESOLUTION_MEDIASOURCE, url, resolution, cancelToken, propStore);
	}

	public Task<MFByteStream> CreateByteStreamFromUrlAsync(
		string url,
		MFResolutionFlag resolution,
		PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_BYTESTREAM = 0x2;
		return CreateObjectFromUrlAsync<MFByteStream, IMFByteStream>(MF_RESOLUTION_BYTESTREAM, url, resolution, propStore);
	}

	public async Task<MFByteStream> CreateByteStreamFromUrlAsync(
		string url,
		MFResolutionFlag resolution,
		CancellationToken cancelToken
		, PropertyStore? propStore = null)
	{
		const uint MF_RESOLUTION_BYTESTREAM = 0x2;
		return await CreateObjectFromUrlAsync<MFByteStream, IMFByteStream>(MF_RESOLUTION_BYTESTREAM, url, resolution, cancelToken, propStore);
	}

	/*
	[PreserveSig]
	int BeginCreateObjectFromByteStream(
		IMFByteStream pByteStream,	string pwszURL,	uint dwFlags,	IPropertyStore pProps,	[MarshalAs(UnmanagedType.IUnknown)] out object? ppIUnknownCancelCookie,	IMFAsyncCallback pCallback,	[MarshalAs(UnmanagedType.IUnknown)] object? punkState);

	[PreserveSig]
	int EndCreateObjectFromByteStream(
		IMFAsyncResult pResult,	out MFObjectType pObjectType,	[MarshalAs(UnmanagedType.IUnknown)] out object? ppObject);*/

	public ComResult CancelObjectCreationNoThrow(object cancelCookie)
		=> new(_obj.CancelObjectCreation(cancelCookie));

	public void CancelObjectCreation(object cancelCookie)
		=> CancelObjectCreationNoThrow(cancelCookie).ThrowIfError();
}

/// <summary>
/// MF_OBJECT_TYPE
/// </summary>
public enum MFObjectType : uint
{
	MediaSource = 0,
	ByteStream,
	Invalid,
}

/// <summary>
/// MF_RESOLUTION_*
/// </summary>
[Flags]
public enum MFResolutionFlag : uint
{
	ContentDoesNotHaveToMatchExtensionOrMimeType = 0x10,
	KeepByteStreamAliveOnFail = 0x20,
	DisableLocalPlugins = 0x40,
	PluginControlPolicyApprovedOnly = 0x80,
	PluginControlPolicyWebOnly = 0x100,
	PluginControlPolicyWebOnlyEdgeMode = 0x200,
	EnableStorePlugins = 0x400,
	Read = 0x10000,
	Write = 0x20000
}
