namespace Potisan.Windows.Shell.ComTypes;

[ComImport]
[Guid("1df0d7f1-b267-4d28-8b10-12e23202a5c4")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IItemNameLimits
{
	[PreserveSig]
	int GetValidCharacters(
		[MarshalAs(UnmanagedType.LPWStr)] out string ppwszValidChars,
		[MarshalAs(UnmanagedType.LPWStr)] out string ppwszInvalidChars);

	[PreserveSig]
	int GetMaxLength(
		string pszName,
		out int piMaxNameLen);
}
