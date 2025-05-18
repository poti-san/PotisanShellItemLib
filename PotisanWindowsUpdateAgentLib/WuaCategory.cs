using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public class WuaCategory(object? o) : ComUnknownWrapperBase<ICategory>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> NameNoThrow
		=> new(_obj.get_Name(out var x), x!);

	public string Name
		=> NameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> CategoryIDNoThrow
		=> new(_obj.get_CategoryID(out var x), x!);

	public string CategoryID
		=> CategoryIDNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaCategoryCollection> ChildrenNoThrow
		=> new(_obj.get_Children(out var x), new(x));

	public WuaCategoryCollection Children
		=> ChildrenNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DescriptionNoThrow
		=> new(_obj.get_Description(out var x), x!);

	public string Description
		=> DescriptionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaImageInformation> ImageNoThrow
		=> new(_obj.get_Image(out var x), new(x));

	public WuaImageInformation Image
		=> ImageNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> OrderNoThrow
		=> new(_obj.get_Order(out var x), x);

	public int Order
		=> OrderNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaCategory> ParentNoThrow
		=> new(_obj.get_Parent(out var x), new(x));

	public WuaCategory Parent
		=> ParentNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> TypeNoThrow
		=> new(_obj.get_Type(out var x), x!);

	public string Type
		=> TypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateCollection> UpdatesNoThrow
		=> new(_obj.get_Updates(out var x), new(x));

	public WuaUpdateCollection Updates
		=> UpdatesNoThrow.Value;
}
