namespace Potisan.Windows.Com.SafeHandles;

public class SafeCoTaskMemHandle : SafeHandle
{
	public SafeCoTaskMemHandle()
		: base(0, true)
	{
	}

	public SafeCoTaskMemHandle(nint handle, bool owns)
		: base(handle, owns)
	{
	}

	public override bool IsInvalid => handle == 0;

	protected override bool ReleaseHandle()
	{
		Marshal.FreeCoTaskMem(handle);
		return true;
	}
}
