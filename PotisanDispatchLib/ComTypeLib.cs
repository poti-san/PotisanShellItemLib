using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public class ComTypeLib(object? o) : ComUnknownWrapperBase<ITypeLib>(o)
{
	public uint TypeInfoCount
		=> _obj.GetTypeInfoCount();

	public ComResult<ComTypeInfo> GetTypeInfoNoThrow(uint index)
		=> new(_obj.GetTypeInfo(index, out var x), new(x));

	public ComTypeInfo GetTypeInfo(uint index)
		=> GetTypeInfoNoThrow(index).Value;

	public IEnumerable<ComTypeInfo> TypeInfoEnumerable
	{
		get
		{
			var c = TypeInfoCount;
			for (uint i = 0; i < c; i++)
				yield return GetTypeInfo(i);
		}
	}

	public ImmutableArray<ComTypeInfo> TypeInfos
		=> [.. TypeInfoEnumerable];

	public ComResult<ComTypeKind> GetTypeInfoTypeNoThrow(uint index)
		=> new(_obj.GetTypeInfoType(index, out var x), x);

	public ComTypeKind GetTypeInfoType(uint index)
		=> GetTypeInfoTypeNoThrow(index).Value;

	public IEnumerable<ComTypeKind> TypeInfoTypeEnumerable
	{
		get
		{
			var c = TypeInfoCount;
			for (uint i = 0; i < c; i++)
				yield return GetTypeInfoType(i);
		}
	}

	public ImmutableArray<ComTypeKind> TypeInfoTypes
		=> [.. TypeInfoTypeEnumerable];

	public ComResult<ComTypeInfo> GetTypeInfoNoThrow(in Guid guid)
		=> new(_obj.GetTypeInfoOfGuid(guid, out var x), new(x));

	public ComTypeInfo GetTypeInfo(in Guid guid)
		=> GetTypeInfoNoThrow(guid).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComTypeLibAttribute> LibraryAttributeNoThrow
	{
		get
		{
			var cr = new ComResult(_obj.GetLibAttr(out var p));
			try
			{
				return new(cr.HResult, Marshal.PtrToStructure<ComTypeLibAttribute>(p)!);
			}
			finally
			{
				_obj.ReleaseTLibAttr(p);
			}
		}
	}

	public ComTypeLibAttribute LibraryAttribute
		=> LibraryAttributeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComTypeComp> TypeCompNoThrow
		=> new(_obj.GetTypeComp(out var x), new(x));

	public ComTypeComp TypeComp
		=> TypeCompNoThrow.Value;

	public ComResult<ComTypeInfoDocumentation> GetTypeInfoDocumentationNoThrow(int index)
		=> new(_obj.GetDocumentation(index, out var x1, out var x2, out var x3, out var x4), new(x1 ?? "", x2 ?? "", x3, x4 ?? ""));

	public ComTypeInfoDocumentation GetTypeInfoDocumentation(int index)
		=> GetTypeInfoDocumentationNoThrow(index).Value;

	public IEnumerable<ComTypeInfoDocumentation> TypeInfoDocumentationEnumerable
	{
		get
		{
			var c = TypeInfoCount;
			for (int i = 0; i < c; i++)
				yield return GetTypeInfoDocumentation(i);
		}
	}

	public ImmutableArray<ComTypeInfoDocumentation> TypeInfoDocumentations
		=> [.. TypeInfoDocumentationEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComTypeInfoDocumentation> DocumentationNoThrow
		=> GetTypeInfoDocumentationNoThrow(-1);

	public ComTypeInfoDocumentation Documentation
		=> DocumentationNoThrow.Value;

	public ComResult<bool> IsNameNoThrow(string name, uint hashValue)
		=> new(_obj.IsName(name, hashValue, out var x), x);

	public bool IsName(string name, uint hashValue)
		=> IsNameNoThrow(name, hashValue).Value;

	public ComResult<(ImmutableArray<ComTypeInfo> TypeInfos, ImmutableArray<ComMemberID> MemberIds)> FindNameNoThrow(string name, uint hashValue)
		=> new(_obj.FindName(name, hashValue, out var tinfos, out var memIds, out _),
			([.. tinfos.Select(x => new ComTypeInfo(x))], ImmutableCollectionsMarshal.AsImmutableArray(memIds)));

	public (ImmutableArray<ComTypeInfo> TypeInfos, ImmutableArray<ComMemberID> MemberIds) FindName(string name, uint hashValue)
		=> FindNameNoThrow(name, hashValue).Value;

	//
	// COMインターフェイス以外
	//

	public static ComResult<ComTypeLib> LoadFromFileNoThrow(string path, ComTypeLibRegisteredKind kind = ComTypeLibRegisteredKind.None)
	{
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		static extern int LoadTypeLibEx(string szFile, ComTypeLibRegisteredKind regkind, out ITypeLib? pptlib);

		return new(LoadTypeLibEx(path, kind, out var x), new(x));
	}

	public static ComTypeLib LoadFromFile(string path, ComTypeLibRegisteredKind kind = ComTypeLibRegisteredKind.None)
		=> LoadFromFileNoThrow(path, kind).Value;

	public static ComResult<ComTypeLib> LoadFromRegistryNoThrow(in Guid typelibId, ushort majorVersion, ushort minorVersion = 0, Lcid? lcid = null)
	{
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		static extern int LoadRegTypeLib(in Guid rguid, ushort wVerMajor, ushort wVerMinor, Lcid lcid, out ITypeLib? pptlib);

		return new(LoadRegTypeLib(typelibId, majorVersion, minorVersion, lcid ?? Lcid.UserDefault, out var x), new(x));
	}

	public static ComTypeLib LoadFromRegistry(in Guid typelibId, ushort majorVersion, ushort minorVersion = 0, Lcid? lcid = null)
		=> LoadFromRegistryNoThrow(typelibId, majorVersion, minorVersion, lcid).Value;

	public ComResult RegisterOnSystemNoThrow(string fullPath, string? helpDir)
	{
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		static extern int RegisterTypeLib(ITypeLib ptlib, string szFullPath, string? szHelpDir);

		return new(RegisterTypeLib(_obj, fullPath, helpDir));
	}

	public void RegisterOnSystem(string fullPath, string? helpDir)
		=> RegisterOnSystemNoThrow(fullPath, helpDir).ThrowIfError();

	public ComResult RegisterForUserNoThrow(string fullPath, string? helpDir)
	{
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		static extern int RegisterTypeLibForUser(ITypeLib ptlib, string szFullPath, string? szHelpDir);

		return new(RegisterTypeLibForUser(_obj, fullPath, helpDir));
	}

	public void RegisterForUser(string fullPath, string? helpDir)
		=> RegisterForUserNoThrow(fullPath, helpDir).ThrowIfError();

	public static ComResult UnregisterOnSystemNoThrow(in Guid typelibId, ushort majorVersion, ushort minorVersion, ComSystemKind systemKind, Lcid? lcid = null)
	{
		[DllImport("oleaut32.dll")]
		static extern int UnRegisterTypeLib(in Guid libID, ushort wVerMajor, ushort wVerMinor, Lcid lcid, ComSystemKind syskind);

		return new(UnRegisterTypeLib(typelibId, majorVersion, minorVersion, lcid ?? Lcid.UserDefault, systemKind));
	}

	public static void UnregisterOnSystem(in Guid typelibId, ushort majorVersion, ushort minorVersion, ComSystemKind systemKind, Lcid lcid)
		=> UnregisterOnSystemNoThrow(typelibId, majorVersion, minorVersion, systemKind, lcid).ThrowIfError();

	public static ComResult UnregisterForUserNoThrow(in Guid typelibId, ushort majorVersion, ushort minorVersion, ComSystemKind systemKind, Lcid? lcid = null)
	{
		[DllImport("oleaut32.dll")]
		static extern int UnRegisterTypeLibForUser(in Guid libID, ushort wVerMajor, ushort wVerMinor, Lcid lcid, ComSystemKind syskind);

		return new(UnRegisterTypeLibForUser(typelibId, majorVersion, minorVersion, lcid ?? Lcid.UserDefault, systemKind));
	}

	public static void UnregisterForUser(in Guid typelibId, ushort majorVersion, ushort minorVersion, ComSystemKind systemKind, Lcid? lcid = null)
		=> UnregisterForUserNoThrow(typelibId, majorVersion, minorVersion, systemKind, lcid).ThrowIfError();

	public static ComResult<string> QueryPathOfRegisteredTypeLibNoThrow(in Guid typelibId, ushort majorVersion, ushort minorVersion = 0, Lcid? lcid = null)
	{
		[DllImport("oleaut32.dll")]
		static extern int QueryPathOfRegTypeLib(in Guid guid, ushort wMaj, ushort wMin, Lcid lcid, [MarshalAs(UnmanagedType.BStr)] out string? lpbstrPathName);

		return new(QueryPathOfRegTypeLib(typelibId, majorVersion, minorVersion, lcid ?? Lcid.UserDefault, out var x), x!);
	}

	public static string QueryPathOfRegisteredTypeLib(in Guid typelibId, ushort majorVersion, ushort minorVersion = 0, Lcid? lcid = null)
		=> QueryPathOfRegisteredTypeLibNoThrow(typelibId, majorVersion, minorVersion, lcid ?? Lcid.UserDefault).Value;
}

/// <summary>
/// SYSKIND
/// </summary>
public enum ComSystemKind : uint
{
	Win16 = 0,
	Win32 = (Win16 + 1),
	Mac = (Win32 + 1),
	Win64 = (Mac + 1),
}

/// <summary>
/// LIBFLAGS
/// </summary>
[Flags]
public enum ComLibraryFlag : ushort
{
	Restricted = 0x1,
	Control = 0x2,
	Hidden = 0x4,
	HarddiskImage = 0x8
}

/// <summary>
/// TLIBATTR
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public sealed record class ComTypeLibAttribute(
	Guid Guid,
	Lcid Lcid,
	ComSystemKind SystemKind,
	ushort MajorVersionNumber,
	ushort MinorVersionNumber,
	ComLibraryFlag LibraryFlags);

/// <summary>
/// <c>REGKIND_*</c>
/// </summary>
public enum ComTypeLibRegisteredKind
{
	/// <summary>
	/// 既定の方法でタイプライブラリを読み込みます。
	/// パスが指定されていない場合、後方互換性のためにレジストリに登録されます。
	/// </summary>
	Default = 0,
	/// <summary>
	/// タイプライブラリを読み込み、レジストリにも登録します。
	/// </summary>
	Register = 1,
	/// <summary>
	/// タイプライブラリを読み込みます。レジストリには登録しません。
	/// </summary>
	None = 2,
}