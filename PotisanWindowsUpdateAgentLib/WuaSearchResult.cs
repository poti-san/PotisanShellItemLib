using System.Collections.Immutable;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaSearchResult(object? o) : ComUnknownWrapperBase<ISearchResult>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaOperationResultCode> ResultCodeNoThrow
		=> new(_obj.get_ResultCode(out var x), x);

	public WuaOperationResultCode ResultCode
		=> ResultCodeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaCategoryCollection> RootCategoryCollectionNoThrow
		=> new(_obj.get_RootCategories(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public WuaCategoryCollection RootCategoryCollection
		=> RootCategoryCollectionNoThrow.Value;

	public ImmutableArray<WuaCategory> RootCategories
		=> [.. RootCategoryCollection];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateCollection> UpdateCollectionNoThrow
		=> new(_obj.get_RootCategories(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public WuaUpdateCollection UpdateCollection
		=> UpdateCollectionNoThrow.Value;

	public ImmutableArray<WuaUpdate> Updates
		=> [.. UpdateCollection];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateExceptionCollection> WarningCollectionNoThrow
		=> new(_obj.get_RootCategories(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public WuaUpdateExceptionCollection WarningCollection
		=> WarningCollectionNoThrow.Value;

	public ImmutableArray<WuaUpdateException> Warnings
		=> [.. WarningCollection];
}
