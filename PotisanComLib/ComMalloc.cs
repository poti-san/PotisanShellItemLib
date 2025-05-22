using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMメモリアロケータ。
/// </summary>
/// <param name="o">RCWオブジェクト</param>
/// <remarks>
/// <c>IMalloc</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ComMalloc(object? o) : ComUnknownWrapperBase<IMalloc>(o)
{
	public static ComResult<ComMalloc> CreateNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CoGetMalloc(uint dwMemContext, out IMalloc ppMalloc);

		return new(CoGetMalloc(1, out var x), new(x));
	}

	public static ComMalloc Create()
		=> CreateNoThrow().Value;

	public nint Alloc(nuint cb)
		=> _obj.Alloc(cb);

	public nint Realloc(nint p, nuint cb)
		=> _obj.Realloc(p, cb);

	public void Free(nint p)
		=> _obj.Free(p);

	public nuint GetSize(nint p)
		=> _obj.GetSize(p);

	public bool? DidAlloc(nint p)
		=> _obj.DidAlloc(p) switch { 0 => false, 1 => true, -1 => null, _ => throw new NotSupportedException() };

	public void HeapMinimize()
		=> _obj.HeapMinimize();
}
