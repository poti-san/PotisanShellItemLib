using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Potisan.Windows.Com;
using Potisan.Windows.Dxgi.ComTypes;

namespace Potisan.Windows.Dxgi;

public class DxgiObject(object? o) : ComUnknownWrapperBase<IDXGIObject>(o)
{
	public ComResult SetPrivateDataNoThrow(in Guid name, ReadOnlySpan<byte> Data)
		=> new(_obj.SetPrivateData(name, (uint)Data.Length, MemoryMarshal.GetReference(Data)));

	public void SetPrivateData(in Guid name, ReadOnlySpan<byte> Data)
		=> SetPrivateDataNoThrow(name, Data).ThrowIfError();

	public ComResult SetPrivateDataInterfaceNoThrow(in Guid name, object? unk)
		=> new(_obj.SetPrivateDataInterface(name, unk));

	public void SetPrivateDataInterface(in Guid name, object? unk)
		=> SetPrivateDataInterfaceNoThrow(name, unk).ThrowIfError();

	public ComResult SetPrivateDataInterfaceNoThrow(in Guid name, IComUnknownWrapper? unk)
		=> new(_obj.SetPrivateDataInterface(name, unk?.WrappedObject));

	public void SetPrivateDataInterface(in Guid name, IComUnknownWrapper? unk)
		=> SetPrivateDataInterfaceNoThrow(name, unk?.WrappedObject).ThrowIfError();

	public ComResult<byte[]> GetPrivateDataNoThrow(in Guid name)
	{
		uint dataSize = 0;
		_obj.GetPrivateData(name, ref dataSize, ref Unsafe.NullRef<byte>());

		var buffer = GC.AllocateUninitializedArray<byte>(checked((int)dataSize));
		return new(_obj.GetPrivateData(name, ref dataSize, ref MemoryMarshal.GetArrayDataReference(buffer)), buffer);
	}

	public byte[] GetPrivateData(in Guid name)
		=> GetPrivateDataNoThrow(name).Value;

	public ComResult<object> GetPrivateDataInterfaceNoThrow(in Guid name)
	{
		uint dataSize = 0;
		_obj.GetPrivateData(name, ref dataSize, ref Unsafe.NullRef<byte>());

		if (dataSize != nint.Size) return new(CommonHResults.EInvalidArg, null!);

		nint p = 0;
		return new(_obj.GetPrivateData(name, ref dataSize, ref Unsafe.As<nint, byte>(ref p)), Marshal.GetObjectForIUnknown(p));
	}

	public object GetPrivateDataInterface(in Guid name)
		=> GetPrivateDataInterfaceNoThrow(name).Value;

	public ComResult<TWrapper> GetPrivateDataInterfaceNoThrow<TWrapper>(in Guid name)
		where TWrapper : IComUnknownWrapper
	{
		uint dataSize = 0;
		_obj.GetPrivateData(name, ref dataSize, ref Unsafe.NullRef<byte>());

		if (dataSize != nint.Size) return IComUnknownWrapper.Wrap<TWrapper>(CommonHResults.EInvalidArg, null);

		nint p = 0;
		return IComUnknownWrapper.Wrap<TWrapper>(
			_obj.GetPrivateData(name, ref dataSize, ref Unsafe.As<nint, byte>(ref p)),
			Marshal.GetObjectForIUnknown(p));
	}

	public TWrapper GetPrivateDataInterface<TWrapper>(in Guid name)
		where TWrapper : IComUnknownWrapper
		=> GetPrivateDataInterfaceNoThrow<TWrapper>(name).Value;

	public ComResult<object> GetParentNoResult(in Guid iid)
		=> new(_obj.GetParent(iid, out var x), x!);

	public object GetParent(in Guid iid)
		=> GetParentNoResult(iid).Value;

	public ComResult<TWrapper> GetParentNoResult<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetParent(typeof(TInterface).GUID, out var x), x);

	public TWrapper GetParent<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetParentNoResult<TWrapper, TInterface>().Value;
}
