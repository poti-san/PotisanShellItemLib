using System.Collections.Immutable;

using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// 既知フォルダ管理。
/// </summary>
/// <param name="o"></param>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.Shell;
///
///using var kfManager = KnownFolderManager.Create();
///foreach (var folder in kfManager.FolderEnumerable)
///{
///	using var item = folder.ShellItem2NoThrow.Or(null!);
///	Console.WriteLine($"{folder.ID}, {folder.Category}, {item?.NormalDisplayName}");
///}]]>
/// </code>
/// </example>
public class KnownFolderManager(object? o) : ComUnknownWrapperBase<IKnownFolderManager>(o)
{
	public static ComResult<KnownFolderManager> CreateNoThrow()
	{
		Guid CLSID_KnownFolderManager = new("4df0c730-df9d-4ae3-9153-aa6b82e9795a");
		return ComHelper.CreateInstanceNoThrow<KnownFolderManager, IKnownFolderManager>(CLSID_KnownFolderManager);
	}

	public static KnownFolderManager Create()
		=> CreateNoThrow().Value;

	public ComResult<Guid> GetFolderIDFromCsidlNoThrow(int csidl)
		=> new(_obj.FolderIdFromCsidl(csidl, out var x), x);

	public Guid GetFolderIDFromCsidl(int csidl)
		=> GetFolderIDFromCsidlNoThrow(csidl).Value;

	public ComResult<int> GetCsidlFromFolderIDNoThrow(in Guid folderId)
		=> new(_obj.FolderIdToCsidl(folderId, out var x), x);

	public int GetCsidlFromFolderID(in Guid folderId)
		=> GetCsidlFromFolderIDNoThrow(folderId).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid[]> FolderIDsNoThrow
		=> new(_obj.GetFolderIds(out var x, out _), x);

	public Guid[] FolderIDs
		=> FolderIDsNoThrow.Value;

	public IEnumerable<KnownFolder> FolderEnumerable
		=> FolderIDs.Select(folderId => GetFolder(folderId));

	public ImmutableArray<KnownFolder> Folders
		=> [.. FolderEnumerable];

	public ComResult<KnownFolder> GetFolderNoThrow(in Guid folderId)
		=> new(_obj.GetFolder(folderId, out var x), new(x));

	public KnownFolder GetFolder(in Guid folderId)
		=> GetFolderNoThrow(folderId).Value;

	public ComResult<KnownFolder> GetFolderNoThrow(string canonicalName)
		=> new(_obj.GetFolderByName(canonicalName, out var x), new(x));

	public KnownFolder GetFolder(string canonicalName)
		=> GetFolderNoThrow(canonicalName).Value;

	public ComResult RegisterFolderNoThrow(in Guid folderId, KnownFolderDefinition definition)
	{
		using var kfd = new KNOWNFOLDER_DEFINITION(definition);
		return new(_obj.RegisterFolder(folderId, kfd));
	}

	public void RegisterFolder(in Guid folderId, KnownFolderDefinition definition)
		=> RegisterFolderNoThrow(folderId, definition).ThrowIfError();

	public ComResult UnregisterFolderNoThrow(in Guid folderId)
		=> new(_obj.UnregisterFolder(folderId));

	public void UnregisterFolder(in Guid folderId)
		=> UnregisterFolderNoThrow(folderId).ThrowIfError();

	public ComResult<KnownFolder> FindFolderNoThrow(string path, FffpMode mode = 0)
		=> new(_obj.FindFolderFromPath(path, mode, out var x), new(x));

	public KnownFolder FindFolder(string path, FffpMode mode = 0)
		=> FindFolderNoThrow(path, mode).Value;

	public ComResult<KnownFolder> FindFolderNoThrow(SafeHandle pidl)
		=> new(_obj.FindFolderFromIDList(pidl.DangerousGetHandle(), out var x), new(x));

	public KnownFolder FindFolder(SafeHandle pidl)
		=> FindFolderNoThrow(pidl).Value;

	public ComResult<string> RedirectNoThrow(
		in Guid folderId,
		string? targetPath,
		KnownFolderRedirectFlag flags = 0,
		Guid[]? exclusionFolderIds = null,
		nint windowHandle = 0)
		=> new(_obj.Redirect(folderId, windowHandle, flags, targetPath,
			checked((uint)(exclusionFolderIds?.Length ?? 0)), exclusionFolderIds, out var x), new(x));

	public string Redirect(
		in Guid folderId,
		string? targetPath,
		KnownFolderRedirectFlag flags = 0,
		Guid[]? exclusionFolderIds = null,
		nint windowHandle = 0)
		=> RedirectNoThrow(folderId, targetPath, flags, exclusionFolderIds, windowHandle).Value;
}

/// <summary>
/// FFFP_MODE
/// </summary>
public enum FffpMode
{
	ExactMatch = 0,
	NearestParentMatch,
}
