using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <summary>
/// MF属性。IMFAttributes COMインターフェイスのラッパーです。
/// </summary>
public class MFAttributes(object? o) : ComUnknownWrapperBase<IMFAttributes>(o)
{
	public ComResult<PropVariant> GetItemNoThrow(in Guid key)
	{
		var pv = new PropVariant();
		return new(_obj.GetItem(key, pv), pv);
	}
	public PropVariant GetItem(in Guid key)
		=> GetItemNoThrow(key).Value;

	public ComResult<MFAttributeType> GetItemTypeNoThrow(in Guid key)
		=> new(_obj.GetItemType(key, out var x), x);

	public MFAttributeType GetItemType(in Guid key)
		=> GetItemTypeNoThrow(key).Value;

	public ComResult<bool> CompareItemNoThrow(in Guid key, PropVariant other)
		=> new(_obj.CompareItem(key, other, out var x), x);

	public bool CompareItem(in Guid key, PropVariant other)
		=> CompareItemNoThrow(key, other);

	public ComResult<uint> GetUInt32NoThrow(in Guid key)
		=> new(_obj.GetUINT32(key, out var x), x);

	public uint GetUInt32(in Guid key)
		=> GetUInt32NoThrow(key).Value;

	public ComResult<ulong> GetUInt64NoThrow(in Guid key)
		=> new(_obj.GetUINT64(key, out var x), x);

	public ulong GetUInt64(in Guid key)
		=> GetUInt64NoThrow(key).Value;

	public ComResult<double> GetDoubleNoThrow(in Guid key)
		=> new(_obj.GetDouble(key, out var x), x);

	public double GetDouble(in Guid key
		) => GetDoubleNoThrow(key).Value;

	public ComResult<Guid> GetGuidNoThrow(in Guid key)
		=> new(_obj.GetGUID(key, out var x), x);

	public Guid GetGuid(in Guid key)
		=> GetGuidNoThrow(key).Value;

	public ComResult<uint> GetStringLengthNoThrow(in Guid key)
		=> new(_obj.GetStringLength(key, out var x), x);

	public uint GetStringLength(in Guid key)
		=> GetStringLengthNoThrow(key).Value;

	//[PreserveSig]
	//int GetString(
	//	in Guid guidKey,
	//	ref char pwszValue,
	//	uint cchBufSize,
	//	ref uint pcchLength);

	public ComResult<string> GetStringNoThrow(in Guid key)
		=> new(_obj.GetAllocatedString(key, out var x, out _), x ?? "");

	public string GetString(in Guid key)
		=> GetStringNoThrow(key).Value;

	public ComResult<uint> GetBlobLengthNoThrow(in Guid key)
		=> new(_obj.GetBlobSize(key, out var x), x);

	public uint GetBlobLength(in Guid key)
		=> GetBlobLengthNoThrow(key).Value;

	public ComResult<byte[]> GetBlobNoThrow(in Guid key)
		=> new(_obj.GetAllocatedBlob(key, out var x, out _), x ?? []);

	public byte[] GetBlob(in Guid key)
		=> GetBlobNoThrow(key).Value;

	//[PreserveSig]
	//int GetBlob(
	//	in Guid guidKey,
	//	ref byte pBuf,
	//	uint cbBufSize,
	//	ref uint pcbBlobSize);

	public ComResult<object> GetUnknownNoThrow(in Guid key)
		=> new(_obj.GetUnknown(key, new Guid("00000000-0000-0000-C000-000000000046"), out var x), x!);

	public object GetUnknown(in Guid key)
		=> GetUnknownNoThrow(key).Value;

