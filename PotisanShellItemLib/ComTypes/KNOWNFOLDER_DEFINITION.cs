#pragma warning disable CA1707 // 識別子はアンダースコアを含むことはできません

namespace Potisan.Windows.Shell.ComTypes;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct KNOWNFOLDER_DEFINITION : IDisposable
{
	public KnownFolderCategory category;
	public nint pszName;
	public nint pszDescription;
	public Guid fidParent;
	public nint pszRelativePath;
	public nint pszParsingName;
	public nint pszTooltip;
	public nint pszLocalizedName;
	public nint pszIcon;
	public nint pszSecurity;
	public uint dwAttributes;
	public KnownFolderDefinitionFlag kfdFlags;
	public FolderTypeID ftidType;

	public KNOWNFOLDER_DEFINITION(KnownFolderDefinition x)
	{
		category = x.Category;
		pszName = Marshal.StringToCoTaskMemUni(x.Name);
		fidParent = x.ParentFolderID;
		pszRelativePath = Marshal.StringToCoTaskMemUni(x.RelativePath);
		pszParsingName = Marshal.StringToCoTaskMemUni(x.ParsingName);
		pszTooltip = Marshal.StringToCoTaskMemUni(x.Tooltip);
		pszLocalizedName = Marshal.StringToCoTaskMemUni(x.LocalizedName);
		pszIcon = Marshal.StringToCoTaskMemUni(x.Icon);
		pszSecurity = Marshal.StringToCoTaskMemUni(x.Security);
		dwAttributes = x.Attributes;
		kfdFlags = x.Flags;
		ftidType = x.FolderTypeID;
	}

	public readonly KnownFolderDefinition ToManaged()
		=> new(
			category,
			Marshal.PtrToStringUni(pszName) ?? "",
			Marshal.PtrToStringUni(pszDescription) ?? "",
			fidParent,
			Marshal.PtrToStringUni(pszRelativePath) ?? "",
			Marshal.PtrToStringUni(pszParsingName) ?? "",
			Marshal.PtrToStringUni(pszTooltip) ?? "",
			Marshal.PtrToStringUni(pszLocalizedName) ?? "",
			Marshal.PtrToStringUni(pszIcon) ?? "",
			Marshal.PtrToStringUni(pszSecurity) ?? "",
			dwAttributes,
			kfdFlags,
			ftidType);

#pragma warning disable IDE0251 // メンバーを 'readonly' にする
	public void Dispose()
	{
		Marshal.FreeCoTaskMem(pszName);
		Marshal.FreeCoTaskMem(pszDescription);
		Marshal.FreeCoTaskMem(pszRelativePath);
		Marshal.FreeCoTaskMem(pszParsingName);
		Marshal.FreeCoTaskMem(pszTooltip);
		Marshal.FreeCoTaskMem(pszLocalizedName);
		Marshal.FreeCoTaskMem(pszIcon);
		Marshal.FreeCoTaskMem(pszSecurity);
	}
#pragma warning restore IDE0251 // メンバーを 'readonly' にする
}
