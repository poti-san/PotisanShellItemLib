namespace Potisan.Windows.Com.SafeHandles;

/// <summary>
/// COMタスクメモリポインタのセーフハンドルです。
/// </summary>
public class SafeCoTaskMemHandle : SafeHandle
{
	/// <summary>
	/// 所有権のある空のセーフハンドルを作成します。
	/// </summary>
	public SafeCoTaskMemHandle()
		: base(0, true)
	{
	}

	/// <summary>
	/// ハンドルと所有権を指定してセーフハンドルを作成します。
	/// </summary>
	/// <param name="handle"></param>
	/// <param name="owns"></param>
	public SafeCoTaskMemHandle(nint handle, bool owns)
		: base(handle, owns)
	{
	}

	/// <summary>
	/// ハンドルが無効か。<c>0</c>の場合は真です。
	/// </summary>
	public override bool IsInvalid => handle == 0;

	/// <summary>
	/// ハンドルを解放します。常に<c>true</c>を返します。
	/// </summary>
	protected override bool ReleaseHandle()
	{
		Marshal.FreeCoTaskMem(handle);
		return true;
	}
}
