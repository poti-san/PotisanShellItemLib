using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Potisan.Windows.Com.SafeHandles;

namespace Potisan.Windows.Com;

/// <summary>
/// COMのヘルパー関数や定数を提供します。
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
	/// <item>nullを含む場合は<see cref="StructureArrayToPtrForClass{T}(ReadOnlySpan{T})"/>を使用してください。</item>
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


	/// <summary>
	/// クラスのアンマネージ表現配列を保持するCOMタスクメモリのセーフハンドルを作成します。nullを許容しません。
	/// </summary>
	/// <typeparam name="T">クラスの型。</typeparam>
	/// <param name="values">クラスの配列。</param>
	/// <param name="destroysStructuresOnDelete">メモリ解放時に各構造体を解放するか。</param>
	/// <returns>構造体またはクラスのアンマネージ表現配列を保持するCOMタスクメモリのポインタ。</returns>
	public static SafeStructureArrayOnCoTaskMemHandle<T> SafeStructureArrayToPtr<T>(ReadOnlySpan<T> values, bool destroysStructuresOnDelete)
		where T : notnull
	{
		nint p = 0;
		try
		{
			p = StructureArrayToPtr(values);
			return new(p, values.Length, destroysStructuresOnDelete);
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
	public static nint StructureArrayToPtrForClass<T>(ReadOnlySpan<T> values)
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

	/// <summary>
	/// クラスのアンマネージ表現配列を保持するCOMタスクメモリのセーフハンドルを作成します。nullを許容します。
	/// </summary>
	/// <typeparam name="T">クラスの型。</typeparam>
	/// <param name="values">クラスの配列。</param>
	/// <param name="destroysStructuresOnDelete">メモリ解放時に各構造体を解放するか。</param>
	/// <returns>構造体またはクラスのアンマネージ表現配列を保持するCOMタスクメモリのポインタ。</returns>
	public static SafeStructureArrayOnCoTaskMemHandle<T> SafeStructureArrayToPtrForClass<T>(ReadOnlySpan<T> values, bool destroysStructuresOnDelete)
		where T : class
	{
		var p = StructureArrayToPtrForClass(values);
		try
		{
			return new(p, values.Length, destroysStructuresOnDelete);
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}

	/// <summary>
	/// バイトデータを保持するCOMタスクメモリを作成します。
	/// </summary>
	/// <param name="data">保持するバイトデータ。</param>
	/// <returns>バイトデータを保持するCOMタスクメモリのポインタ。</returns>
	public static nint ByteArrayToPtr(ReadOnlySpan<byte> data)
	{
		var p = Marshal.AllocCoTaskMem(data.Length);
		try
		{
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

	/// <summary>
	/// バイトデータを保持するCOMタスクメモリのセーフハンドルを作成します。
	/// </summary>
	/// <param name="data">保持するバイトデータ。</param>
	/// <returns>バイトデータを保持するCOMタスクメモリのポインタ。</returns>
	public static SafeCoTaskMemHandle SafeByteArrayToPtr(ReadOnlySpan<byte> data)
	{
		nint p = ByteArrayToPtr(data);
		try
		{
			return new(p, true);
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}
}

/// <summary>
/// COMタスクメモリ上の構造体配列をメモリリークを不正で管理します。
/// 必要に応じて解放時に構造体の解放処理も呼び出せます。
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class SafeStructureArrayOnCoTaskMemHandle<T> : SafeHandle
{
	/// <summary>
	/// 構造体の個数を取得します。
	/// </summary>
	public int Count { get; }

	/// <summary>
	/// ハンドルの解放時に構造体を解放するか。
	/// </summary>
	public bool DestroysStructuresOnDelete { get; }

	internal SafeStructureArrayOnCoTaskMemHandle(nint p, int count, bool destroysStructuresOnDelete) : base(p, true)
	{
		Count = count;
		DestroysStructuresOnDelete = destroysStructuresOnDelete;
	}

	/// <inheritdoc/>
	public override bool IsInvalid => handle == 0;

	/// <inheritdoc/>
	protected override bool ReleaseHandle()
	{
		if (DestroysStructuresOnDelete)
		{
			// 状態を戻せないので例外はそのまま伝播させます。
			var structSize = Marshal.SizeOf<T>();
			var p = handle;
			for (var i = 0; i < Count; i++)
			{
				Marshal.DestroyStructure<T>(p);
				p += structSize;
			}
		}
		Marshal.FreeCoTaskMem(handle);
		return true;
	}
}