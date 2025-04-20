using PotisanShellItemLib.PropertySystem.ComTypes;

namespace PotisanShellItemLib.PropertySystem;

public class PropertyDescription : IComUnknownWrapper
{
	private readonly IPropertyDescription _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public PropertyDescription(object? o)
	{
		_obj = o == null ? null! : (IPropertyDescription)o;
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

	public ComResult<PropertyKey> PropertyKeyNoThrow
	{
		get
		{
			var x = new PropertyKey();
			return new(_obj.GetPropertyKey(x), x);
		}
	}
	public PropertyKey PropertyKey => PropertyKeyNoThrow.Value;

	public ComResult<string> CanonicalNameNoThrow => new(_obj.GetCanonicalName(out var x), x);
	public string CanonicalName => CanonicalNameNoThrow.Value;

	public ComResult<VarType> PropertyTypeNoThrow => new(_obj.GetPropertyType(out var x), x);
	public VarType PropertyType => PropertyTypeNoThrow.Value;

	public ComResult<string> DisplayNameNoThrow => new(_obj.GetDisplayName(out var x), x);
	public string DisplayName => DisplayNameNoThrow.Value;

	public ComResult<string> EditInvitationNoThrow => new(_obj.GetEditInvitation(out var x), x);
	public string EditInvitation => EditInvitationNoThrow.Value;

	public ComResult<PROPDESC_TYPE_FLAGS> TypeFlagsNoThrow => new(_obj.GetTypeFlags((PROPDESC_TYPE_FLAGS)0xffffffff, out var x), x);
	public PROPDESC_TYPE_FLAGS TypeFlags => TypeFlagsNoThrow.Value;

	public ComResult<PROPDESC_VIEW_FLAGS> ViewFlagsNoThrow => new(_obj.GetViewFlags(out var x), x);
	public PROPDESC_VIEW_FLAGS ViewFlags => ViewFlagsNoThrow.Value;

	public ComResult<uint> DefaultColumnWidthNoThrow => new(_obj.GetDefaultColumnWidth(out var x), x);
	public uint DefaultColumnWidth => DefaultColumnWidthNoThrow.Value;

	public ComResult<PROPDESC_DISPLAYTYPE> DisplayTypeNoThrow => new(_obj.GetDisplayType(out var x), x);
	public PROPDESC_DISPLAYTYPE DisplayType => DisplayTypeNoThrow.Value;

	public ComResult<SHCOLSTATE> ColumnStateNoThrow => new(_obj.GetColumnState(out var x), x);
	public SHCOLSTATE ColumnState => ColumnStateNoThrow.Value;

	public ComResult<PROPDESC_GROUPING_RANGE> GroupingRangeNoThrow => new(_obj.GetGroupingRange(out var x), x);
	public PROPDESC_GROUPING_RANGE GroupingRange => GroupingRangeNoThrow.Value;

	public ComResult<PROPDESC_RELATIVEDESCRIPTION_TYPE> RelativeDescriptionTypeNoThrow => new(_obj.GetRelativeDescriptionType(out var x), x);
	public PROPDESC_RELATIVEDESCRIPTION_TYPE RelativeDescriptionType => RelativeDescriptionTypeNoThrow.Value;

	public ComResult<(string Desc1, string Desc2)> GetRelativeDescriptionNoThrow(PropVariant value1, PropVariant value2)
		=> new(_obj.GetRelativeDescription(value1, value2, out var x1, out var x2), (x1, x2));
	public (string Desc1, string Desc2) GetRelativeDescription(PropVariant value1, PropVariant value2)
		=> GetRelativeDescriptionNoThrow(value1, value2).Value;

	public ComResult<PROPDESC_SORTDESCRIPTION> SortDescriptionNoThrow => new(_obj.GetSortDescription(out var x), x);
	public PROPDESC_SORTDESCRIPTION SortDescription => SortDescriptionNoThrow.Value;

	public ComResult<string> SortDescriptionLabelAscendingNoThrow => new(_obj.GetSortDescriptionLabel(false, out var x), x);
	public string SortDescriptionLabelAscending => SortDescriptionLabelAscendingNoThrow.Value;

	public ComResult<string> SortDescriptionLabelDescendingNoThrow => new(_obj.GetSortDescriptionLabel(true, out var x), x);
	public string SortDescriptionLabelDescending => SortDescriptionLabelDescendingNoThrow.Value;

	public ComResult<PROPDESC_AGGREGATION_TYPE> AggregationTypeNoThrow => new(_obj.GetAggregationType(out var x), x);
	public PROPDESC_AGGREGATION_TYPE AggregationTyp => AggregationTypeNoThrow.Value;

	public ComResult<(PROPDESC_CONDITION_TYPE ConditionType, CONDITION_OPERATION Operation)> ConditionTypeNoThrow
		=> new(_obj.GetConditionType(out var x1, out var x2), (x1, x2));
	public (PROPDESC_CONDITION_TYPE ConditionType, CONDITION_OPERATION Operation) ConditionType => ConditionTypeNoThrow.Value;

	// TODO
	//[PreserveSig]
	//int GetEnumTypeList(
	//	in Guid riid,
	//	[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	public ComResult CoerceToCanonicalValueNoThrow(PropVariant value) => new(_obj.CoerceToCanonicalValue(value));
	public void CoerceToCanonicalValue(PropVariant value) => CoerceToCanonicalValueNoThrow(value).ThrowIfError();

	public ComResult<string> FormatForDisplayNoThrow(PropVariant value, PROPDESC_FORMAT_FLAGS flags)
		=> new(_obj.FormatForDisplay(value, flags, out var x), x);
	public string FormatForDisplay(PropVariant value, PROPDESC_FORMAT_FLAGS flags)
		=> FormatForDisplayNoThrow(value, flags).Value;

	public ComResult<bool> IsValueCanonicalNoThrow(PropVariant value) => ComResult.HRSuccess(_obj.CoerceToCanonicalValue(value));
	public bool IsValueCanonical(PropVariant value) => IsValueCanonicalNoThrow(value).Value;
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
public enum PROPDESC_TYPE_FLAGS : uint
{
	PDTF_DEFAULT = 0,
	PDTF_MULTIPLEVALUES = 0x1,
	PDTF_ISINNATE = 0x2,
	PDTF_ISGROUP = 0x4,
	PDTF_CANGROUPBY = 0x8,
	PDTF_CANSTACKBY = 0x10,
	PDTF_ISTREEPROPERTY = 0x20,
	PDTF_INCLUDEINFULLTEXTQUERY = 0x40,
	PDTF_ISVIEWABLE = 0x80,
	PDTF_ISQUERYABLE = 0x100,
	PDTF_CANBEPURGED = 0x200,
	PDTF_SEARCHRAWVALUE = 0x400,
	PDTF_DONTCOERCEEMPTYSTRINGS = 0x800,
	PDTF_ALWAYSINSUPPLEMENTALSTORE = 0x1000,
	PDTF_ISSYSTEMPROPERTY = 0x80000000,
}

/// <summary>
/// PROPDESC_VIEW_FLAGS
/// </summary>
public enum PROPDESC_VIEW_FLAGS : uint
{
	PDVF_DEFAULT = 0,
	PDVF_CENTERALIGN = 0x1,
	PDVF_RIGHTALIGN = 0x2,
	PDVF_BEGINNEWGROUP = 0x4,
	PDVF_FILLAREA = 0x8,
	PDVF_SORTDESCENDING = 0x10,
	PDVF_SHOWONLYIFPRESENT = 0x20,
	PDVF_SHOWBYDEFAULT = 0x40,
	PDVF_SHOWINPRIMARYLIST = 0x80,
	PDVF_SHOWINSECONDARYLIST = 0x100,
	PDVF_HIDELABEL = 0x200,
	PDVF_HIDDEN = 0x800,
	PDVF_CANWRAP = 0x1000,
	PDVF_MASK_ALL = 0x1bff
}

/// <summary>
/// PROPDESC_DISPLAYTYPE
/// </summary>
public enum PROPDESC_DISPLAYTYPE : uint
{
	PDDT_STRING = 0,
	PDDT_NUMBER = 1,
	PDDT_BOOLEAN = 2,
	PDDT_DATETIME = 3,
	PDDT_ENUMERATED = 4
}

/// <summary>
/// PROPDESC_GROUPING_RANGE
/// </summary>
public enum PROPDESC_GROUPING_RANGE
{
	PDGR_DISCRETE = 0,
	PDGR_ALPHANUMERIC = 1,
	PDGR_SIZE = 2,
	PDGR_DYNAMIC = 3,
	PDGR_DATE = 4,
	PDGR_PERCENT = 5,
	PDGR_ENUMERATED = 6
}

/// <summary>
/// PROPDESC_FORMAT_FLAGS
/// </summary>
public enum PROPDESC_FORMAT_FLAGS
{
	PDFF_DEFAULT = 0,
	PDFF_PREFIXNAME = 0x1,
	PDFF_FILENAME = 0x2,
	PDFF_ALWAYSKB = 0x4,
	PDFF_RESERVED_RIGHTTOLEFT = 0x8,
	PDFF_SHORTTIME = 0x10,
	PDFF_LONGTIME = 0x20,
	PDFF_HIDETIME = 0x40,
	PDFF_SHORTDATE = 0x80,
	PDFF_LONGDATE = 0x100,
	PDFF_HIDEDATE = 0x200,
	PDFF_RELATIVEDATE = 0x400,
	PDFF_USEEDITINVITATION = 0x800,
	PDFF_READONLY = 0x1000,
	PDFF_NOAUTOREADINGORDER = 0x2000
}

/// <summary>
/// PROPDESC_SORTDESCRIPTION
/// </summary>
public enum PROPDESC_SORTDESCRIPTION : uint
{
	PDSD_GENERAL = 0,
	PDSD_A_Z = 1,
	PDSD_LOWEST_HIGHEST = 2,
	PDSD_SMALLEST_BIGGEST = 3,
	PDSD_OLDEST_NEWEST = 4
}

/// <summary>
/// PROPDESC_RELATIVEDESCRIPTION_TYPE
/// </summary>
public enum PROPDESC_RELATIVEDESCRIPTION_TYPE : uint
{
	PDRDT_GENERAL = 0,
	PDRDT_DATE = 1,
	PDRDT_SIZE = 2,
	PDRDT_COUNT = 3,
	PDRDT_REVISION = 4,
	PDRDT_LENGTH = 5,
	PDRDT_DURATION = 6,
	PDRDT_SPEED = 7,
	PDRDT_RATE = 8,
	PDRDT_RATING = 9,
	PDRDT_PRIORITY = 10
}

/// <summary>
/// PROPDESC_AGGREGATION_TYPE
/// </summary>
public enum PROPDESC_AGGREGATION_TYPE : uint
{
	PDAT_DEFAULT = 0,
	PDAT_FIRST = 1,
	PDAT_SUM = 2,
	PDAT_AVERAGE = 3,
	PDAT_DATERANGE = 4,
	PDAT_UNION = 5,
	PDAT_MAX = 6,
	PDAT_MIN = 7
}

/// <summary>
/// PROPDESC_CONDITION_TYPE
/// </summary>
public enum PROPDESC_CONDITION_TYPE : uint
{
	PDCOT_NONE = 0,
	PDCOT_STRING = 1,
	PDCOT_SIZE = 2,
	PDCOT_DATETIME = 3,
	PDCOT_BOOLEAN = 4,
	PDCOT_NUMBER = 5
}


/// <summary>
/// SHCOLSTATE
/// </summary>
public enum SHCOLSTATE : uint
{
	SHCOLSTATE_DEFAULT = 0,
	SHCOLSTATE_TYPE_STR = 0x1,
	SHCOLSTATE_TYPE_INT = 0x2,
	SHCOLSTATE_TYPE_DATE = 0x3,
	SHCOLSTATE_TYPEMASK = 0xf,
	SHCOLSTATE_ONBYDEFAULT = 0x10,
	SHCOLSTATE_SLOW = 0x20,
	SHCOLSTATE_EXTENDED = 0x40,
	SHCOLSTATE_SECONDARYUI = 0x80,
	SHCOLSTATE_HIDDEN = 0x100,
	SHCOLSTATE_PREFER_VARCMP = 0x200,
	SHCOLSTATE_PREFER_FMTCMP = 0x400,
	SHCOLSTATE_NOSORTBYFOLDERNESS = 0x800,
	SHCOLSTATE_VIEWONLY = 0x10000,
	SHCOLSTATE_BATCHREAD = 0x20000,
	SHCOLSTATE_NO_GROUPBY = 0x40000,
	SHCOLSTATE_FIXED_WIDTH = 0x1000,
	SHCOLSTATE_NODPISCALE = 0x2000,
	SHCOLSTATE_FIXED_RATIO = 0x4000,
	SHCOLSTATE_DISPLAYMASK = 0xf000
}

/// <summary>
/// CONDITION_OPERATION
/// </summary>
public enum CONDITION_OPERATION : uint
{
	COP_IMPLICIT = 0,
	COP_EQUAL = (COP_IMPLICIT + 1),
	COP_NOTEQUAL = (COP_EQUAL + 1),
	COP_LESSTHAN = (COP_NOTEQUAL + 1),
	COP_GREATERTHAN = (COP_LESSTHAN + 1),
	COP_LESSTHANOREQUAL = (COP_GREATERTHAN + 1),
	COP_GREATERTHANOREQUAL = (COP_LESSTHANOREQUAL + 1),
	COP_VALUE_STARTSWITH = (COP_GREATERTHANOREQUAL + 1),
	COP_VALUE_ENDSWITH = (COP_VALUE_STARTSWITH + 1),
	COP_VALUE_CONTAINS = (COP_VALUE_ENDSWITH + 1),
	COP_VALUE_NOTCONTAINS = (COP_VALUE_CONTAINS + 1),
	COP_DOSWILDCARDS = (COP_VALUE_NOTCONTAINS + 1),
	COP_WORD_EQUAL = (COP_DOSWILDCARDS + 1),
	COP_WORD_STARTSWITH = (COP_WORD_EQUAL + 1),
	COP_APPLICATION_SPECIFIC = (COP_WORD_STARTSWITH + 1)
}