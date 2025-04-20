using System.Diagnostics.CodeAnalysis;

namespace PotisanShellItemLib.Core;

/// <summary>
/// COMタスクメモリのヘルパー関数を提供します。
/// </summary>
public static class CoTaskMemHelper
{
	/// <summary>
	/// 構造体またはクラスのアンマネージ表現を保持するCOMタスクメモリを作成します。
	/// </summary>
	/// <typeparam name="T">構造体またはクラスの型。</typeparam>
	/// <param name="value">構造体またはクラス。</param>
	/// <returns>構造体またはクラスのアンマネージ表現を保持するCOMタスクメモリ。</returns>
	public static nint CoTaskMemAllocWithStructure<T>([DisallowNull] in T value)
	{
		var p = Marshal.AllocCoTaskMem(Marshal.SizeOf<T>());
		try
		{
			Marshal.StructureToPtr(value, p, false);
			return p;
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}
}
