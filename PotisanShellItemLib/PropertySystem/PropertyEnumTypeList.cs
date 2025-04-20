using PotisanShellItemLib.PropertySystem.ComTypes;

namespace PotisanShellItemLib.PropertySystem;

public class PropertyEnumTypeList : IComUnknownWrapper
{
	private readonly IPropertyEnumTypeList _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public PropertyEnumTypeList(object? o)
	{
		_obj = o == null ? null! : (IPropertyEnumTypeList)o;
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

	public ComResult<uint> CountNoThrow => new(_obj.GetCount(out var x), x);
	public uint Count => CountNoThrow.Value;

	public ComResult<PropertyEnumType> GetAtNothrow(uint index) => new(_obj.GetAt(index, typeof(IPropertyEnumType).GUID, out var x), new(x));
	public PropertyEnumType GetAt(uint index) => GetAtNothrow(index).Value;

	public ComResult<uint> FindMatchingIndexNoThrow(PropVariant value) => new(_obj.FindMatchingIndex(value, out var x), x);
	public uint FindMatchingIndex(PropVariant value) => FindMatchingIndexNoThrow(value).Value;

	public IEnumerable<PropertyEnumType> Items
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetAt(i);
		}
	}
}
