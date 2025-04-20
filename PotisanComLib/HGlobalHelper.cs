namespace Potisan.Windows.Com;

/// <summary>
/// グローバルメモリ(HGLOBAL)のヘルパー関数。
/// </summary>
public static class HGlobalHelper
{
	public static nuint SizeOf(nint handle)
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern nuint GlobalSize(nint hMem);

		var size = GlobalSize(handle);
		if (size == 0 && Marshal.GetLastPInvokeError() != 0)
			Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		return size;
	}

	public static byte[] ReadAllBytes(nint handle)
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern nint GlobalLock(nint hMem);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool GlobalUnlock(nint hMem);

		var p = GlobalLock(handle);
		if (p == 0)
			Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		try
		{
			var buffer = GC.AllocateUninitializedArray<byte>(checked((int)SizeOf(handle)));
			Marshal.Copy(p, buffer, 0, buffer.Length);
			return buffer;
		}
		finally
		{
			if (!GlobalUnlock(handle) && Marshal.GetLastWin32Error() != 0)
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		}
	}
}
