using PotisanShellItemLib.PropertySystem.ComTypes;

namespace PotisanShellItemLib.PropertySystem;

public class PropertyDescriptionList : IComUnknownWrapper
{
	private readonly IPropertyDescriptionList _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public PropertyDescriptionList(object? o)
	{
		_obj = o == null ? null! : (IPropertyDescriptionList)o;
	}

	public object? WrappedObject => _obj;

	public void Dispose()
	{
		if (_obj != null)
			Marshal.FinalReleaseComObject(_obj);
		GC.SuppressFinalize(this);
	}

	public ComResult<uint> CountNoThrow => new(_obj.GetCount(out var x), x);
	public uint Count => CountNoThrow.Value;

	public ComResult<PropertyDescription> GetAtNoThrow(uint index)
		=> new(_obj.GetAt(index, typeof(IPropertyDescription).GUID, out var x), new(x));
	public PropertyDescription GetAt(uint index) => GetAtNoThrow(index).Value;

	public IEnumerable<PropertyDescription> Items
	{
		get
		{
			var c = Count;
			for (uint i = 0; i < c; i++)
				yield return GetAt(i);
		}
	}
}