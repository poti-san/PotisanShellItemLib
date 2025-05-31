using System.Collections.Immutable;

using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

public sealed class PropertySetStorage(object? o) : ComUnknownWrapperBase<IPropertySetStorage>(o)
{
	public ComResult<PropertyStorage> CreateNoThrow(
			in Guid fmtId, in Guid clsid, PropertySetFlag flags, ComStorageMode mode)
		=> new(_obj.Create(fmtId, clsid, (uint)flags, (uint)mode, out var x), new(x));

	public PropertyStorage Create(in Guid fmtId, in Guid clsid, PropertySetFlag flags, ComStorageMode mode)
		=> CreateNoThrow(fmtId, clsid, flags, mode).Value;

	public ComResult<PropertyStorage> OpenNoThrow(in Guid fmtid, ComStorageMode mode)
		=> new(_obj.Open(fmtid, (uint)mode, out var x), new(x));

	public PropertyStorage Open(in Guid fmtid, ComStorageMode mode)
		=> OpenNoThrow(fmtid, mode).Value;

	public ComResult DeleteNoThrow(in Guid fmtid)
		=> new(_obj.Delete(fmtid));

	public void Delete(in Guid fmtid)
		=> DeleteNoThrow(fmtid).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<StatPropSetStorageEnumerable> StatPropSetStorageEnumerableNoThrow
		=> new(_obj.Enum(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public StatPropSetStorageEnumerable StatPropSetStorageEnumerable
		=> StatPropSetStorageEnumerableNoThrow.Value;

	public ImmutableArray<ComStatPropSetStorage> StatPropSetStorages
		=> [.. StatPropSetStorageEnumerable];
}

[Flags]
public enum PropertySetFlag : uint
{
	Default = 0,
	NonSimple = 0x1,
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	Ansi = 0x02,
	Unbuffered = 0x4,
	CaseSensitive = 0x08,
}