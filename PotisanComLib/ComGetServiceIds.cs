using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public class ComGetServiceIds(object? o) : ComUnknownWrapperBase<IGetServiceIds>(o)
{
	public ComResult<Guid[]> GetServiceIDsNoThrow()
		=> new(_obj.GetServiceIds(out _, out var x), x);

	public Guid[] GetServiceIDs()
		=> GetServiceIDsNoThrow().Value;
}
