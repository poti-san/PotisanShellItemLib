using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.Text.Tsf.ComTypes;

namespace Potisan.Windows.Text.Tsf;

public class TFCompartmentManager(object? o) : ComUnknownWrapperBase<ITfCompartmentMgr>(o)
{
	public ComResult<TFCompartment> GetNoThrow(in Guid guid)
		=> new(_obj.GetCompartment(guid, out var x), new(x));

	public TFCompartment Get(in Guid guid)
		=> GetNoThrow(guid).Value;

	public ComResult ClearNoThrow(TFClientID id, in Guid guid)
		=> new(_obj.ClearCompartment(id, guid));

	public void Clear(TFClientID id, in Guid guid)
		=> ClearNoThrow(id, guid).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComGuidEnumerable> GuidEnumerableNoThrow
		=> new(_obj.EnumCompartments(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComGuidEnumerable GuidEnumerable
		=> GuidEnumerableNoThrow.Value;

	public ImmutableArray<Guid> Guids
		=> [.. GuidEnumerable];
}
