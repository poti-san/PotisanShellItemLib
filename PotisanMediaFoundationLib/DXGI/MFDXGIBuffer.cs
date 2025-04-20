using System.Diagnostics;

using Potisan.Windows.MediaFoundation.DXGI.ComTypes;

namespace Potisan.Windows.MediaFoundation.DXGI;

public class MFDXGIBuffer(object? o) : ComUnknownWrapperBase<IMFDXGIBuffer>(o)
{
	public ComResult<object> GetResourceNoThrow(in Guid iid)
		=> new(_obj.GetResource(iid, out var x), x!);

	public object GetResource(in Guid iid)
		=> GetResourceNoThrow(iid).Value;

	public ComResult<TWrapper> GetResourceNoThrow<TWrapper, TUnknown>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetResource(typeof(TUnknown).GUID, out var x), x);

	public object GetResource<TWrapper, TUnknown>()
		where TWrapper : IComUnknownWrapper
		=> GetResourceNoThrow<TWrapper, TUnknown>().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> SubresourceIndexNoThrow
		=> new(_obj.GetSubresourceIndex(out var x), x);

	public uint SubresourceIndex
		=> SubresourceIndexNoThrow.Value;

	public ComResult<object> GetUnknownNoThrow(in Guid guid, in Guid iid)
		=> new(_obj.GetUnknown(guid, iid, out var x), x!);

	public object GetUnknown(in Guid guid, in Guid iid)
		=> GetUnknownNoThrow(guid, iid).Value;

	public ComResult<TWrapper> GetUnknownNoThrow<TWrapper, TUnknown>(in Guid guid)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetUnknown(guid, typeof(TUnknown).GUID, out var x), x);

	public object GetUnknown<TWrapper, TUnknown>(in Guid guid)
		where TWrapper : IComUnknownWrapper
		=> GetUnknownNoThrow<TWrapper, TUnknown>(guid).Value;

	public ComResult SetUnknowNoThrown(in Guid guid, object unkData)
		=> new(_obj.SetUnknown(guid, unkData));

	public void SetUnknow(in Guid guid, object unkData)
		=> SetUnknowNoThrown(guid, unkData).ThrowIfError();

	public ComResult SetUnknowNoThrown(in Guid guid, IComUnknownWrapper unkData)
		=> new(_obj.SetUnknown(guid, unkData.WrappedObject));

	public void SetUnknow(in Guid guid, IComUnknownWrapper unkData)
		=> SetUnknowNoThrown(guid, unkData).ThrowIfError();
}