	public ComResult<TWrapper> GetUnknownNoThrow<TWrapper, TInterface>(in Guid key)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetUnknown(key, typeof(TInterface).GUID, out var x), x);

	public TWrapper GetUnknown<TWrapper, TInterface>(in Guid key)
		where TWrapper : IComUnknownWrapper
		=> GetUnknownNoThrow<TWrapper, TInterface>(key).Value;

	public ComResult SetItemNoThrow(in Guid key, PropVariant value)
		=> new(_obj.SetItem(key, value));

	public void SetItem(in Guid key, PropVariant value)
		=> SetItemNoThrow(key, value).ThrowIfError();

	public ComResult DeleteItemNoThrow(in Guid key)
		=> new(_obj.DeleteItem(key));

	public void DeleteItem(in Guid key)
		=> DeleteItemNoThrow(key).ThrowIfError();

	public ComResult DeleteAllItemsNoThrow()
		=> new(_obj.DeleteAllItems());

	public void DeleteAllItems()
		=> DeleteAllItemsNoThrow().ThrowIfError();

	public ComResult SetUInt32NoThrow(in Guid key, uint value)
		=> new(_obj.SetUINT32(key, value));

	public void SetUInt32(in Guid key, uint value)
		=> SetUInt32NoThrow(key, value).ThrowIfError();

	public ComResult SetUInt64NoThrow(in Guid key, ulong value)
		=> new(_obj.SetUINT64(key, value));

	public void SetUInt64(in Guid key, ulong value)
		=> SetUInt64NoThrow(key, value).ThrowIfError();

	public ComResult SetDoubleNoThrow(in Guid key, double value)
		=> new(_obj.SetDouble(key, value));

	public void SetDouble(in Guid key, double value)
		=> SetDoubleNoThrow(key, value).ThrowIfError();

	public ComResult SetGuidNoThrow(in Guid key, in Guid value)
		=> new(_obj.SetGUID(key, value));

	public void SetGuid(in Guid key, in Guid value)
		=> SetGuidNoThrow(key, value).ThrowIfError();

	public ComResult SetStringNoThrow(in Guid key, string value)
		=> new(_obj.SetString(key, value));

	public void SetString(in Guid key, string value)
		=> SetStringNoThrow(key, value).ThrowIfError();

	public ComResult SetBlobNoThrow(in Guid key, ReadOnlySpan<byte> value)
		=> new(_obj.SetBlob(key, in MemoryMarshal.GetReference(value), checked((uint)value.Length)));

	public void SetBlob(in Guid key, ReadOnlySpan<byte> value)
		=> SetBlobNoThrow(key, value).ThrowIfError();

	public ComResult SetUnknownNoThrow(in Guid key, object value)
		=> new(_obj.SetUnknown(key, value));

	public void SetUnknown(in Guid key, object value)
		=> SetUnknownNoThrow(key, value).ThrowIfError();

	public ComResult SetUnknownNoThrow<TWrapper>(in Guid key, TWrapper value)
		where TWrapper : IComUnknownWrapper
		=> new(_obj.SetUnknown(key, value.WrappedObject));

	public void SetUnknown<TWrapper>(in Guid key, TWrapper value)
		where TWrapper : IComUnknownWrapper
		=> SetUnknownNoThrow(key, value).ThrowIfError();

	#region プロパティ操作用の簡易メソッド

	public uint? GetUInt32WithDefault(in Guid key) => GetUInt32NoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public ulong? GetUInt64WithDefault(in Guid key) => GetUInt64NoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public double? GetDoubleWithDefault(in Guid key) => GetDoubleNoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public Guid? GetGuidWithDefault(in Guid key) => GetGuidNoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public string? GetStringWithDefault(in Guid key) => GetStringNoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public uint? GetBlobLengthWithDefault(in Guid key) => GetBlobLengthNoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public byte[]? GetBlobWithDefault(in Guid key) => GetBlobNoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public object? GetUnknownWithDefault(in Guid key) => GetUnknownNoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null;
	public object? GetUnknownWithDefault<TWrapper, TInterface>(in Guid key)
		where TWrapper : IComUnknownWrapper
		where TInterface : class
		=> IComUnknownWrapper.Casted<TWrapper, TInterface>((GetUnknownNoThrow(key) is { Succeeded: true, ValueUnchecked: var value } ? value : null)!).ValueUnchecked;

	public void SetUInt32WithDefault(in Guid key, uint? value) { if (value is { } v) SetUInt32NoThrow(key, v); else DeleteItemNoThrow(key); }
	public void SetUInt64WithDefault(in Guid key, ulong? value) { if (value is { } v) SetUInt64NoThrow(key, v); else DeleteItemNoThrow(key); }
	public void SetDoubleWithDefault(in Guid key, double? value) { if (value is { } v) SetDoubleNoThrow(key, v); else DeleteItemNoThrow(key); }
	public void SetGuidWithDefault(in Guid key, Guid? value) { if (value is { } v) SetGuidNoThrow(key, v); else DeleteItemNoThrow(key); }
	public void SetStringWithDefault(in Guid key, string? value) { if (value is { } v) SetStringNoThrow(key, v); else DeleteItemNoThrow(key); }
	public void SetBlobWithDefault(in Guid key, byte[]? value) { if (value is { } v) SetBlobNoThrow(key, v); else DeleteItemNoThrow(key); }
	public void SetUnknownWithDefault(in Guid key, object? value) { if (value is { } v) SetUnknownNoThrow(key, v); else DeleteItemNoThrow(key); }
	public void SetUnknownWithDefault(in Guid key, IComUnknownWrapper? value) { if (value is { } v) SetUnknownNoThrow(key, v.WrappedObject!); else DeleteItemNoThrow(key); }

	#endregion プロパティ操作用の簡易メソッド

	public ComResult LockStoreNoThrow()
		=> new(_obj.LockStore());

	public void LockStore()
		=> LockStoreNoThrow().ThrowIfError();

	public ComResult UnlockStoreNoThrow()
		=> new(_obj.UnlockStore());

	public void UnlockStore()
		=> UnlockStoreNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CountNoThrow
		=> new(_obj.GetCount(out var x), x);

	public uint Count
		=> CountNoThrow.Value;

	public ComResult<KeyValuePair<Guid, PropVariant>> GetItemByIndexNoThrow(uint index)
	{
		var pv = new PropVariant();
		return new(_obj.GetItemByIndex(index, out var key, pv), new(key, pv));
	}

	public KeyValuePair<Guid, PropVariant> GetItemByIndex(uint index)
		=> GetItemByIndexNoThrow(index).Value;

	public ComResult<Guid> GetItemKeyByIndexNoThrow(uint index)
	{
		return new(_obj.GetItemByIndex(index, out var key, null!), key);
	}

	public Guid GetItemKeyByIndex(uint index)
		=> GetItemKeyByIndexNoThrow(index).Value;

	public ComResult<PropVariant> GetItemValueByIndexNoThrow(uint index)
	{
		var pv = new PropVariant();
		return new(_obj.GetItemByIndex(index, out _, pv), pv);
	}

	public PropVariant GetItemValueByIndex(uint index)
		=> GetItemValueByIndexNoThrow(index).Value;

	public ComResult CopyAllItemsToNoThrow(MFAttributes destination)
		=> new(_obj.CopyAllItems((IMFAttributes)destination.WrappedObject!));

	public void CopyAllItemsTo(MFAttributes destination)
		=> CopyAllItemsToNoThrow(destination).ThrowIfError();

	public static ComResult<MFAttributes> CreateNoThrow(uint initialSize = 0)
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateAttributes(out IMFAttributes ppMFAttributes, uint cInitialSize);

		return new(MFCreateAttributes(out var x, initialSize), new(x));
	}

	public static MFAttributes Create(uint initialSize = 0)
		=> CreateNoThrow(initialSize).Value;

	public static ComResult<MFAttributes> CreateNoThrow(ReadOnlySpan<byte> blob)
	{
		[DllImport("mfplat.dll")]
		static extern int MFInitAttributesFromBlob(IMFAttributes pAttributes, in byte pBuf, uint cbBufSize);

		var attrs = CreateNoThrow(0);
		if (!attrs) return new(CommonHResults.EFail, new(null));

		var hr = MFInitAttributesFromBlob(
			(IMFAttributes)attrs.ValueUnchecked.WrappedObject!, in MemoryMarshal.GetReference(blob), checked((uint)blob.Length));
		return new(hr, hr == 0 ? attrs.ValueUnchecked : new MFAttributes(null));
	}

	public static MFAttributes Create(ReadOnlySpan<byte> blob)
		=> CreateNoThrow(blob).Value;

	public ComResult<MFAttributes> CloneNoThrow()
	{
		var attrs = CreateNoThrow(Count);
		if (!attrs) return new(attrs.HResult, new(null));

		var ret = CopyAllItemsToNoThrow(attrs.ValueUnchecked);
		return ret ? attrs : new(ret.HResult, new(null));
	}

	public MFAttributes Clone()
		=> CloneNoThrow().Value;

	public IEnumerable<KeyValuePair<Guid, PropVariant>> GetEnumerable()
	{
		var c = Count;
		for (uint i = 0; i < c; i++)
		{
			yield return GetItemByIndex(i);
		}
	}

	public ImmutableArray<KeyValuePair<Guid, PropVariant>> Items
	{
		get
		{
			var c = Count;
			var a = GC.AllocateUninitializedArray<KeyValuePair<Guid, PropVariant>>(checked((int)c));
			for (uint i = 0; i < c; i++)
			{
				a[i] = GetItemByIndex(i);
			}
			return ImmutableCollectionsMarshal.AsImmutableArray(a);
		}
	}

	public ComResult SerializeNoThrow(MFAttributeSerializeOption options, ComStream stream)
	{
		[DllImport("mfplat.dll")]
		static extern int MFSerializeAttributesToStream(IMFAttributes pAttr, uint dwOptions, IStream pStm);

		return new(MFSerializeAttributesToStream((IMFAttributes)WrappedObject!, (uint)options, (IStream)stream.WrappedObject!));
	}

	public static ComResult<MFAttributes> DeserializeNoThrow(MFAttributeSerializeOption options, ComStream stream)
	{
		[DllImport("mfplat.dll")]
		static extern int MFDeserializeAttributesToStream(IMFAttributes pAttr, uint dwOptions, IStream pStm);

		try
		{
			var attrs = Create();
			return new(
				MFDeserializeAttributesToStream((IMFAttributes)attrs.WrappedObject!, (uint)options, (IStream)stream.WrappedObject!), new(attrs));
		}
		catch (Exception ex)
		{
			return new(ex.HResult, new(null));
		}
	}

	public MFDeviceSourceAttributes ForDeviceSource => new(this);
	public MFMediaTypeAttributes ForMediaType => new(this);
	public MFSourceReaderAttributes ForSourceReader => new(this);
	public MFMediaEngineAttributes ForMediaEngine => new(this);
}

