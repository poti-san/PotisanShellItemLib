namespace Potisan.Windows.Com.Automation;

public static class InternalUtil
{
	public static T[] PtrToUnmanagedArray<T>(nint p, int length)
		where T : unmanaged
	{
		unsafe
		{
			return [.. new ReadOnlySpan<T>((void*)p, length)];
		}
	}
}
