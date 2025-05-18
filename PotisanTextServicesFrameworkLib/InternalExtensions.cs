namespace Potisan.Windows.Text.Tsf;

internal static class InternalExtensions
{
	public static ref T GetReference<T>(this Span<T> span)
	{
		return ref MemoryMarshal.GetReference(span);
	}

	public static ref T GetReference<T>(this ReadOnlySpan<T> span)
	{
		return ref MemoryMarshal.GetReference(span);
	}
}