//	STDAPI
//	MFGetAttributesAsBlobSize(
//		_In_ IMFAttributes*  pAttributes,
//		_Out_ UINT32*         pcbBufSize

//		);

//	STDAPI
//	MFGetAttributesAsBlob(
//		_In_ IMFAttributes*  pAttributes,
//		_Out_writes_bytes_(cbBufSize) UINT8*          pBuf,
//		uint            cbBufSize

//		);

/// <summary>
/// MF_ATTRIBUTE_TYPE
/// </summary>
public enum MFAttributeType : uint
{
	UInt32 = VarType.UI4,
	UInt64 = VarType.UI8,
	Double = VarType.R8,
	Guid = VarType.Clsid,
	String = VarType.LPWStr,
	Blob = VarType.Vector | VarType.UI1,
	IUnknown = VarType.Unknown,
}

/// <summary>
/// MF_ATTRIBUTES_MATCH_TYPE
/// </summary>
public enum MFAttributeMatchType : uint
{
	OurItems = 0,
	TheirItems = 1,
	AllItems = 2,
	Intersection = 3,
	Smaller = 4,
}

/// <summary>
/// MF_ATTRIBUTE_SERIALIZE_OPTIONS
/// </summary>
[Flags]
public enum MFAttributeSerializeOption : uint
{
	UnknownByRef = 0x1
}