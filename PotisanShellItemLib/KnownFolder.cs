using Potisan.Windows.Com.SafeHandles;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// 既知フォルダ。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para>作成には<see cref="KnownFolderManager"/>を使用します。</para>
/// <para><c>IKnownFolder</c> COMインターフェイスのラッパーです。</para>
/// </remarks>
public class KnownFolder(object? o) : ComUnknownWrapperBase<IKnownFolder>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> IDNoThrow
		=> new(_obj.GetId(out var x), x);

	public Guid ID
		=> IDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<KnownFolderCategory> CategoryNoThrow
		=> new(_obj.GetCategory(out var x), x);

	public KnownFolderCategory Category
		=> CategoryNoThrow.Value;

	public ComResult<ShellItem> GetShellItemNoThrow(KnownFolderFlag flags = 0)
		=> new(_obj.GetShellItem((uint)flags, typeof(IShellItem).GUID, out var x), new(x));

	public ShellItem GetShellItem(KnownFolderFlag flags = 0)
		=> GetShellItemNoThrow(flags).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItem> ShellItemNoThrow
		=> GetShellItemNoThrow(0);

	public ShellItem ShellItem
		=> ShellItemNoThrow.Value;

	public ComResult<ShellItem2> GetShellItem2NoThrow(KnownFolderFlag flags = 0)
		=> IComUnknownWrapper.Wrap<ShellItem2, ShellItem>(GetShellItemNoThrow(flags));

	public ShellItem GetShellItem2(KnownFolderFlag flags = 0)
		=> GetShellItem2NoThrow(flags).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ShellItem2> ShellItem2NoThrow
		=> GetShellItem2NoThrow(0);

	public ShellItem ShellItem2
		=> ShellItem2NoThrow.Value;

	public ComResult<string> GetPathNoThrow(KnownFolderFlag flags = 0)
		=> new(_obj.GetPath((uint)flags, out var x), x);

	public ComResult SetPathNoThrow(string value, KnownFolderFlag flags = 0)
		=> new(_obj.SetPath((uint)flags, value));

	public string GetPath(KnownFolderFlag flags = 0)
		=> GetPathNoThrow(flags).Value;

	public void SetPath(string value, KnownFolderFlag flags = 0)
		=> SetPathNoThrow(value, flags).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> PathNoThrow
		=> GetPathNoThrow(0);

	public string Path
	{
		get => PathNoThrow.Value;
		set => SetPathNoThrow(value).ThrowIfError();
	}

	public ComResult<SafeCoTaskMemHandle> GetIDListNoThrow(KnownFolderFlag flags = 0)
		=> new(_obj.GetIDList((uint)flags, out var x), new(x, true));

	public SafeCoTaskMemHandle GetIDList(KnownFolderFlag flags = 0)
		=> GetIDListNoThrow(flags).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<SafeCoTaskMemHandle> IDListNoThrow
		=> GetIDListNoThrow(0);

	public SafeCoTaskMemHandle IDList
		=> IDListNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<FolderTypeID> FolderTypeNoThrow
		=> new(_obj.GetFolderType(out var x), x);

	public FolderTypeID FolderType
		=> FolderTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<KnownFolderRedirectionCapability> RedirectionCapabilitiesNoThrow
		=> new(_obj.GetRedirectionCapabilities(out var x), x);

	public KnownFolderRedirectionCapability RedirectionCapabilities
		=> RedirectionCapabilitiesNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<KnownFolderDefinition> FolderDefinitionNoThrow
	{
		get
		{
			var hr = _obj.GetFolderDefinition(out var x);
			using (x)
			{
				return new(hr, x.ToManaged());
			}
		}
	}

	public KnownFolderDefinition FolderDefinition
		=> FolderDefinitionNoThrow.Value;
}

/// <summary>
/// 既知フォルダのフラグ。
/// </summary>
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

/// <summary>
/// KF_CATEGORY
/// </summary>
public enum KnownFolderCategory : uint
{
	Virtual = 1,
	Fixed = 2,
	Common = 3,
	PerUser = 4,
}

/// <summary>
/// KF_DEFINITION_FLAGS
/// </summary>
[Flags]
public enum KnownFolderDefinitionFlag : uint
{
	LocalRedirectOnly = 0x2,
	Roamable = 0x4,
	PreCreate = 0x8,
	Stream = 0x10,
	PublishExpandedPath = 0x20,
	NoRedirectUI = 0x40,
}

/// <summary>
/// KF_REDIRECT_FLAGS
/// </summary>
[Flags]
public enum KnownFolderRedirectFlag : uint
{
	UserExclusive = 0x1,
	CopySourceDacl = 0x2,
	OwnerUser = 0x4,
	SetOwnerExplicit = 0x8,
	CheckOnly = 0x10,
	RedirectWithUI = 0x20,
	UnPin = 0x40,
	Pin = 0x80,
	CopyContents = 0x200,
	DeleteSourceContents = 0x400,
	ExcludeAllKnownSubFolders = 0x800,
}

/// <summary>
/// KF_REDIRECTION_CAPABILITIES
/// </summary>
[Flags]
public enum KnownFolderRedirectionCapability : uint
{
	AllowAll = 0xff,
	Redirectable = 0x1,
	DenyAll = 0xfff00,
	DenyPolicyRedirected = 0x100,
	DenyPolicy = 0x200,
	DenyPermissions = 0x400,
}

public record class KnownFolderDefinition(
	KnownFolderCategory Category,
	string Name,
	string Description,
	Guid ParentFolderID,
	string RelativePath,
	string ParsingName,
	string Tooltip,
	string LocalizedName,
	string Icon,
	string Security,
	uint Attributes,
	KnownFolderDefinitionFlag Flags,
	FolderTypeID FolderTypeID);
