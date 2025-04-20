using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティ記述子。IPropertyDescription COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class PropertyDescription(object? o) : ComUnknownWrapperBase<IPropertyDescription>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropertyKey> PropertyKeyNoThrow
	{
		get
		{
			var x = new PropertyKey();
			return new(_obj.GetPropertyKey(x), x);
		}
	}
	public PropertyKey PropertyKey
		=> PropertyKeyNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> CanonicalNameNoThrow
		=> new(_obj.GetCanonicalName(out var x), x!);

	public string CanonicalName
		=> CanonicalNameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<VarType> PropertyTypeNoThrow
		=> new(_obj.GetPropertyType(out var x), x);

	public VarType PropertyType
		=> PropertyTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DisplayNameNoThrow
		=> new(_obj.GetDisplayName(out var x), x!);

	public string DisplayName
		=> DisplayNameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> EditInvitationNoThrow
		=> new(_obj.GetEditInvitation(out var x), x!);

	public string EditInvitation
		=> EditInvitationNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropDescTypeFlag> TypeFlagsNoThrow
		=> new(_obj.GetTypeFlags((PropDescTypeFlag)0xffffffff, out var x), x);

	public PropDescTypeFlag TypeFlags
		=> TypeFlagsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropDescViewFlag> ViewFlagsNoThrow
		=> new(_obj.GetViewFlags(out var x), x);

	public PropDescViewFlag ViewFlags
		=> ViewFlagsNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> DefaultColumnWidthNoThrow
		=> new(_obj.GetDefaultColumnWidth(out var x), x);

	public uint DefaultColumnWidth
		=> DefaultColumnWidthNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropDescDisplayType> DisplayTypeNoThrow
		=> new(_obj.GetDisplayType(out var x), x);

	public PropDescDisplayType DisplayType
		=> DisplayTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellColumnState> ColumnStateNoThrow
		=> new(_obj.GetColumnState(out var x), x);

	public ShellColumnState ColumnState
		=> ColumnStateNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropDescGroupingRange> GroupingRangeNoThrow
		=> new(_obj.GetGroupingRange(out var x), x);

	public PropDescGroupingRange GroupingRange
		=> GroupingRangeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropDescRelativeDescriptionType> RelativeDescriptionTypeNoThrow
		=> new(_obj.GetRelativeDescriptionType(out var x), x);

	public PropDescRelativeDescriptionType RelativeDescriptionType
		=> RelativeDescriptionTypeNoThrow.Value;

	public ComResult<(string Desc1, string Desc2)> GetRelativeDescriptionNoThrow(PropVariant value1, PropVariant value2)
		=> new(_obj.GetRelativeDescription(value1, value2, out var x1, out var x2), (x1!, x2!));

	public (string Desc1, string Desc2) GetRelativeDescription(PropVariant value1, PropVariant value2)
		=> GetRelativeDescriptionNoThrow(value1, value2).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropDescSortDescription> SortDescriptionNoThrow
		=> new(_obj.GetSortDescription(out var x), x);

	public PropDescSortDescription SortDescription
		=> SortDescriptionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> SortDescriptionLabelAscendingNoThrow
		=> new(_obj.GetSortDescriptionLabel(false, out var x), x!);

	public string SortDescriptionLabelAscending
		=> SortDescriptionLabelAscendingNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> SortDescriptionLabelDescendingNoThrow
		=> new(_obj.GetSortDescriptionLabel(true, out var x), x!);

	public string SortDescriptionLabelDescending
		=> SortDescriptionLabelDescendingNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropDescAggregationType> AggregationTypeNoThrow
		=> new(_obj.GetAggregationType(out var x), x);

	public PropDescAggregationType AggregationTyp
		=> AggregationTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(PropDescConditionType ConditionType, ConditionOperation Operation)> ConditionTypeNoThrow
		=> new(_obj.GetConditionType(out var x1, out var x2), (x1, x2));

	public (PropDescConditionType ConditionType, ConditionOperation Operation) ConditionTyp
		=> ConditionTypeNoThrow.Value;

	public ComResult<PropertyEnumTypeList> GetEnumTypeListNoThrow()
		=> new(_obj.GetEnumTypeList(typeof(IPropertyEnumTypeList).GUID, out var x), new(x));

	public PropertyEnumTypeList GetEnumTypeList()
		=> GetEnumTypeListNoThrow().Value;

	public ComResult CoerceToCanonicalValueNoThrow(PropVariant value)
		=> new(_obj.CoerceToCanonicalValue(value));

	public void CoerceToCanonicalValue(PropVariant value)
		=> CoerceToCanonicalValueNoThrow(value).ThrowIfError();

	public ComResult<string> FormatForDisplayNoThrow(PropVariant value, PropDescFormatFlag flags)
		=> new(_obj.FormatForDisplay(value, flags, out var x), x!);

	public string FormatForDisplay(PropVariant value, PropDescFormatFlag flags)
		=> FormatForDisplayNoThrow(value, flags).Value;

	public ComResult<bool> IsValueCanonicalNoThrow(PropVariant value)
		=> ComResult.HRSuccess(_obj.CoerceToCanonicalValue(value));

	public bool IsValueCanonical(PropVariant value)
		=> IsValueCanonicalNoThrow(value).Value;

	public static ComResult<PropertyDescription> CreateByCanonicalNameNoThrow(string canonicalName)
	{
		[DllImport("propsys.dll", CharSet = CharSet.Unicode)]
		static extern int PSGetPropertyDescriptionByName(string pszCanonicalName, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

		return new(PSGetPropertyDescriptionByName(canonicalName, typeof(IPropertyDescription).GUID, out var x), new(x));
	}

	public static PropertyDescription CreateByCanonicalName(string canonicalName)
		=> CreateByCanonicalNameNoThrow(canonicalName).Value;
}

