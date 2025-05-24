using System.Collections;
using System.Collections.Immutable;

using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティストア。プロパティのキーと値の組を管理します。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IPropertyStore</c> COMインターフェイスのラッパーです。
/// </remarks>
public class PropertyStore(object? o) : ComUnknownWrapperBase<IPropertyStore>(o), IReadOnlyDictionary<PropertyKey, PropVariant>
{
	/// <summary>
	/// メモリ内プロパティストアを作成します。
	/// </summary>
	/// <returns>メモリ内プロパティストア。</returns>
	/// <remarks>
	/// メモリ内プロパティストアは次のCOMインターフェイスをサポートします。
	/// IPropertyStore、INamedPropertyStore、IPropertyStoreCache、IPersistStream、IPropertyBag、IPersistSerializedPropStorage。
	/// </remarks>
	public static ComResult<PropertyStore> CreateInMemoryNoThrow()
	{
		// TODO IPropertyStore、INamedPropertyStore、IPropertyStoreCache、IPersistStream、IPropertyBag、IPersistSerializedPropStorage。
		[DllImport("propsys.dll")]
		static extern int PSCreateMemoryPropertyStore(in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(PSCreateMemoryPropertyStore(typeof(IPropertyStore).GUID, out var x), new(x));
	}

	/// <inheritdoc cref="CreateInMemoryNoThrow"/>
	public static PropertyStore CreateInMemory()
		=> CreateInMemoryNoThrow().Value;

	/// <summary>
	/// プロパティ数。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CountNoThrow
		=> new(_obj.GetCount(out var x), x);

	/// <inheritdoc cref="CountNoThrow"/>
	public uint Count
		=> CountNoThrow.Value;

	/// <summary>
	/// 指定位置のプロパティキーを取得します。
	/// </summary>
	/// <param name="index">プロパティキーの位置。</param>
	/// <returns>プロパティキー。</returns>
	public ComResult<PropertyKey> GetKeyNoThrow(uint index)
	{
		var key = new PropertyKey();
		return new(_obj.GetAt(index, key), key);
	}

	/// <inheritdoc cref="GetKey"/>
	public PropertyKey GetKey(uint index)
		=> GetKeyNoThrow(index).Value;

	/// <summary>
	/// プロパティキーに対応するプロパティ値を取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティ値。</returns>
	public ComResult<PropVariant> GetValueNoThrow(PropertyKey key)
	{
		var x = new PropVariant();
		return new(_obj.GetValue(key, x), x);
	}

	/// <inheritdoc cref="GetValueNoThrow(PropertyKey)"/>
	public PropVariant GetValue(PropertyKey key)
		=> GetValueNoThrow(key).Value;

	/// <summary>
	/// 指定位置のプロパティ値を取得します。
	/// </summary>
	/// <param name="index"></param>
	/// <returns>プロパティ値。</returns>
	/// <remarks>
	/// このメソッドは内部で指定位置のプロパティキーを取得します。繰り返し呼び出す場合はキーの取得を検討してください。
	/// </remarks>
	public ComResult<PropVariant> GetValueNoThrow(uint index)
		=> GetValueNoThrow(GetKey(index));

	/// <inheritdoc cref="GetValueNoThrow(uint)"/>
	public PropVariant GetValue(uint index)
		=> GetValueNoThrow(index).Value;

	/// <summary>
	/// プロパティキーに対応するプロパティ値を設定します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <param name="value">プロパティ値。</param>
	public ComResult SetValueNoThrow(PropertyKey key, PropVariant value)
		=> new(_obj.SetValue(key, value));

	/// <inheritdoc cref="SetValueNoThrow(PropertyKey, PropVariant)"/>
	public void SetValue(PropertyKey key, PropVariant value)
		=> SetValueNoThrow(key, value).ThrowIfError();

	/// <summary>
	/// 指定位置のプロパティ値を設定します。
	/// </summary>
	/// <param name="index">プロパティ位置。</param>
	/// <param name="value">プロパティ値。</param>
	/// <remarks>
	/// このメソッドは内部で指定位置のプロパティキーを取得します。繰り返し呼び出す場合はキーの取得を検討してください。
	/// </remarks>
	public ComResult SetValueNoThrow(uint index, PropVariant value)
		=> new(_obj.SetValue(GetKey(index), value));

	/// <inheritdoc cref="SetValueNoThrow(uint, PropVariant)"/>
	public void SetValue(uint index, PropVariant value)
		=> SetValueNoThrow(GetKey(index), value).ThrowIfError();

	/// <summary>
	/// プロパティの変更を反映します。
	/// </summary>
	/// <remarks>
	/// メモリ内プロパティストアでは何も起きません。
	/// </remarks>
	public ComResult CommitNoThrow()
		=> new(_obj.Commit());

	/// <inheritdoc cref="CommitNoThrow"/>
	public void Commit()
		=> CommitNoThrow().ThrowIfError();

	/// <summary>
	/// プロパティキーと値の辞書を作成します。
	/// </summary>
	/// <returns>プロパティキーと値の辞書。この辞書の内容はプロパティストアに反映されません。</returns>
	public Dictionary<PropertyKey, PropVariant> ToDictionary()
		=> Items.ToDictionary();

	/// <summary>
	/// プロパティキーのイテレーターを取得します。
	/// </summary>
	/// <remarks>プロパティストア自体が解放された場合は無効化されることに注意してください。</remarks>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<PropertyKey> KeyEnumerable
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetKey(i);
		}
	}

	/// <summary>
	/// プロパティキーの配列を取得します。
	/// </summary>
	public ImmutableArray<PropertyKey> Keys
		=> [.. KeyEnumerable];

	/// <summary>
	/// プロパティ値のイテレーター。
	/// </summary>
	/// <remarks>プロパティストア自体が解放された場合は無効化されることに注意してください。</remarks>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<PropVariant> ValueEnumerable
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetValue(i);
		}
	}

	public ImmutableArray<PropVariant> Values
		=> [.. ValueEnumerable];

	/// <summary>
	/// プロパティキーと値ペアのイテレーター。
	/// </summary>
	/// <remarks>プロパティストア自体が解放された場合は無効化されることに注意してください。</remarks>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<KeyValuePair<PropertyKey, PropVariant>> ItemEnumerable
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

	/// <summary>
	/// プロパティキーと値ペアの配列。
	/// </summary>
	public ImmutableArray<KeyValuePair<PropertyKey, PropVariant>> Items
		=> [.. ItemEnumerable];

	/// <summary>
	/// 与えられたプロパティキーをプロパティストアに有効な値が設定されているかでフィルター処理します。
	/// </summary>
	public IEnumerable<PropertyKey> WhereValidKeys(IEnumerable<PropertyKey> keys)
	{
		return keys.Where(key => GetValueNoThrow(key) is var cr && cr && !cr.ValueUnchecked.IsEmpty);
	}

	/// <summary>
	///  与えられたプロパティキーから有効なプロパティキーと値のペア配列を作成します。
	/// </summary>
	/// <param name="keys">プロパティキー。無効なキーが含まれていても構いません。</param>
	/// <returns>プロパティキーと値のペア配列。</returns>
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

	IEnumerable<PropertyKey> IReadOnlyDictionary<PropertyKey, PropVariant>.Keys
		=> Keys;

	IEnumerable<PropVariant> IReadOnlyDictionary<PropertyKey, PropVariant>.Values
		=> Values;

	int IReadOnlyCollection<KeyValuePair<PropertyKey, PropVariant>>.Count
		=> checked((int)Count);

	PropVariant IReadOnlyDictionary<PropertyKey, PropVariant>.this[PropertyKey key]
		=> GetValue(key);

	bool IReadOnlyDictionary<PropertyKey, PropVariant>.ContainsKey(PropertyKey key)
		=> Keys.Contains(key);

	bool IReadOnlyDictionary<PropertyKey, PropVariant>.TryGetValue(PropertyKey key, out PropVariant value)
	{
		var cr = GetValueNoThrow(key);
		value = cr.ValueUnchecked;
		return cr;
	}

	IEnumerator<KeyValuePair<PropertyKey, PropVariant>> IEnumerable<KeyValuePair<PropertyKey, PropVariant>>.GetEnumerator()
		=> ItemEnumerable.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> ItemEnumerable.GetEnumerator();
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