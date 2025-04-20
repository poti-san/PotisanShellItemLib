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
