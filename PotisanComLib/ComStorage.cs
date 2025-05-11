using System.Runtime.CompilerServices;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMストレージ。
/// </summary>
/// <param name="o"></param>
/// <example>
/// マイドキュメントに「test.stg」ファイルを作成します。ファイルが既に存在する場合は失敗します。
/// <code>
///<![CDATA[using System.Runtime.InteropServices;
///
///using Potisan.Windows.Com;
///
///var storageFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.stg");
///var storage = ComStorage.CreateFile(storageFilePath);
///
///var stream1 = storage.CreateStream("Stream1");
///stream1.Write(MemoryMarshal.AsBytes("ABCDEFG".AsSpan()));
///
///var stream2 = storage.CreateStream("Stream2");
///stream2.Write(MemoryMarshal.AsBytes("あいうえお".AsSpan()));
///
///storage.Commit();
///]]>
/// </code>
/// </example>
public class ComStorage(object? o) : ComUnknownWrapperBase<IStorage>(o)
{
	public ComResult<ComStream> CreateStreamNoThrow(string name, ComStorageMode mode)
		=> new(_obj.CreateStream(name, (uint)mode, 0, 0, out var x), new(x));

	public ComStream CreateStream(string name, ComStorageMode mode = ComStorageMode.ReadWrite | ComStorageMode.ShareExclusive)
		=> CreateStreamNoThrow(name, mode).Value;

	public ComResult<ComStream> OpenStreamNoThrow(string name, ComStorageMode mode = ComStorageMode.ReadWrite | ComStorageMode.ShareExclusive)
		=> new(_obj.OpenStream(name, 0, (uint)mode, 0, out var x), new(x));

	public ComStream OpenStream(string name, ComStorageMode mode)
		=> OpenStreamNoThrow(name, mode).Value;

	public ComResult<ComStorage> CreateStorageNoThrow(string name, ComStorageMode mode)
		=> new(_obj.CreateStorage(name, (uint)mode, 0, 0, out var x), new(x));

	public ComStorage CreateStorage(string name, ComStorageMode mode)
		=> CreateStorageNoThrow(name, mode).Value;

	public ComResult<ComStorage> OpenStorageNoThrow(string name, ComStorageMode mode, ReadOnlySpan<string> excludes)
	{
		using var snb = ComStorageHelper.CreateStringNameBlock(excludes);
		return new(_obj.OpenStorage(name, null, (uint)mode, snb.DangerousGetHandle(), 0, out var x), new(x));
	}

	public ComStorage OpenStorage(string name, ComStorageMode mode, ReadOnlySpan<string> excludes)
		=> OpenStorageNoThrow(name, mode, excludes).Value;

	public ComResult<ComStorage> OpenStorageNoThrow(string name, ComStorageMode mode)
		=> OpenStorageNoThrow(name, mode, []);

	public ComStorage OpenStorage(string name, ComStorageMode mode)
		=> OpenStorage(name, mode, []);

	public ComResult CopyToNoThrow(ComStorage destination, Guid[]? excludeInterfaceIds = null, string[]? excludes = null)
	{
		using var p = excludes != null ? ComStorageHelper.CreateStringNameBlock(excludes) : null;
		return new(_obj.CopyTo(
			(uint)(excludeInterfaceIds?.Length ?? 0),
			excludeInterfaceIds != null ? MemoryMarshal.GetArrayDataReference(excludeInterfaceIds) : Unsafe.NullRef<Guid>(),
			p != null ? p.DangerousGetHandle() : 0,
			destination._obj!));
	}

	public void CopyTo(ComStorage destination, Guid[]? excludeInterfaceIds = null, string[]? excludes = null)
		=> CopyToNoThrow(destination, excludeInterfaceIds, excludes).ThrowIfError();

	public ComResult MoveElementToNoThrow(string name, ComStorage destination, string newName, ComStorageMove flags)
		=> new(_obj.MoveElementTo(name, destination._obj, newName, (uint)flags));

	public void MoveElementTo(string name, ComStorage destination, string newName, ComStorageMove flags)
		=> MoveElementToNoThrow(name, destination, newName, flags).ThrowIfError();

	public ComResult CommitNoThrow(ComStorageCommit flags = 0)
		=> new(_obj.Commit((uint)flags));

	public void Commit(ComStorageCommit flags = 0)
		=> CommitNoThrow(flags).ThrowIfError();

	public ComResult RevertNoThrow()
		=> new(_obj.Revert());

