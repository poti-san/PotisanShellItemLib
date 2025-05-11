namespace Potisan.Windows.Com;

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
