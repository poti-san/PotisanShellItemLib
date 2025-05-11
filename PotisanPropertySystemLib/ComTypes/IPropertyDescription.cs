namespace Potisan.Windows.PropertySystem.ComTypes;

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
		PropDescTypeFlag mask,
		out PropDescTypeFlag ppdtFlags);

	[PreserveSig]
	int GetViewFlags(
		out PropDescViewFlag ppdvFlags);

	[PreserveSig]
	int GetDefaultColumnWidth(
		out uint pcxChars);

	[PreserveSig]
	int GetDisplayType(
		out PropDescDisplayType pdisplaytype);

	[PreserveSig]
	int GetColumnState(
		out ShellColumnState pcsFlags);

	[PreserveSig]
	int GetGroupingRange(
		out PropDescGroupingRange pgr);

	[PreserveSig]
	int GetRelativeDescriptionType(
		out PropDescRelativeDescriptionType prdt);

	[PreserveSig]
	int GetRelativeDescription(
		PropVariant propvar1,
		PropVariant propvar2,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDesc1,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDesc2);

	[PreserveSig]
	int GetSortDescription(
		out PropDescSortDescription psd);

	[PreserveSig]
	int GetSortDescriptionLabel(
		[MarshalAs(UnmanagedType.Bool)] bool fDescending,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDescription);

	[PreserveSig]
	int GetAggregationType(
		out PropDescAggregationType paggtype);

	[PreserveSig]
	int GetConditionType(
		out PropDescConditionType pcontype,
		out ConditionOperation popDefault);

	[PreserveSig]
	int GetEnumTypeList(
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int CoerceToCanonicalValue(
		[In, Out] PropVariant ppropvar);

	[PreserveSig]
	int FormatForDisplay(
		PropVariant propvar,
		PropDescFormatFlag pdfFlags,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDisplay);

	[PreserveSig]
	int IsValueCanonical(
		PropVariant propvar);
}