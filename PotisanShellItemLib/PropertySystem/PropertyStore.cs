using PotisanShellItemLib.PropertySystem.ComTypes;

namespace PotisanShellItemLib.PropertySystem;

public class PropertyStore : IComUnknownWrapper
{
	private readonly IPropertyStore _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public PropertyStore(object? o)
	{
		_obj = o == null ? null! : (IPropertyStore)o;
	}

	/// <inheritdoc/>
	public object? WrappedObject => _obj;

	/// <inheritdoc/>
	public void Dispose()
	{
		if (_obj != null)
			Marshal.FinalReleaseComObject(_obj);
		GC.SuppressFinalize(this);
	}

	public static ComResult<PropertyStore> CreateInMemoryNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PSCreateMemoryPropertyStore(in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(PSCreateMemoryPropertyStore(typeof(IPropertyStore).GUID, out var x), new(x));
	}
	public static PropertyStore CreateInMemory() => CreateInMemoryNoThrow().Value;

	public ComResult<uint> CountNoThrow => new(_obj.GetCount(out var x), x);
	public uint Count => CountNoThrow.Value;

	public ComResult<PropertyKey> GetKeyNoThrow(uint index)
	{
		var key = new PropertyKey();
		return new(_obj.GetAt(index, key), key);
	}
	public PropertyKey GetKey(uint index) => GetKeyNoThrow(index).Value;

	public ComResult<PropVariant> GetValueNoThrow(in PropertyKey key)
	{
		var x = new PropVariant();
		return new(_obj.GetValue(key, x), x);
	}
	public PropVariant GetValue(in PropertyKey key) => GetValueNoThrow(key).Value;

	public ComResult<PropVariant> GetValueNoThrow(uint index) => GetValueNoThrow(GetKey(index));
	public PropVariant GetValue(uint index) => GetValueNoThrow(index).Value;

	public ComResult SetValueNoThrow(PropertyKey key, PropVariant value) => new(_obj.SetValue(key, value));
	public void SetValue(PropertyKey key, PropVariant value) => SetValueNoThrow(key, value).ThrowIfError();

	public ComResult SetValueNoThrow(uint index, PropVariant value) => new(_obj.SetValue(GetKey(index), value));
	public void SetValue(uint index, PropVariant value) => SetValueNoThrow(GetKey(index), value).ThrowIfError();

	public ComResult CommitNoThrow() => new(_obj.Commit());
	public void Commit() => CommitNoThrow().ThrowIfError();

	public Dictionary<PropertyKey, PropVariant> ToDictionary => Items.ToDictionary();

	public IEnumerable<PropertyKey> Keys
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetKey(i);
		}
	}

	public IEnumerable<PropVariant> Values
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetValue(i);
		}
	}

	public IEnumerable<KeyValuePair<PropertyKey, PropVariant>> Items
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
			{
				var key = GetKey(i);
				yield return new(key, GetValue(key));
			}
		}
	}

	public IEnumerable<PropertyKey> WhereValidKeys(IEnumerable<PropertyKey> keys)
	{
		return keys.Where(key => GetValueNoThrow(key) is var cr && cr && !cr.ValueUnchecked.IsEmpty);
	}

	public KeyValuePair<PropertyKey, PropVariant>[] GetItemsForKeysIgnoreMissingKeys(IEnumerable<PropertyKey> keys)
	{
		var items = new List<KeyValuePair<PropertyKey, PropVariant>>();
		foreach (var key in keys)
		{
			var value = GetValueNoThrow(key);
			if (value)
				items.Add(new(key, value.ValueUnchecked));
		}
		return [.. items];
	}
}

/// <summary>
/// GETPROPERTYSTOREFLAGS
/// </summary>
public enum GetPropertyStoreFlag : uint
{
	Default = 0,
	HandlerPropertiesOnly = 0x1,
	ReadWrite = 0x2,
	Temporary = 0x4,
	FastPropertiesOnly = 0x8,
	OpenSlowItem = 0x10,
	DelayCreation = 0x20,
	BestEffort = 0x40,
	NoOPLock = 0x80,
	PreferQueryProperties = 0x100,
	ExtrinsicProperties = 0x200,
	ExtrinsicPropertiesOnly = 0x400,
	VolatileProperties = 0x800,
	VolatilePropertiesOnly = 0x1000,
}