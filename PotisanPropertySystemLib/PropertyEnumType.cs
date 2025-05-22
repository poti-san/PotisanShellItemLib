using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// 列挙型プロパティの値情報。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IPropertyEnumType</c> COMインターフェイスのラッパーです。
/// </remarks>
public class PropertyEnumType(object? o) : ComUnknownWrapperBase<IPropertyEnumType>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropEnumType> EnumTypeNoThrow
		=> new(_obj.GetEnumType(out var x), x);

	public PropEnumType EnumType
		=> EnumTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropVariant> ValueNoThrow
	{
		get
		{
			var x = new PropVariant();
			return new(_obj.GetValue(x), x);
		}
	}

	public PropVariant Value
		=> ValueNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropVariant> RangeMinValueNoThrow
	{
		get
		{
			var x = new PropVariant();
			return new(_obj.GetRangeMinValue(x), x);
		}
	}

	public PropVariant RangeMinValue
		=> RangeMinValueNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropVariant> RangeSetValueNoThrow
	{
		get
		{
			var x = new PropVariant();
			return new(_obj.GetRangeSetValue(x), x);
		}
	}

	public PropVariant RangeSetValue
		=> RangeSetValueNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DisplayTextNoThrow
		=> new(_obj.GetDisplayText(out var x), x);

	public string DisplayText
		=> DisplayTextNoThrow.Value;
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