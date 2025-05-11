namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("ca724e8a-c3e6-442b-88a4-6fb0db8035a3")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertySystem
{
	[PreserveSig]
	int GetPropertyDescription(
		PropertyKey propkey,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int GetPropertyDescriptionByName(
		[MarshalAs(UnmanagedType.LPWStr)] string pszCanonicalName,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int GetPropertyDescriptionListFromString(
		[MarshalAs(UnmanagedType.LPWStr)] string pszPropList,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int EnumeratePropertyDescriptions(
		PropDescEnumFilter filterOn,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int FormatForDisplay(
		PropertyKey key,
		PropVariant propvar,
		PropDescFormatFlag pdff,
		[MarshalAs(UnmanagedType.LPWStr)] ref char pszText,
		uint cchText);

	[PreserveSig]
	int FormatForDisplayAlloc(
		PropertyKey key,
		PropVariant propvar,
		PropDescFormatFlag pdff,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppszDisplay);

	[PreserveSig]
	int RegisterPropertySchema(
		[MarshalAs(UnmanagedType.LPWStr)] string pszPath);

	[PreserveSig]
	int UnregisterPropertySchema(
		[MarshalAs(UnmanagedType.LPWStr)] string pszPath);

	[PreserveSig]
	int RefreshPropertySchema();
}