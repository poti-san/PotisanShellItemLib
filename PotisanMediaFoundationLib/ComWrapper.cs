using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class ComUnknownWrapperWithMFAttribute<TIUnknown>(object? o) : ComUnknownWrapperBase<TIUnknown>(o)
	where TIUnknown : class
{
	public MFAttributes Attributes { get; } = new(o as IMFAttributes);
}