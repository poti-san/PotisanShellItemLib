using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFByteStreamProxyClassFactory(object? o) : ComUnknownWrapperBase<IMFByteStreamProxyClassFactory>(o)
{
	public static ComResult<MFByteStreamProxyClassFactory> CreateNoThrow()
	{
		Guid CLSID_MFByteStreamProxyClassFactory = new(0x770e8e77, 0x4916, 0x441c, 0xa9, 0xa7, 0xb3, 0x42, 0xd0, 0xee, 0xbc, 0x71);
		return ComHelper.CreateInstanceNoThrow<MFByteStreamProxyClassFactory, IMFByteStreamProxyClassFactory>(
			CLSID_MFByteStreamProxyClassFactory);
	}

	public static MFByteStreamProxyClassFactory Create()
		=> CreateNoThrow().Value;

	public ComResult<object> CreateByteStreamProxyNoThrow(MFByteStream byteStream, in Guid iid)
		=> new(_obj.CreateByteStreamProxy((IMFByteStream)byteStream.WrappedObject!, null, iid, out var x), x!);

	public object CreateByteStreamProxy(MFByteStream byteStream, in Guid iid)
		=> CreateByteStreamProxyNoThrow(byteStream, iid).Value;

	public ComResult<TWrapper> CreateByteStreamProxyNoThrow<TWrapper, TInterface>(MFByteStream byteStream)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.CreateByteStreamProxy(
			(IMFByteStream)byteStream.WrappedObject!, null, typeof(TInterface).GUID, out var x), x);

	public TWrapper CreateByteStreamProxy<TWrapper, TInterface>(MFByteStream byteStream)
		where TWrapper : IComUnknownWrapper
		=> CreateByteStreamProxyNoThrow<TWrapper, TInterface>(byteStream).Value;
}
