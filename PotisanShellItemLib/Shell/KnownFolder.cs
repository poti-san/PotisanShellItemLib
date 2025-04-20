namespace PotisanShellItemLib.Shell;

[Flags]
public enum KnownFolderFlag : uint
{
	Default = 0x00000000,
    ForceAppDataRedirection = 0x00080000,
    ReturnFilterRedirectionTarget = 0x00040000,
    ForcePackageRedirection = 0x00020000,
    NoPackageRedirection = 0x00010000,
    ForceAppContainerRedirection = 0x00020000,
    NoAppContainerRedirection = 0x00010000,
	Create = 0x00008000,
	FontVerify = 0x00004000,
	DontUnexpand = 0x00002000,
	NoAlias = 0x00001000,
	Initialize = 0x00000800,
	DefaultPath = 0x00000400,
	NotParentRelative = 0x00000200,
	SimpleIDList = 0x00000100,
	AliasOnly = 0x80000000,
}