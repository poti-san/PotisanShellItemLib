using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Potisan.Windows.Com.SafeHandles;

namespace Potisan.Windows.Com;

/// <summary>
/// COMタスクメモリのヘルパー関数。
/// </summary>
public static partial class ComHelper
{
	/// <summary>
	/// 構造体またはクラスのアンマネージ表現を保持するCOMタスクメモリを作成します。
	/// </summary>
	/// <typeparam name="T">構造体またはクラスの型。</typeparam>
	/// <param name="value">構造体またはクラス。</param>
	/// <returns>構造体またはクラスのアンマネージ表現を保持するCOMタスクメモリのポインタ。</returns>
	public static nint StructureToPtr<T>([DisallowNull] in T value)
	{
		nint p = 0;
		try
		{
			p = Marshal.AllocCoTaskMem(Marshal.SizeOf<T>());
			Marshal.StructureToPtr(value, p, false);
			return p;
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}

	/// <summary>
	/// 構造体またはクラスのアンマネージ表現配列を保持するCOMタスクメモリを作成します。nullを許容しません。
	/// </summary>
	/// <typeparam name="T">構造体またはクラスの型。</typeparam>
	/// <param name="values">構造体またはクラスの配列。</param>
	/// <returns>構造体またはクラスのアンマネージ表現配列を保持するCOMタスクメモリのポインタ。</returns>
	/// <remarks>
	/// <list type="bullet">
	/// <item>メモリ上の構造体は必要に応じて<see cref="Marshal.DestroyStructure{T}(nint)"/>等で解放してください。</item>
	/// <item>nullを含む場合は<see cref="StructureToPtrForClass{T}(ReadOnlySpan{T})"/>を使用してください。</item>
	/// </list>
	/// </remarks>
	public static nint StructureArrayToPtr<T>(ReadOnlySpan<T> values)
		where T : notnull
	{
		var structSize = Marshal.SizeOf<T>();
		nint p0 = 0;
		try
		{
			p0 = Marshal.AllocCoTaskMem(structSize * values.Length);
			var p = p0;
			for (var i = 0; i < values.Length; i++)
			{
				Marshal.StructureToPtr(values[i], p, false);
				p += structSize;
			}
			return p0;
		}
		catch
		{
			Marshal.FreeCoTaskMem(p0);
			throw;
		}
	}

	public static SafeCoTaskMemHandle SafeStructureArrayToPtr<T>(ReadOnlySpan<T> values)
		where T : notnull
	{
		nint p = 0;
		try
		{
			p = StructureArrayToPtr<T>(values);
			return new(p, true);
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}

	/// <summary>
	/// クラスのアンマネージ表現配列を保持するCOMタスクメモリを作成します。nullを許容します。
	/// </summary>
	/// <typeparam name="T">クラスの型。</typeparam>
	/// <param name="values">クラスの配列。</param>
	/// <returns>構造体またはクラスのアンマネージ表現配列を保持するCOMタスクメモリのポインタ。</returns>
	/// <remarks>
	/// メモリ上の構造体は必要に応じて<see cref="Marshal.DestroyStructure{T}(nint)"/>等で解放してください。
	/// </remarks>
	public static nint StructureToPtrForClass<T>(ReadOnlySpan<T> values)
		where T : class
	{
		var structSize = Marshal.SizeOf<T>();
		nint p0 = 0;
		try
		{
			p0 = Marshal.AllocCoTaskMem(structSize * values.Length);
			var p = p0;
			for (var i = 0; i < values.Length; i++)
			{
				if (values[i] != null)
					Marshal.StructureToPtr(values[i], p, false);
				p += structSize;
			}
			return p0;
		}
		catch
		{
			Marshal.FreeCoTaskMem(p0);
			throw;
		}
	}

	public static SafeCoTaskMemHandle SafeStructureToPtrForClass<T>(ReadOnlySpan<T> values)
		where T : class
	{
		nint p = 0;
		try
		{
			p = StructureToPtrForClass<T>(values);
			return new(p, true);
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}

	/// <summary>
	/// バイトデータのコピーを保持するCOMタスクメモリを作成します。
	/// </summary>
	/// <param name="data">保持するバイトデータ。</param>
	/// <returns>バイトデータを保持するCOMタスクメモリのポインタ。</returns>
	public static nint ByteArrayToPtr(ReadOnlySpan<byte> data)
	{
		nint p = 0;
		try
		{
			p = Marshal.AllocCoTaskMem(data.Length);
			unsafe
			{
				Unsafe.Copy((void*)p, ref MemoryMarshal.GetReference(data));
			}
			return p;
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}

	public static SafeCoTaskMemHandle SafeByteArrayToPtr(ReadOnlySpan<byte> data)
	{
		nint p = 0;
		try
		{
			p = ByteArrayToPtr(data);
			return new(p, true);
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}
}
