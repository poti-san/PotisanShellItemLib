using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public sealed class MFSecureBuffer(object? o) : ComUnknownWrapperBase<IMFSecureBuffer>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> IDNoThrow
		=> new(_obj.GetIdentifier(out var x), x);

	public Guid ID
		=> IDNoThrow.Value;
}
