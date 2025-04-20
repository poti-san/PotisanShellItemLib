using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public static class NativeVariantHelper
{
	public static int VariantStructSize => Marshal.SizeOf<VARIANT>();
}

/// <summary>
/// COMタスクメモリ上に確保された<c>VARIANT</c>型配列。
///
/// <code>
/// {Variant1, Variant2, Variant3, ...}
/// </code>
/// </summary>
public sealed class NativeVariantArrayOnCoTaskMem : IDisposable
{
	public nint Pointer { get; private set; }
	public int Length { get; private set; }

	public NativeVariantArrayOnCoTaskMem() { }

	public NativeVariantArrayOnCoTaskMem(ReadOnlySpan<object?> objects)
	{
		[DllImport("oleaut32.dll")]
		static extern int VariantCopy(nint pvargDest, [MarshalAs(UnmanagedType.Struct)] in object? pvargSrc);

		var variantSize = NativeVariantHelper.VariantStructSize;
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

	public static NativeVariantArrayOnCoTaskMem FromEnumerable(IEnumerable<object?> objects)
	{
		[DllImport("oleaut32.dll")]
		static extern int VariantCopy(nint pvargDest, [MarshalAs(UnmanagedType.Struct)] in object? pvargSrc);

		var variantSize = NativeVariantHelper.VariantStructSize;

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

	public NativeVariantArrayOnCoTaskMem(int length)
	{
		[DllImport("oleaut32.dll")]
		static extern void VariantInit(nint pvarg);

		var variantSize = NativeVariantHelper.VariantStructSize;
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

	~NativeVariantArrayOnCoTaskMem()
	{
		InternalClear();
	}

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

		var variantSize = NativeVariantHelper.VariantStructSize;
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

	public object[] ToObjectArray()
	{
		[DllImport("oleaut32.dll")]
		static extern int VariantCopy([MarshalAs(UnmanagedType.Struct)] out object pvargDest, nint pvargSrc);

		var variantSize = NativeVariantHelper.VariantStructSize;
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