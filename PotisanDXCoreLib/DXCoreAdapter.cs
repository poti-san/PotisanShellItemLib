using System.Buffers.Binary;
using System.Text;

using Potisan.Windows.Com;
using Potisan.Windows.DXCore.ComTypes;

namespace Potisan.Windows.DXCore;

/// <summary>
/// DXCoreのアダプタ情報（バージョン1）。<see cref="DXCoreAdapterList"/>から作成します。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class DXCoreAdapter(object? o) : ComUnknownWrapperBase<IDXCoreAdapter>(o)
{
	public bool IsValid
		=> _obj.IsValid();

	public bool IsAttributeSupported(in Guid attributeGUID)
		=> _obj.IsAttributeSupported(attributeGUID);

	public bool IsPropertySupported(DXCoreAdapterProperty property)
		=> _obj.IsPropertySupported(property);

	public ComResult<byte[]> GetPropertyNoThrow(DXCoreAdapterProperty property)
	{
		var cr = GetPropertySizeNoThrow(property);
		if (cr is not { Succeeded: true, ValueUnchecked: var size })
			return new(cr.HResult, null!);

		var buffer = GC.AllocateUninitializedArray<byte>((int)size);
		return new(_obj.GetProperty(property, size, ref MemoryMarshal.GetArrayDataReference(buffer)), buffer);
	}

	public byte[] GetProperty(DXCoreAdapterProperty property)
		=> GetPropertyNoThrow(property).Value;

	public ComResult<byte[]> GetPropertyNoThrow(DXCoreAdapterProperty property, uint size)
	{
		var buffer = GC.AllocateUninitializedArray<byte>((int)size);
		return new(_obj.GetProperty(property, size, ref MemoryMarshal.GetArrayDataReference(buffer)), buffer);
	}

	public byte[] GetProperty(DXCoreAdapterProperty property, uint size)
		=> GetPropertyNoThrow(property, size).Value;

	public ComResult GetPropertyNoThrow(DXCoreAdapterProperty property, Span<byte> buffer)
		=> new(_obj.GetProperty(property, (uint)buffer.Length, ref MemoryMarshal.GetReference(buffer)));

	public void GetProperty(DXCoreAdapterProperty property, Span<byte> buffer)
		=> GetPropertyNoThrow(property, buffer).ThrowIfError();

	#region プロパティ

	public ComResult<string> GetPropertyStringNoThrow(DXCoreAdapterProperty property)
		=> GetPropertyNoThrow(property) switch
		{
			{ Succeeded: true, HResult: var hr, ValueUnchecked: var value } => new(hr, Encoding.UTF8.GetString(value[..^1])),
			{ HResult: var hr } => new(hr, null!),
		};

	public string GetPropertyString(DXCoreAdapterProperty property)
		=> GetPropertyStringNoThrow(property).Value;

	public ComResult<uint> GetPropertyUInt32NoThrow(DXCoreAdapterProperty property)
		=> GetPropertyNoThrow(property, sizeof(uint)) switch
		{
			{ Succeeded: true, HResult: var hr, ValueUnchecked: var value } => new(hr, BinaryPrimitives.ReadUInt32LittleEndian(value)),
			{ HResult: var hr } => new(hr, 0),
		};

	public string GetPropertyUInt32(DXCoreAdapterProperty property)
		=> GetPropertyStringNoThrow(property).Value;

	public ComResult<T> GetPropertyUnmanagedNoThrow<T>(DXCoreAdapterProperty property)
		where T : unmanaged
		=> GetPropertyNoThrow(property, (uint)Marshal.SizeOf<T>()) switch
		{
			{ Succeeded: true, HResult: var hr, ValueUnchecked: var value } => new(hr, Unsafe.As<byte, T>(ref value[0])),
			{ HResult: var hr } => new(hr, default),
		};

	public T GetPropertyUnmanaged<T>(DXCoreAdapterProperty property)
		where T : unmanaged
		=> GetPropertyUnmanagedNoThrow<T>(property).Value;

	public ComResult<string> DriverDescriptionNoThrow => GetPropertyStringNoThrow(DXCoreAdapterProperty.DriverDescription);
	public string DriverDescription => DriverDescriptionNoThrow.Value;

	public ComResult<string> AdapterEngineNameNoThrow => GetPropertyStringNoThrow(DXCoreAdapterProperty.AdapterEngineName);
	public string AdapterEngineName => AdapterEngineNameNoThrow.Value;

	#endregion

	public ComResult<nuint> GetPropertySizeNoThrow(DXCoreAdapterProperty property)
		=> new(_obj.GetPropertySize(property, out var x), x);

	public nuint GetPropertySize(DXCoreAdapterProperty property)
		=> GetPropertySizeNoThrow(property).Value;

	public bool IsQueryStateSupported(DXCoreAdapterState property)
		=> _obj.IsQueryStateSupported(property);

	//[PreserveSig]
	//int QueryState(
	//	DXCoreAdapterState state,
	//	nuint inputStateDetailsSize,
	//	in byte inputStateDetails,
	//	nuint outputBufferSize,
	//	ref byte outputBuffer);

	public bool IsSetStateSupported(DXCoreAdapterState property)
		=> _obj.IsSetStateSupported(property);

	//[PreserveSig]
	//int SetState(
	//	DXCoreAdapterState state,
	//	nuint inputStateDetailsSize,
	//	in byte inputStateDetails,
	//	nuint inputDataSize,
	//	in byte inputData);

	#region GetFactory

	public ComResult<object> GetFactoryNoThrow(in Guid iid)
		=> new(_obj.GetFactory(iid, out var x), x!);

	public object GetFactory(in Guid iid)
		=> GetFactoryNoThrow(iid).Value;

	public ComResult<TWrapper> GetFactoryTNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetFactory(typeof(TInterface).GUID, out var x), x);

	public TWrapper GetFactoryT<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> GetFactoryTNoThrow<TWrapper, TInterface>().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<DXCoreAdapterFactory> FactoryNoThrow
		=> GetFactoryTNoThrow<DXCoreAdapterFactory, IDXCoreAdapterFactory>();

	public DXCoreAdapterFactory Factory
		=> FactoryNoThrow.Value;

	#endregion GetFactory
}
