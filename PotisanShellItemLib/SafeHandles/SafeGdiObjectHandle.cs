namespace Potisan.Windows.Shell.SafeHandles;

/// <summary>
/// GDIオブジェクトの安全なハンドルです。<c>DeleteObject</c>ネイティブ関数で解放されます。
/// </summary>
public sealed class SafeGdiObjectHandle : SafeHandle
{
	public SafeGdiObjectHandle()
		: base(0, true)
	{
	}

	public SafeGdiObjectHandle(nint handle, bool owns)
		: base(handle, owns)
	{
	}

	public override bool IsInvalid => handle == 0;

	protected override bool ReleaseHandle()
	{
		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool DeleteObject(nint h);

		return DeleteObject(handle);
	}
}
