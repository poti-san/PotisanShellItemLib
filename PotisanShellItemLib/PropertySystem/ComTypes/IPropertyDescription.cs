namespace PotisanShellItemLib.PropertySystem.ComTypes;

[ComImport]
[Guid("6f79d558-3e96-4549-a1d1-7d75d2288814")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertyDescription
{
	[PreserveSig]
	int GetPropertyKey(
		[Out] PropertyKey pkey);

	[PreserveSig]
	int GetCanonicalName(
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszName);

	[PreserveSig]
	int GetPropertyType(
		out VarType pvartype);

	[PreserveSig]
	int GetDisplayName(
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszName);

	[PreserveSig]
	int GetEditInvitation(
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszInvite);

	[PreserveSig]
	int GetTypeFlags(
		PROPDESC_TYPE_FLAGS mask,
		out PROPDESC_TYPE_FLAGS ppdtFlags);

	[PreserveSig]
	int GetViewFlags(
		out PROPDESC_VIEW_FLAGS ppdvFlags);

	[PreserveSig]
	int GetDefaultColumnWidth(
		out uint pcxChars);

	[PreserveSig]
	int GetDisplayType(
		out PROPDESC_DISPLAYTYPE pdisplaytype);

	[PreserveSig]
	int GetColumnState(
		out SHCOLSTATE pcsFlags);

	[PreserveSig]
	int GetGroupingRange(
		out PROPDESC_GROUPING_RANGE pgr);

	[PreserveSig]
	int GetRelativeDescriptionType(
		out PROPDESC_RELATIVEDESCRIPTION_TYPE prdt);

	[PreserveSig]
	int GetRelativeDescription(
		PropVariant propvar1,
		PropVariant propvar2,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDesc1,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDesc2);

	[PreserveSig]
	int GetSortDescription(
		out PROPDESC_SORTDESCRIPTION psd);

	[PreserveSig]
	int GetSortDescriptionLabel(
		[MarshalAs(UnmanagedType.Bool)]bool fDescending,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDescription);

	[PreserveSig]
	int GetAggregationType(
		out PROPDESC_AGGREGATION_TYPE paggtype);

	[PreserveSig]
	int GetConditionType(
		out PROPDESC_CONDITION_TYPE pcontype,
		out CONDITION_OPERATION popDefault);

	[PreserveSig]
	int GetEnumTypeList(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)]out object? ppv);

	[PreserveSig]
	int CoerceToCanonicalValue(
		[In,Out] PropVariant ppropvar);

	[PreserveSig]
	int FormatForDisplay(
		PropVariant propvar,
		PROPDESC_FORMAT_FLAGS pdfFlags,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDisplay);

	[PreserveSig]
	int IsValueCanonical(
		PropVariant propvar);
}