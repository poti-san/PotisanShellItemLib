using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public static partial class ComHelper
{
	/// <summary>
	/// <c>VARIANT</c> Win SDK型のバイト数。
	/// </summary>
	public static int VariantStructSize => Marshal.SizeOf<VARIANT>();
}

/// <summary>
/// COMタスクメモリ上に確保された<c>VARIANT</c> Win SDK型可変長配列。
/// </summary>
public sealed class NativeVariantArrayOnCoTaskMem : IDisposable
{
	/// <summary>
	/// メモリアドレス。
	/// </summary>
	public nint Pointer { get; private set; }
	/// <summary>
	/// 構造体の個数。
	/// </summary>
	public int Length { get; private set; }

	/// <summary>
	/// 空の<c>VARIANT</c> Win SDK型メモリ上配列を作成します。
	/// </summary>
	public NativeVariantArrayOnCoTaskMem() { }

	/// <summary>
	/// 元となる要素を指定して<c>VARIANT</c> Win SDK型メモリ上配列を作成します。
	/// </summary>
	public NativeVariantArrayOnCoTaskMem(ReadOnlySpan<object?> objects)
	{
		[DllImport("oleaut32.dll")]
		static extern int VariantCopy(nint pvargDest, [MarshalAs(UnmanagedType.Struct)] in object? pvargSrc);

		var variantSize = ComHelper.VariantStructSize;
		var p0 = default(nint);
		try
		{
			p0 = Marshal.AllocCoTaskMem(objects.Length * variantSize);
			var p = p0;
			for (var i = 0; i < objects.Length; i++)
			{
				_ = VariantCopy(p, objects[i]);
				p += variantSize;
			}
			Pointer = p0;
			Length = objects.Length;
			GC.AddMemoryPressure(Length * variantSize);
		}
		catch
		{
			Marshal.FreeCoTaskMem(p0);
			throw;
		}
	}

	/// <summary>
	/// 元となる要素を指定して<c>VARIANT</c> Win SDK型メモリ上配列を作成します。
	/// </summary>
	public static NativeVariantArrayOnCoTaskMem FromEnumerable(IEnumerable<object?> objects)
	{
		[DllImport("oleaut32.dll")]
		static extern int VariantCopy(nint pvargDest, [MarshalAs(UnmanagedType.Struct)] in object? pvargSrc);

		var variantSize = ComHelper.VariantStructSize;

		var collection = objects as ICollection<object?> ?? [.. objects];
		var p0 = default(nint);
		try
		{
			p0 = Marshal.AllocCoTaskMem(collection.Count * variantSize);
			var p = p0;
			foreach (var o in collection)
			{
				_ = VariantCopy(p, o);
				p += variantSize;
			}
			var nva = new NativeVariantArrayOnCoTaskMem { Pointer = p0, Length = collection.Count };
			GC.AddMemoryPressure(nva.Length * variantSize);
			return nva;
		}
		catch
		{
			Marshal.FreeCoTaskMem(p0);
			throw;
		}
	}

	/// <summary>
	/// 長さを指定して<c>VARIANT</c> Win SDK型メモリ上配列を作成します。
	/// </summary>
	public NativeVariantArrayOnCoTaskMem(int length)
	{
		[DllImport("oleaut32.dll")]
		static extern void VariantInit(nint pvarg);

		var variantSize = ComHelper.VariantStructSize;
		var p0 = Marshal.AllocCoTaskMem(length * variantSize);
		var p = p0;
		for (var i = 0; i < length; i++)
		{
			VariantInit(p);
			p += variantSize;
		}
		Pointer = p0;
		Length = length;
		GC.AddMemoryPressure(Length * variantSize);
	}

	/// <summary>
	/// <c>VARIANT</c> Win SDK型メモリ上配列を解放します。各要素も解放します。
	/// </summary>
	~NativeVariantArrayOnCoTaskMem()
	{
		InternalClear();
	}

	/// <summary>
	/// <c>VARIANT</c> Win SDK型メモリ上配列を解放します。各要素も解放します。
	/// </summary>
	public void Dispose()
	{
		InternalClear();
		GC.SuppressFinalize(this);
	}

	private void InternalClear()
	{
		[DllImport("oleaut.dll")]
		static extern int VariantClear(nint pvarg);

		if (Pointer == 0) return;

		var variantSize = ComHelper.VariantStructSize;
		var p = Pointer;
		for (var i = 0; i < Length; i++)
		{
			_ = VariantClear(p);
			p += variantSize;
		}

		Pointer = 0;
		Length = 0;
		GC.RemoveMemoryPressure(Length * variantSize);
	}

	/// <summary>
	/// 各要素の複製を持つ<c>object[]</c>を作成します。
	/// </summary>
	public object[] ToObjectArray()
	{
		[DllImport("oleaut32.dll")]
		static extern int VariantCopy([MarshalAs(UnmanagedType.Struct)] out object pvargDest, nint pvargSrc);

		var variantSize = ComHelper.VariantStructSize;
		var p = Pointer;
		var objects = new object[Length];
		for (var i = 0; i < objects.Length; i++)
		{
			_ = VariantCopy(out objects[i], p);
			p += variantSize;
		}
		return objects;
	}
}