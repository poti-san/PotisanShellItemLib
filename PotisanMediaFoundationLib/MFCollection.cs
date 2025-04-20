using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFCollection(object? o) : ComUnknownWrapperBase<IMFCollection>(o)
{
	public static ComResult<MFCollection> CreateNoThrow()
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateCollection(out IMFCollection ppIMFCollection);

		return new(MFCreateCollection(out var x), new(x));
	}

	public static MFCollection Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> CountNoThrow
		=> new(_obj.GetElementCount(out var x), x);

	public uint Count
		=> CountNoThrow.Value;

	public ComResult<object> GetNoThrow(uint index)
		=> new(_obj.GetElement(index, out var x), x);

	public object Get(uint index)
		=> GetNoThrow(index).Value;

	public ComResult AddNoThrow(object? element)
		=> new(_obj.AddElement(element));

	public void Add(object? element)
		=> AddNoThrow(element).ThrowIfError();

	public ComResult<object?> RemoveNoThrow(uint index)
		=> new(_obj.RemoveElement(index, out var x), x);

	public object? Remove(uint index)
		=> RemoveNoThrow(index).Value;

	public ComResult InsertNoThrow(uint index, object? value)
		=> new(_obj.InsertElementAt(index, value));

	public void Insert(uint index, object? value)
		=> InsertNoThrow(index, value).ThrowIfError();

	public ComResult ClearNoThrow()
		=> new(_obj.RemoveAllElements());

	public void Clear()
		=> ClearNoThrow().ThrowIfError();
}
