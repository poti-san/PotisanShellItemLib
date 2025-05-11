using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// 名前付き項目のプロパティストア。INamedPropertyStore COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class NamedPropertyStore(object? o) : ComUnknownWrapperBase<INamedPropertyStore>(o)
{
	public static ComResult<NamedPropertyStore> CreateInMemoryNoThrow()
	{
		var cr = PropertyStore.CreateInMemoryNoThrow();
		return new(cr.HResult, new(cr.ValueUnchecked.WrappedObject));
	}

	public static NamedPropertyStore CreateInMemory()
		=> CreateInMemoryNoThrow().Value;

	public ComResult<PropVariant> GetNamedValueNoThrow(string name)
	{
		var x = new PropVariant();
		return new(_obj.GetNamedValue(name, x), x);
	}

	public PropVariant GetNamedValue(string name)
		=> GetNamedValueNoThrow(name).Value;

	public ComResult SetNamedValueNoThrow(string name, PropVariant value)
		=> new(_obj.SetNamedValue(name, value));

	public void SetNamedValue(string name, PropVariant value)
		=> SetNamedValueNoThrow(name, value).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> NameCountNoThrow
		=> new(_obj.GetNameCount(out var x), x);

	public uint NameCount
		=> NameCountNoThrow.Value;

	public ComResult<string> GetNameAtNoThrow(uint index)
		=> new(_obj.GetNameAt(index, out var x), x);

	public string GetNameAt(uint index)
		=> GetNameAtNoThrow(index).Value;
}