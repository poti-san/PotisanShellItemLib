using System.ComponentModel;

using Potisan.Windows.Com.SafeHandles;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// ショートカットファイル（シェルリンク）のデータリスト。
/// </summary>
/// <param name="o"></param>
public sealed class ShellLinkDataList(object? o) : ComUnknownWrapperBase<IShellLinkDataList>(o)
{
	// TODO データブロックの操作
	private ComResult AddDataBlockNoThrow(ReadOnlySpan<byte> dataBlock)
		=> new(_obj.AddDataBlock(MemoryMarshal.GetReference(dataBlock)));

	public ComResult<SafeCoTaskMemHandle> CopyDataBlockNoThrow(uint signature)
		=> new(_obj.CopyDataBlock(signature, out var x), new(x, true));

	public ComResult RemoveDataBlockNoThrow(uint signature)
		=> new(_obj.RemoveDataBlock(signature));

	public void RemoveDataBlock(uint signature)
		=> RemoveDataBlockNoThrow(signature).ThrowIfError();

	public ComResult<ShellLinkDataListFlag> FlagsNoThrow
		=> new(_obj.GetFlags(out var x), (ShellLinkDataListFlag)x);

	public ComResult SetFlagsNoThrow(ShellLinkDataListFlag flags)
		=> new(_obj.SetFlags((uint)flags));

	public ShellLinkDataListFlag Flags
	{
		get => FlagsNoThrow.Value;
		set => SetFlagsNoThrow(value).ThrowIfError();
	}
}

[Flags]
public enum ShellLinkDataListFlag : uint
{
	Default = 0x00000000,
	HasIDList = 0x00000001,
	HasLinkInfo = 0x00000002,
	HasName = 0x00000004,
	HasRelativePath = 0x00000008,
	HasWorkingDirectory = 0x00000010,
	HasArguments = 0x00000020,
	HasIconLocation = 0x00000040,
	Unicode = 0x00000080,
	ForceNoLinkInfo = 0x00000100,
	HasExpSz = 0x00000200,
	RunInSeparate = 0x00000400,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("現在使用されていません。")]
	HasLogo3ID = 0x00000800,
	HasDarwinID = 0x00001000,
	RunAsUser = 0x00002000,
	HasExpIconSz = 0x00004000,
	NoItemIDAlias = 0x00008000,
	ForceUncName = 0x00010000,
	RunWithShimLayer = 0x00020000,
	ForceNoLinkTrack = 0x00040000,
	EnableTargetMetadata = 0x00080000,
	DisableLinkPathTracking = 0x00100000,
	DisableKnownFolderRelativeTracking = 0x00200000,
	NoKnownFolderAlias = 0x00400000,
	AllowLinkToLink = 0x00800000,
	UnaliasOnSave = 0x01000000,
	PreferEnvironmentPath = 0x02000000,
	KeepLocalIDListForUncTarget = 0x04000000,
	PersistVolumeIDRelative = 0x08000000,
	Valid = 0x0FFFF7FF,
	Reserved = 0x80000000,
}