/// <summary>
/// PROPDESC_ENUMFILTER
/// </summary>
public enum PropDescEnumFilter : uint
{
	All = 0,
	System = 1,
	NonSystem = 2,
	Viewable = 3,
	Queryable = 4,
	InFullTextQuery = 5,
	Column = 6
}

/// <summary>
/// PROPDESC_TYPE_FLAGS
/// </summary>
public enum PropDescTypeFlag : uint
{
	Default = 0,
	MultipleValues = 0x1,
	IsInnate = 0x2,
	IsGroup = 0x4,
	CanGroupBy = 0x8,
	CanStackBy = 0x10,
	IsTreeProperty = 0x20,
	IncludeInFullTextQuery = 0x40,
	IsViewable = 0x80,
	IsQueryable = 0x100,
	CanBePurged = 0x200,
	SearchRawValue = 0x400,
	DontCoerceEmptyStrings = 0x800,
	AlwaysInsupplementalStore = 0x1000,
	IsSystemProperty = 0x80000000,
}

/// <summary>
/// PROPDESC_VIEW_FLAGS
/// </summary>
public enum PropDescViewFlag : uint
{
	Default = 0,
	CenterAlign = 0x1,
	RightAlign = 0x2,
	BeginNewGroup = 0x4,
	FillArea = 0x8,
	SortDescending = 0x10,
	ShowOnlyIfPresent = 0x20,
	ShowByDefault = 0x40,
	ShowInPrimaryList = 0x80,
	ShowInSecondaryList = 0x100,
	HideLabel = 0x200,
	Hidden = 0x800,
	CanWrap = 0x1000,
}

/// <summary>
/// PROPDESC_DISPLAYTYPE
/// </summary>
public enum PropDescDisplayType : uint
{
	String = 0,
	Number = 1,
	Boolean = 2,
	DateTime = 3,
	Enumerated = 4
}

/// <summary>
/// PROPDESC_GROUPING_RANGE
/// </summary>
public enum PropDescGroupingRange
{
	Discrete = 0,
	AlphaNumeric = 1,
	Size = 2,
	Dynamic = 3,
	Date = 4,
	Percent = 5,
	Enumerated = 6
}

/// <summary>
/// PROPDESC_FORMAT_FLAGS
/// </summary>
public enum PropDescFormatFlag
{
	Default = 0,
	PrefixName = 0x1,
	FileName = 0x2,
	AlwaysKB = 0x4,
	ReservedRTL = 0x8,
	ShortTime = 0x10,
	LongTime = 0x20,
	HideTime = 0x40,
	ShortDate = 0x80,
	LongDate = 0x100,
	HideDate = 0x200,
	RelativeDate = 0x400,
	UseEditInvitation = 0x800,
	ReadOnly = 0x1000,
	NoAutoReadingOrder = 0x2000
}

/// <summary>
/// PROPDESC_SORTDESCRIPTION
/// </summary>
public enum PropDescSortDescription : uint
{
	General = 0,
	AToZ = 1,
	LowestHighest = 2,
	SmallestBiggest = 3,
	OldestNewest = 4
}

/// <summary>
/// PROPDESC_RELATIVEDESCRIPTION_TYPE
/// </summary>
public enum PropDescRelativeDescriptionType : uint
{
	General = 0,
	Date = 1,
	Size = 2,
	Count = 3,
	Revision = 4,
	Length = 5,
	Duration = 6,
	Speed = 7,
	Rate = 8,
	Rating = 9,
	Priority = 10
}

/// <summary>
/// PROPDESC_AGGREGATION_TYPE
/// </summary>
public enum PropDescAggregationType : uint
{
	Default = 0,
	First = 1,
	Sum = 2,
	Average = 3,
	DateRange = 4,
	Union = 5,
	Max = 6,
	Min = 7
}

/// <summary>
/// PROPDESC_CONDITION_TYPE
/// </summary>
public enum PropDescConditionType : uint
{
	None = 0,
	String = 1,
	Size = 2,
	DateTime = 3,
	Boolean = 4,
	Number = 5
}

/// <summary>
/// SHCOLSTATE
/// </summary>
public enum ShellColumnState : uint
{
	Default = 0,
	TypeString = 0x1,
	TypeInteger = 0x2,
	TypeDate = 0x3,
	OneByDefault = 0x10,
	Slow = 0x20,
	Extended = 0x40,
	SecondaryUI = 0x80,
	Hidden = 0x100,
	PreferVarCompare = 0x200,
	PreferFormatCompare = 0x400,
	NoSortByFolderness = 0x800,
	ViewOnly = 0x10000,
	BatchRead = 0x20000,
	NoGroupBy = 0x40000,
	FixedWidth = 0x1000,
	NoDpiScale = 0x2000,
	FixedRatio = 0x4000,
}

/// <summary>
/// CONDITION_OPERATION
/// </summary>
public enum ConditionOperation : uint
{
	Implicit = 0,
	Equal = (Implicit + 1),
	NotEqual = (Equal + 1),
	LessThan = (NotEqual + 1),
	GreaterThan = (LessThan + 1),
	LessThanOrEqual = (GreaterThan + 1),
	GreaterThanOrEqual = (LessThanOrEqual + 1),
	ValueStartsWith = (GreaterThanOrEqual + 1),
	ValueEndsWith = (ValueStartsWith + 1),
	ValueContains = (ValueEndsWith + 1),
	ValueNotContains = (ValueContains + 1),
	DosWildcards = (ValueNotContains + 1),
	WordEqual = (DosWildcards + 1),
	WordStartsWith = (WordEqual + 1),
	ApplicationSpecific = (WordStartsWith + 1)
}