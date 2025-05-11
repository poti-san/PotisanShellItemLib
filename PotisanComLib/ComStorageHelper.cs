using System.Runtime.CompilerServices;

using Potisan.Windows.Com.SafeHandles;

namespace Potisan.Windows.Com;

/// <summary>
/// COMストレージ用のヘルパ機能。
/// </summary>
public static class ComStorageHelper
{
	/// <summary>
	/// COMストレージで使用される文字列名ブロックを作成します。
	/// </summary>
	/// <returns></returns>
	/// <remarks>
	/// 文字列名ブロックはnull終端の文字列ポインタ配列と実際の文字列からなる配列です。
	/// バイト配列として作成も可能ですが、作成後に移動されると最初のポインタ配列が不正となるためにメモリ上に作成します。
	/// </remarks>
	/// <example>
	/// <code>
	///<![CDATA[using System.Runtime.InteropServices;
	///
	///using Potisan.Windows.Com;
	///
	///var comMalloc = ComMalloc.Create();
	///
	///var snb = ComStorageHelper.CreateStringNameBlock(["ABC", "DEF", "あいう"]);
	///unsafe
	///{
	///	var p = (nint*)snb.DangerousGetHandle();
	///var view = new Span<byte>(p, (int)comMalloc.GetSize((nint)p));
	///
	///var s1 = Marshal.PtrToStringUni(p[0]);
	///var s2 = Marshal.PtrToStringUni(p[1]);
	///var s3 = Marshal.PtrToStringUni(p[2]);
	///var s4 = Marshal.PtrToStringUni(p[3]); // nullptr
	///
	///Console.WriteLine(s1); // "ABC"
	///Console.WriteLine(s2); // "DEF"
	///Console.WriteLine(s3); // "あいう"
	///Console.WriteLine(s4); // ""
	///}]]>
	/// </code>
	/// </example>
	public static SafeCoTaskMemHandle CreateStringNameBlock(ReadOnlySpan<string> strings)
	{
		// 文字列全体のバイト数（'\0'含む）
		var totalCb = 0;
		for (var i = 0; i < strings.Length; i++)
		{
			totalCb += (strings[i].Length + 1) * sizeof(char);
		}

		// 書き込み用の配列
		var pointersSize = nint.Size * (strings.Length + 1/*nullptr*/);
		var p = Marshal.AllocCoTaskMem(pointersSize + totalCb);
		try
		{
			unsafe
			{
				// 配列部分。書き込む度に加算。
				var parray = (nint*)p;
				// 実際の文字列の書き込み先。書き込む度に加算。
				var pstr = (char*)(p + pointersSize);

				for (var i = 0; i < strings.Length; i++)
				{
					*parray++ = (nint)pstr;
					var s = strings[i];
					fixed (char* ps = s)
					{
						var l = s.Length;
						Unsafe.CopyBlock(pstr, ps, (uint)l * sizeof(char));
						pstr += l;
						*pstr++ = '\0';
					}
				}
				*parray = 0;
			}

			return new(p, true);
		}
		catch
		{
			Marshal.FreeCoTaskMem(p);
			throw;
		}
	}
}
