using PotisanShellItemLib.PropertySystem.ComTypes;

namespace PotisanShellItemLib.PropertySystem;

public class PropertyEnumType : IComUnknownWrapper
{
	private readonly IPropertyEnumType _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public PropertyEnumType(object? o)
	{
		_obj = o == null ? null! : (IPropertyEnumType)o;
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

	public ComResult<PropEnumType> EnumTypeNoThrow => new(_obj.GetEnumType(out var x), x);
	public PropEnumType EnumType => EnumTypeNoThrow.Value;

	public ComResult<PropVariant> ValueNoThrow
	{
		get
		{
			var x = new PropVariant();
			return new(_obj.GetValue(x), x);
		}
	}
	public PropVariant Value => ValueNoThrow.Value;

	public ComResult<PropVariant> RangeMinValueNoThrow
	{
		get
		{
			var x = new PropVariant();
			return new(_obj.GetRangeMinValue(x), x);
		}
	}
	public PropVariant RangeMinValue => RangeMinValueNoThrow.Value;

	public ComResult<PropVariant> RangeSetValueNoThrow
	{
		get
		{
			var x = new PropVariant();
			return new(_obj.GetRangeSetValue(x), x);
		}
	}
	public PropVariant RangeSetValue => RangeSetValueNoThrow.Value;

	public ComResult<string> DisplayTextNoThrow => new(_obj.GetDisplayText(out var x), x);
	public string DisplayText => DisplayTextNoThrow.Value;
}

/// <summary>
/// PROPENUMTYPE
/// </summary>
public enum PropEnumType : uint
{
	DiscreteValue = 0,
	RangedValue = 1,
	DefaultValue = 2,
	EndRange = 3
}