	public void Revert()
		=> RevertNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComStatStorageEnumerable> ComStatStorageEnumerableNoThrow
		=> new(_obj.EnumElements(0, 0, 0, out var x), new(x));

	public ComStatStorageEnumerable ComStatStorageEnumerable
		=> ComStatStorageEnumerableNoThrow.Value;

	public ComResult DestroyElementNoThrow(string oldName)
		=> new(_obj.DestroyElement(oldName));

	public void DestroyElement(string oldName)
		=> DestroyElementNoThrow(oldName).ThrowIfError();

	public ComResult RenameElementNoThrow(string oldName, string newName)
		=> new(_obj.RenameElement(oldName, newName));

	public void RenameElement(string oldName, string newName)
		=> RenameElementNoThrow(oldName, newName).ThrowIfError();

	public ComResult SetElementTimesNoThrow(string name, DateTime ctime, DateTime atime, DateTime mtime)
		=> new(_obj.SetElementTimes(name, ctime.ToFileTime(), atime.ToFileTime(), mtime.ToFileTime()));

	public void SetElementTimes(string name, DateTime ctime, DateTime atime, DateTime mtime)
		=> SetElementTimesNoThrow(name, ctime, atime, mtime).ThrowIfError();

	public ComResult SetClassNoThrow(in Guid clsid)
		=> new(_obj.SetClass(clsid));

	public void SetClass(in Guid clsid)
		=> SetClassNoThrow(clsid).ThrowIfError();

	/// <summary>
	/// 現在は機能しません。
	/// </summary>
	public ComResult SetStateBits(uint stateBits, uint mask)
		=> new(_obj.SetStateBits(stateBits, mask));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComStatStorage> StatNoThrow
		=> new(_obj.Stat(out var x, 0), x.GetAndFree());

	public ComStatStorage Stat
		=> StatNoThrow.Value;

	public static ComResult<ComStorage> CreateFileNoThrow(string name, ComStorageMode mode, ComStorageFormat format)
	{
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		static extern int StgCreateStorageEx(
			string pwcsName,
			uint grfMode,
			uint stgfmt,
			uint grfAttrs,
			[MarshalAs(UnmanagedType.LPArray)] ComStorageOptions[]? pStgOptions,
			nint pSecurityDescriptor,
			in Guid riid,
			[MarshalAs(UnmanagedType.IUnknown)] out object? ppObjectOpen);

		return new(StgCreateStorageEx(name, (uint)mode, (uint)format, 0, null, 0, typeof(IStorage).GUID, out var x), new(x));
	}

	public static ComStorage CreateFile(string name, ComStorageMode mode = ComStorageMode.ReadWrite | ComStorageMode.ShareExclusive, ComStorageFormat format = ComStorageFormat.Storage)
		=> CreateFileNoThrow(name, mode, format).Value;

	public static ComResult<ComStorage> OpenFileNoThrow(string name, ComStorageMode mode = ComStorageMode.ReadWrite | ComStorageMode.ShareExclusive, ComStorageFormat format = ComStorageFormat.Storage)
	{
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		static extern int StgOpenStorageEx(
			string pwcsName,
			uint grfMode,
			uint stgfmt,
			uint grfAttrs,
			[MarshalAs(UnmanagedType.LPArray)] ComStorageOptions[]? pStgOptions,
			nint pSecurityDescriptor,
			in Guid riid,
			[MarshalAs(UnmanagedType.IUnknown)] out object? ppObjectOpen);

		return new(StgOpenStorageEx(name, (uint)mode, (uint)format, 0, null, 0, typeof(IStorage).GUID, out var x), new(x));
	}

	public static ComStorage OpenFile(string name, ComStorageMode mode, ComStorageFormat format)
		=> OpenFileNoThrow(name, mode, format).Value;
}

/// <summary>
/// STGMOVE
/// </summary>
public enum ComStorageMove : uint
{
	Move = 0,
	Copy = 1,
	ShallowCopy = 2,
}

/// <summary>
/// STGFMT
/// </summary>
public enum ComStorageFormat : uint
{
	Storage = 0,
	File = 3,
	Any = 4,
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	DocFile = 5
}

/// <summary>
/// STGOPTIONS
/// </summary>
public struct ComStorageOptions
{
	public ushort Version;
	public ushort Reserved;
	public uint SectorSize;
	[MarshalAs(UnmanagedType.LPWStr)]
	public string TempleteFile;
}
